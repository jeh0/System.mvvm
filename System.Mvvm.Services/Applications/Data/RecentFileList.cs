using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Mvvm.Services
{
    /// <summary> This class encapsulates the logic for a recent file list. </summary>
    /// <remarks> This class can be used in a Settings file to store and load the recent file list as user settings. In Visual Studio you might need
    /// to enter the full class name "System.Waf.Applications.RecentFileList" in the "Select a type" dialog.
    /// </remarks>
    public sealed class RecentFileList : IXmlSerializable
    {
        const string RECENT_FILE = "RecentFile";

        int m_MaxFilesNumber = 8;
        readonly ObservableCollection<RecentFile> m_RecentFiles;
        readonly ReadOnlyObservableCollection<RecentFile> m_ReadOnlyRecentFiles;
        

        /// <summary> Initializes a new instance of the <see cref="RecentFileList"/> class. </summary>
        public RecentFileList()
        {
            m_RecentFiles = new ObservableCollection<RecentFile>();
            m_ReadOnlyRecentFiles = new ReadOnlyObservableCollection<RecentFile>(m_RecentFiles);
        }


        /// <summary> Gets the list of recent files. </summary>
        public ReadOnlyObservableCollection<RecentFile> RecentFiles { get { return m_ReadOnlyRecentFiles; } }

        /// <summary> Gets or sets the maximum number of recent files in the list. </summary>
        /// <remarks> If the set number is lower than the list count then the recent file list is truncated to the number. </remarks>
        /// <exception cref="ArgumentException">The value must be equal or larger than 1.</exception>
        public int MaxFilesNumber
        {
            get { return m_MaxFilesNumber; }
            set
            {
                if (m_MaxFilesNumber != value)
                {
                    if (value <= 0)
                        throw new ArgumentException("The value must be equal or larger than 1.");

                    m_MaxFilesNumber = value;

                    if (m_RecentFiles.Count - m_MaxFilesNumber >= 1)
                        this.RemoveRange(m_MaxFilesNumber, m_RecentFiles.Count - m_MaxFilesNumber);
                }
            }
        }

        private int PinCount => m_RecentFiles.Count(r => r.IsPinned);

        public static string RECENT_FILE_nameField => RECENT_FILE;


        /// <summary> Loads the specified collection into the recent file list. Use this method when you need to initialize the RecentFileList 
        /// manually. This can be useful when you are using an own persistence implementation.
        /// </summary>
        /// <remarks>Recent file items that exist before the Load method is called are removed.</remarks>
        /// <param name="_recentFiles">The recent files.</param>
        /// <exception cref="ArgumentNullException">The argument recentFiles must not be null.</exception>
        public void Load(IEnumerable<RecentFile> _recentFiles)
        {
            if (null == _recentFiles) { throw new ArgumentNullException(nameof(_recentFiles)); }

            Clear();
            AddRange(_recentFiles.Take(m_MaxFilesNumber));
        }

        /// <summary> Adds a file to the recent file list. If the file already exists in the list then it only changes the position in the list. </summary>
        /// <param name="_fileName">The path of the recent file.</param>
        /// <exception cref="ArgumentException">The argument fileName must not be null or empty.</exception>
        public void AddFile(string _fileName)
        {
            if (string.IsNullOrEmpty(_fileName)) { throw new ArgumentException("The argument fileName must not be null or empty."); }

            RecentFile __recentFile = m_RecentFiles.FirstOrDefault(r => r.Path == _fileName);
            
            if (null != __recentFile)
            {
                int __oldIndex = m_RecentFiles.IndexOf(__recentFile);
                int __newIndex = __recentFile.IsPinned ? 0 : PinCount;
                if (__oldIndex != __newIndex)
                {
                    m_RecentFiles.Move(__oldIndex, __newIndex);
                }
            }
            else
            {
                if (PinCount < m_MaxFilesNumber)
                {
                    if (m_RecentFiles.Count >= m_MaxFilesNumber)
                        RemoveAt(m_RecentFiles.Count - 1);

                    Insert(PinCount, new RecentFile(_fileName));
                }
            }
        }

        /// <summary> Removes the specified recent file. </summary>
        /// <param name="_recentFile">The recent file to remove.</param>
        /// <exception cref="ArgumentNullException">The argument recentFile must not be null.</exception>
        /// <exception cref="ArgumentException">The argument recentFile was not found in the recent files list.</exception>
        public void Remove(RecentFile _recentFile)
        {
            if (null == _recentFile)
                throw new ArgumentNullException(nameof(_recentFile));

            if (m_RecentFiles.Remove(_recentFile))
                _recentFile.PropertyChanged -= RecentFilePropertyChanged;

            else
                throw new ArgumentException("The passed recentFile was not found in the recent files list.");
        }

        XmlSchema IXmlSerializable.GetSchema() => null;

        void IXmlSerializable.ReadXml(XmlReader _reader)
        {
            if (null == _reader) { throw new ArgumentNullException(nameof(_reader)); }

            _reader.ReadToDescendant(RECENT_FILE);
            while (_reader.MoveToContent() == XmlNodeType.Element && _reader.LocalName == RECENT_FILE)
            {
                var __recentFile = new RecentFile();
                ((IXmlSerializable)__recentFile).ReadXml(_reader);
                Add(__recentFile);
            }

            if (!_reader.IsEmptyElement)
                _reader.ReadEndElement();
        }

        void IXmlSerializable.WriteXml(XmlWriter _writer)
        {
            if (null == _writer) { throw new ArgumentNullException(nameof(_writer)); }

            foreach (RecentFile _recentFile in m_RecentFiles)
            {
                _writer.WriteStartElement(RECENT_FILE);
                ((IXmlSerializable)_recentFile).WriteXml(_writer);
                _writer.WriteEndElement();
            }
        }

        void Insert(int _index, RecentFile _recentFile)
        {
            _recentFile.PropertyChanged += RecentFilePropertyChanged;
            m_RecentFiles.Insert(_index, _recentFile);
        }
        
        void Add(RecentFile _recentFile)
        {
            _recentFile.PropertyChanged += RecentFilePropertyChanged;
            m_RecentFiles.Add(_recentFile);
        }

        void AddRange(IEnumerable<RecentFile> _recentFilesToAdd)
        {
            foreach (RecentFile _recentFile in _recentFilesToAdd)
                Add(_recentFile);
        }

        void RemoveAt(int _index)
        {
            m_RecentFiles[_index].PropertyChanged -= RecentFilePropertyChanged;
            m_RecentFiles.RemoveAt(_index);
        }

        void RemoveRange(int _index, int _count)
        {
            for (int i = 0; i < _count; i++)
                RemoveAt(_index);
        }

        void Clear()
        {
            foreach (RecentFile _recentFile in m_RecentFiles)
                _recentFile.PropertyChanged -= RecentFilePropertyChanged;

            m_RecentFiles.Clear();
        }

        void RecentFilePropertyChanged(object _sender, PropertyChangedEventArgs _e)
        {
            if (_e.PropertyName == "IsPinned")
            {
                var __recentFile = (RecentFile)_sender;
                int __oldIndex = m_RecentFiles.IndexOf(__recentFile);
                if (__recentFile.IsPinned)
                    m_RecentFiles.Move(__oldIndex, 0);

                else
                {
                    int newIndex = PinCount;
                    if (__oldIndex != newIndex)
                        m_RecentFiles.Move(__oldIndex, newIndex);
                }
            }
        }
    }
}
