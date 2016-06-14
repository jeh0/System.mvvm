using System.Collections.Generic;

namespace System.Mvvm.Services
{
    /// <summary> Provides method overloads for the <see cref="IFileDialogService"/> to simplify its usage. </summary>
    public static class FileDialogServiceExtensions
    {
        /// <summary> Shows the open file dialog box that allows a user to specify a file that should be opened. </summary>
        /// <param name="_service">The file dialog service.</param>
        /// <param name="_fileType">The supported file type.</param>
        /// <returns>A FileDialogResult object which contains the filename selected by the user.</returns>
        /// <exception cref="ArgumentNullException">service must not be null.</exception>
        /// <exception cref="ArgumentNullException">fileType must not be null.</exception>
        public static FileDialogResult ShowOpenFileDialog(this IFileDialogService _service, FileType _fileType)
        {
            if (null == _service)
                throw new ArgumentNullException(nameof(_service));
            if (null == _fileType)
                throw new ArgumentNullException(nameof(_fileType));

            return _service.ShowOpenFileDialog(null, new FileType[] { _fileType }, _fileType, null);
        }

        /// <summary> Shows the open file dialog box that allows a user to specify a file that should be opened. </summary>
        /// <param name="_service">The file dialog service.</param>
        /// <param name="owner">The window that owns this OpenFileDialog.</param>
        /// <param name="_fileType">The supported file type.</param>
        /// <returns>A FileDialogResult object which contains the filename selected by the user.</returns>
        /// <exception cref="ArgumentNullException">service must not be null.</exception>
        /// <exception cref="ArgumentNullException">fileType must not be null.</exception>
        public static FileDialogResult ShowOpenFileDialog(this IFileDialogService _service, object owner, FileType _fileType)
        {
            if (null == _service)
                throw new ArgumentNullException(nameof(_service));
            if (null == _fileType)
                throw new ArgumentNullException(nameof(_fileType));

            return _service.ShowOpenFileDialog(owner, new FileType[] { _fileType }, _fileType, null);
        }

        /// <summary> Shows the open file dialog box that allows a user to specify a file that should be opened. </summary>
        /// <param name="_service">The file dialog service.</param>
        /// <param name="_fileType">The supported file type.</param>
        /// <param name="_defaultFileName">Default filename. The directory name is used as initial directory when it is specified.</param>
        /// <returns>A FileDialogResult object which contains the filename selected by the user.</returns>
        /// <exception cref="ArgumentNullException">service must not be null.</exception>
        /// <exception cref="ArgumentNullException">fileType must not be null.</exception>
        public static FileDialogResult ShowOpenFileDialog(this IFileDialogService _service, FileType _fileType, string _defaultFileName)
        {
            if (null == _service)
                throw new ArgumentNullException(nameof(_service));
            if (null == _fileType)
                throw new ArgumentNullException(nameof(_fileType));

            return _service.ShowOpenFileDialog(null, new FileType[] { _fileType }, _fileType, _defaultFileName);
        }

        /// <summary> Shows the open file dialog box that allows a user to specify a file that should be opened. </summary>
        /// <param name="_service">The file dialog service.</param>
        /// <param name="_owner">The window that owns this OpenFileDialog.</param>
        /// <param name="_fileType">The supported file type.</param>
        /// <param name="_defaultFileName">Default filename. The directory name is used as initial directory when it is specified.</param>
        /// <returns>A FileDialogResult object which contains the filename selected by the user.</returns>
        /// <exception cref="ArgumentNullException">service must not be null.</exception>
        /// <exception cref="ArgumentNullException">fileType must not be null.</exception>
        public static FileDialogResult ShowOpenFileDialog(this IFileDialogService _service, object _owner, FileType _fileType, string _defaultFileName)
        {
            if (null == _service)
                throw new ArgumentNullException(nameof(_service));
            if (null == _fileType)
                throw new ArgumentNullException(nameof(_fileType));

            return _service.ShowOpenFileDialog(_owner, new FileType[] { _fileType }, _fileType, _defaultFileName);
        }

        /// <summary> Shows the open file dialog box that allows a user to specify a file that should be opened. </summary>
        /// <param name="_service">The file dialog service.</param>
        /// <param name="_fileTypes">The supported file types.</param>
        /// <returns>A FileDialogResult object which contains the filename selected by the user.</returns>
        /// <exception cref="ArgumentNullException">service must not be null.</exception>
        /// <exception cref="ArgumentNullException">fileTypes must not be null.</exception>
        /// <exception cref="ArgumentException">fileTypes must contain at least one item.</exception>
        public static FileDialogResult ShowOpenFileDialog(this IFileDialogService _service, IEnumerable<FileType> _fileTypes)
        {
            if (null == _service)
                throw new ArgumentNullException(nameof(_service));

            return _service.ShowOpenFileDialog(null, _fileTypes, null, null);
        }

