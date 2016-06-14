using System.ComponentModel;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Mvvm.Services
{
    using Base;

    /// <summary>  Represents a recent file. </summary>
    public sealed class RecentFile : Model, IXmlSerializable
    {
        bool m_IsPinned;
        string m_Path;


        /// <summary> This constructor overload is reserved and should not be used. It is used internally by the XmlSerializer. </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public RecentFile() { }

        /// <summary> Initializes a new instance of the <see cref="RecentFile"/> class. </summary>
        /// <param name="_path">The file path.</param>
        /// <exception cref="ArgumentException">The argument path must not be null or empty.</exception>
        public RecentFile(string _path)
        {
            if (string.IsNullOrEmpty(_path)) { throw new ArgumentException("The argument path must not be null or empty."); }

            m_Path = _path;
        }


        /// <summary> Gets or sets a value indicating whether this recent file is pinned. </summary>
        public bool IsPinned
        {
            get { return m_IsPinned; }
            set { SetProperty(ref m_IsPinned, value); }
        }

        /// <summary> Gets the file path. </summary>
        public string Path => m_Path;


        XmlSchema IXmlSerializable.GetSchema() => null;

        void IXmlSerializable.ReadXml(XmlReader _reader)
        {
            if (null == _reader) { throw new ArgumentNullException(nameof(_reader)); }

            this.IsPinned = bool.Parse(_reader.GetAttribute(nameof(this.IsPinned)));
            m_Path = _reader.ReadElementContentAsString();
        }

        void IXmlSerializable.WriteXml(XmlWriter _writer)
        {
            if (null == _writer) { throw new ArgumentNullException(nameof(_writer)); }

            _writer.WriteAttributeString(nameof(this.IsPinned), IsPinned.ToString(CultureInfo.InvariantCulture));
            _writer.WriteString(this.Path);
        }
    }
}