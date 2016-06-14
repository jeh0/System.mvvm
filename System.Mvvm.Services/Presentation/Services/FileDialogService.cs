using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using static System.String;

namespace System.Mvvm.Presentation
{
    using Services;

    /// <summary> This is the default implementation of the <see cref="IFileDialogService"/> interface. It shows an open or save file dialog box. </summary>
    /// <remarks> If the default implementation of this service doesn't serve your need then you can provide your own implementation. </remarks>
    [Export(typeof(IFileDialogService))]
    public class FileDialogService : IFileDialogService
    {
        /// <summary> Shows the open file dialog box that allows a user to specify a file that should be opened. </summary>
        /// <param name="_owner">The window that owns this OpenFileDialog.</param>
        /// <param name="_fileTypes">The supported file types.</param>
        /// <param name="_defaultFileType">Default file type.</param>
        /// <param name="_defaultFileName">Default filename. The directory name is used as initial directory when it is specified.</param>
        /// <returns>A FileDialogResult object which contains the filename selected by the user.</returns>
        /// <exception cref="ArgumentNullException">fileTypes must not be null.</exception>
        /// <exception cref="ArgumentException">fileTypes must contain at least one item.</exception>
        public FileDialogResult ShowOpenFileDialog(object _owner
            , IEnumerable<FileType> _fileTypes, FileType _defaultFileType, string _defaultFileName)
        {
            if (null == _fileTypes)
                throw new ArgumentNullException(nameof(_fileTypes));

            if (!_fileTypes.Any())
                throw new ArgumentException("The fileTypes collection must contain at least one item.");

            var __dialog = new OpenFileDialog();

            return ShowFileDialog(_owner, __dialog, _fileTypes, _defaultFileType, _defaultFileName);
        }

        /// <summary> Shows the save file dialog box that allows a user to specify a filename to save a file as. </summary>
        /// <param name="_owner">The window that owns this SaveFileDialog.</param>
        /// <param name="_fileTypes">The supported file types.</param>
        /// <param name="_defaultFileType">Default file type.</param>
        /// <param name="_defaultFileName">Default filename. The directory name is used as initial directory when it is specified.</param>
        /// <returns>A FileDialogResult object which contains the filename entered by the user.</returns>
        /// <exception cref="ArgumentNullException">fileTypes must not be null.</exception>
        /// <exception cref="ArgumentException">fileTypes must contain at least one item.</exception>
        public FileDialogResult ShowSaveFileDialog(object _owner
            , IEnumerable<FileType> _fileTypes, FileType _defaultFileType, string _defaultFileName)
        {
            if (null == _fileTypes)
                throw new ArgumentNullException(nameof(_fileTypes));

            if (!_fileTypes.Any())
                throw new ArgumentException("The fileTypes collection must contain at least one item.");

            var __dialog = new SaveFileDialog();

            return ShowFileDialog(_owner, __dialog, _fileTypes, _defaultFileType, _defaultFileName);
        }

        private static FileDialogResult ShowFileDialog(object _owner, FileDialog _dialog
            , IEnumerable<FileType> _fileTypes, FileType _defaultFileType, string _defaultFileName)
        {
            int __filterIndex = _fileTypes.ToList().IndexOf(_defaultFileType);
            if (0 <= __filterIndex)
                _dialog.FilterIndex = __filterIndex + 1;

            if (!IsNullOrEmpty(_defaultFileName))
            {
                _dialog.FileName = Path.GetFileName(_defaultFileName);
                string __directory = Path.GetDirectoryName(_defaultFileName);
                if (!IsNullOrEmpty(__directory))
                    _dialog.InitialDirectory = __directory;
            }

            _dialog.Filter = CreateFilter(_fileTypes);
            if (true == _dialog.ShowDialog(_owner as Window))
            {
                __filterIndex = _dialog.FilterIndex - 1;

                _defaultFileType = 0 <= __filterIndex && __filterIndex < _fileTypes.Count() ? _fileTypes.ElementAt(__filterIndex) : null;

                return new FileDialogResult(_dialog.FileName, _defaultFileType);
            }
            else
                return new FileDialogResult();
        }

        private static string CreateFilter(IEnumerable<FileType> _fileTypes)
        {
            string __filter = Empty;
            foreach (FileType _fileType in _fileTypes)
            {
                if (!IsNullOrEmpty(__filter))
                    __filter += "|";

                __filter += _fileType.Description + "|*" + _fileType.FileExtension;
            }

            return __filter;
        }
    }
}
