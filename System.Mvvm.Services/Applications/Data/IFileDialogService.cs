using System.Collections.Generic;

namespace System.Mvvm.Services
{ 
    /// <summary> This service allows a user to specify a filename to open or save a file. </summary>
    /// <remarks> This interface is designed for simplicity. If you have to accomplish more advanced
    /// scenarios then we recommend implementing your own specific file dialog service.
    /// </remarks>
    public interface IFileDialogService
    {
        /// <summary> Shows the open file dialog box that allows a user to specify a file that should be opened. </summary>
        /// <param name="_owner">The window that owns this OpenFileDialog.</param>
        /// <param name="_fileTypes">The supported file types.</param>
        /// <param name="_defaultFileType">Default file type.</param>
        /// <param name="_defaultFileName">Default filename. The directory name is used as initial directory when it is specified.</param>
        /// <returns>A FileDialogResult object which contains the filename selected by the user.</returns>
        /// <exception cref="ArgumentNullException">fileTypes must not be null.</exception>
        /// <exception cref="ArgumentException">fileTypes must contain at least one item.</exception>
        FileDialogResult ShowOpenFileDialog(object _owner, IEnumerable<FileType> _fileTypes, FileType _defaultFileType, string _defaultFileName);

        /// <summary> Shows the save file dialog box that allows a user to specify a filename to save a file as. </summary>
        /// <param name="_owner">The window that owns this SaveFileDialog.</param>
        /// <param name="_fileTypes">The supported file types.</param>
        /// <param name="_defaultFileType">Default file type.</param>
        /// <param name="_defaultFileName">Default filename. The directory name is used as initial directory when it is specified.</param>
        /// <returns>A FileDialogResult object which contains the filename entered by the user.</returns>
        /// <exception cref="ArgumentNullException">fileTypes must not be null.</exception>
        /// <exception cref="ArgumentException">fileTypes must contain at least one item.</exception>
        FileDialogResult ShowSaveFileDialog(object _owner, IEnumerable<FileType> _fileTypes, FileType _defaultFileType, string _defaultFileName);
    }
}