        /// <summary> Shows the open file dialog box that allows a user to specify a file that should be opened. </summary>
        /// <param name="_service">The file dialog service.</param>
        /// <param name="_owner">The window that owns this OpenFileDialog.</param>
        /// <param name="_fileTypes">The supported file types.</param>
        /// <returns>A FileDialogResult object which contains the filename selected by the user.</returns>
        /// <exception cref="ArgumentNullException">service must not be null.</exception>
        /// <exception cref="ArgumentNullException">fileTypes must not be null.</exception>
        /// <exception cref="ArgumentException">fileTypes must contain at least one item.</exception>
        public static FileDialogResult ShowOpenFileDialog(this IFileDialogService _service, object _owner, IEnumerable<FileType> _fileTypes)
        {
            if (null == _service)
                throw new ArgumentNullException(nameof(_service));

            return _service.ShowOpenFileDialog(_owner, _fileTypes, null, null);
        }

        /// <summary> Shows the open file dialog box that allows a user to specify a file that should be opened. </summary>
        /// <param name="_service">The file dialog service.</param>
        /// <param name="_fileTypes">The supported file types.</param>
        /// <param name="_defaultFileType">Default file type.</param>
        /// <param name="_defaultFileName">Default filename. The directory name is used as initial directory when it is specified.</param>
        /// <returns>A FileDialogResult object which contains the filename selected by the user.</returns>
        /// <exception cref="ArgumentNullException">service must not be null.</exception>
        /// <exception cref="ArgumentNullException">fileTypes must not be null.</exception>
        /// <exception cref="ArgumentException">fileTypes must contain at least one item.</exception>
        public static FileDialogResult ShowOpenFileDialog(this IFileDialogService _service, IEnumerable<FileType> _fileTypes
            , FileType _defaultFileType, string _defaultFileName)
        {
            if (null == _service) { throw new ArgumentNullException(nameof(_service)); }
            return _service.ShowOpenFileDialog(null, _fileTypes, _defaultFileType, _defaultFileName);
        }

        /// <summary> Shows the save file dialog box that allows a user to specify a filename to save a file as. </summary>
        /// <param name="_service">The file dialog service.</param>
        /// <param name="_fileType">The supported file type.</param>
        /// <returns>A FileDialogResult object which contains the filename entered by the user.</returns>
        /// <exception cref="ArgumentNullException">service must not be null.</exception>
        /// <exception cref="ArgumentNullException">fileType must not be null.</exception>
        public static FileDialogResult ShowSaveFileDialog(this IFileDialogService _service, FileType _fileType)
        {
            if (null == _service)
                throw new ArgumentNullException(nameof(_service));
            if (null == _fileType)
                throw new ArgumentNullException(nameof(_fileType));

            return _service.ShowSaveFileDialog(null, new FileType[] { _fileType }, _fileType, null);
        }

        /// <summary> Shows the save file dialog box that allows a user to specify a filename to save a file as. </summary>
        /// <param name="_service">The file dialog service.</param>
        /// <param name="_owner">The window that owns this SaveFileDialog.</param>
        /// <param name="_fileType">The supported file type.</param>
        /// <returns>A FileDialogResult object which contains the filename entered by the user.</returns>
        /// <exception cref="ArgumentNullException">service must not be null.</exception>
        /// <exception cref="ArgumentNullException">fileType must not be null.</exception>
        public static FileDialogResult ShowSaveFileDialog(this IFileDialogService _service, object _owner, FileType _fileType)
        {
            if (null == _service)
                throw new ArgumentNullException(nameof(_service));
            if (null == _fileType)
                throw new ArgumentNullException(nameof(_fileType));

            return _service.ShowSaveFileDialog(_owner, new FileType[] { _fileType }, _fileType, null);
        }

