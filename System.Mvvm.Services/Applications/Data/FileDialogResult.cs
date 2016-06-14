namespace System.Mvvm.Services
{
    /// <summary> Contains the result information about the work with the file dialog box. </summary>
    public class FileDialogResult
    {
        readonly string m_FileName;
        readonly FileType m_SelectedFileType;


        /// <summary> Initializes a new instance of the <see cref="FileDialogResult"/> class with null values.
        /// Use this constructor when the user canceled the file dialog box.
        /// </summary>
        public FileDialogResult() : this(null, null)
        {
        }

        /// <summary> Initializes a new instance of the <see cref="FileDialogResult"/> class. </summary>
        /// <param name="_fileName">The filename entered by the user.</param>
        /// <param name="_selectedFileType">The file type selected by the user.</param>
        public FileDialogResult(string _fileName, FileType _selectedFileType)
        {
            m_FileName = _fileName;
            m_SelectedFileType = _selectedFileType;
        }


        /// <summary> Gets a value indicating whether this instance contains valid data. This property returns <c>false</c>
        ///     when the user canceled the file dialog box.
        /// </summary>
        public bool IsValid => null != this.FileName && null != this.SelectedFileType;

        /// <summary> Gets the filename entered by the user or <c>null</c> when the user canceled the dialog box. </summary>
        public string FileName => m_FileName;

        /// <summary> Gets the file type selected by the user or <c>null</c> when the user canceled the dialog box. </summary>
        public FileType SelectedFileType => m_SelectedFileType;
    }
}