        /// <summary> Shows the save file dialog box that allows a user to specify a filename to save a file as. </summary>
        /// <param name="_service">The file dialog service.</param>
        /// <param name="_fileType">The supported file type.</param>
        /// <param name="_defaultFileName">Default filename. The directory name is used as initial directory when it is specified.</param>
        /// <returns>A FileDialogResult object which contains the filename entered by the user.</returns>
        /// <exception cref="ArgumentNullException">service must not be null.</exception>
        /// <exception cref="ArgumentNullException">fileType must not be null.</exception>
        public static FileDialogResult ShowSaveFileDialog(this IFileDialogService _service, FileType _fileType, string _defaultFileName)
        {
            if (null == _service)
                throw new ArgumentNullException(nameof(_service));
            if (null == _fileType)
                throw new ArgumentNullException(nameof(_fileType));

            return _service.ShowSaveFileDialog(null, new FileType[] { _fileType }, _fileType, _defaultFileName);
        }

        /// <summary> Shows the save file dialog box that allows a user to specify a filename to save a file as. </summary>
        /// <param name="_service">The file dialog service.</param>
        /// <param name="_owner">The window that owns this SaveFileDialog.</param>
        /// <param name="_fileType">The supported file type.</param>
        /// <param name="_defaultFileName">Default filename. The directory name is used as initial directory when it is specified.</param>
        /// <returns>A FileDialogResult object which contains the filename entered by the user.</returns>
        /// <exception cref="ArgumentNullException">service must not be null.</exception>
        /// <exception cref="ArgumentNullException">fileType must not be null.</exception>
        public static FileDialogResult ShowSaveFileDialog(this IFileDialogService _service, object _owner, FileType _fileType, string _defaultFileName)
        {
            if (null == _service)
                throw new ArgumentNullException(nameof(_service));
            if (null == _fileType)
                throw new ArgumentNullException(nameof(_fileType));

            return _service.ShowSaveFileDialog(_owner, new FileType[] { _fileType }, _fileType, _defaultFileName);
        }

        /// <summary> Shows the save file dialog box that allows a user to specify a filename to save a file as. </summary>
        /// <param name="_service">The file dialog service.</param>
        /// <param name="_fileTypes">The supported file types.</param>
        /// <returns>A FileDialogResult object which contains the filename entered by the user.</returns>
        /// <exception cref="ArgumentNullException">service must not be null.</exception>
        /// <exception cref="ArgumentNullException">fileTypes must not be null.</exception>
        /// <exception cref="ArgumentException">fileTypes must contain at least one item.</exception>
        public static FileDialogResult ShowSaveFileDialog(this IFileDialogService _service, IEnumerable<FileType> _fileTypes)
        {
            if (null == _service)
                throw new ArgumentNullException(nameof(_service));

            return _service.ShowSaveFileDialog(null, _fileTypes, null, null);
        }

        /// <summary> Shows the save file dialog box that allows a user to specify a filename to save a file as. </summary>
        /// <param name="_service">The file dialog service.</param>
        /// <param name="_owner">The window that owns this SaveFileDialog.</param>
        /// <param name="_fileTypes">The supported file types.</param>
        /// <returns>A FileDialogResult object which contains the filename entered by the user.</returns>
        /// <exception cref="ArgumentNullException">service must not be null.</exception>
        /// <exception cref="ArgumentNullException">fileTypes must not be null.</exception>
        /// <exception cref="ArgumentException">fileTypes must contain at least one item.</exception>
        public static FileDialogResult ShowSaveFileDialog(this IFileDialogService _service, object _owner, IEnumerable<FileType> _fileTypes)
        {
            if (null == _service)
                throw new ArgumentNullException(nameof(_service));

            return _service.ShowSaveFileDialog(_owner, _fileTypes, null, null);
        }

        /// <summary> Shows the save file dialog box that allows a user to specify a filename to save a file as. </summary>
        /// <param name="_service">The file dialog service.</param>
        /// <param name="_fileTypes">The supported file types.</param>
        /// <param name="_defaultFileType">Default file type.</param>
        /// <param name="_defaultFileName">Default filename. The directory name is used as initial directory when it is specified.</param>
        /// <returns>A FileDialogResult object which contains the filename entered by the user.</returns>
        /// <exception cref="ArgumentNullException">service must not be null.</exception>
        /// <exception cref="ArgumentNullException">fileTypes must not be null.</exception>
        /// <exception cref="ArgumentException">fileTypes must contain at least one item.</exception>
        public static FileDialogResult ShowSaveFileDialog(this IFileDialogService _service, IEnumerable<FileType> _fileTypes
            , FileType _defaultFileType, string _defaultFileName)
        {
            if (null == _service)
                throw new ArgumentNullException(nameof(_service));

            return _service.ShowSaveFileDialog(null, _fileTypes, _defaultFileType, _defaultFileName);
        }
    }
}
