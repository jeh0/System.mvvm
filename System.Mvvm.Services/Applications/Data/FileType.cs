using System.Collections.Generic;

namespace System.Mvvm.Services
{
    /// <summary> Represents a file type. </summary>
    public class FileType
    {
        readonly string m_Description;
        readonly string m_FileExtension;


        /// <summary> Initializes a new instance of the <see cref="FileType"/> class. </summary>
        /// <param name="_description">The description of the file type.</param>
        /// <param name="_fileExtension">The file extension. This string has to start with a '.' point. Use the string ".*" to allow all file extensions.</param>
        /// <exception cref="ArgumentException">description is null or an empty string.</exception>
        /// <exception cref="ArgumentException">fileExtension is null, an empty string or doesn't start with a '.' point character.</exception>
        public FileType(string _description, string _fileExtension)
        {
            if (string.IsNullOrEmpty(_description)) throw new ArgumentException("The argument description must not be null or empty."); 
            if (string.IsNullOrEmpty(_fileExtension)) throw new ArgumentException("The argument fileExtension must not be null or empty."); 
            if ('.' != _fileExtension[0]) throw new ArgumentException("The argument fileExtension must start with the '.' character.");

            m_Description = _description;
            m_FileExtension = _fileExtension;
        }

        /// <summary> Initializes a new instance of the <see cref="FileType"/> class. </summary>
        /// <param name="_description">The description of the file type.</param>
        /// <param name="_fileExtensions">A list of file extensions. Every string has to start with a '.' point.</param>
        /// <exception cref="ArgumentException">description is null or an empty string.</exception>
        /// <exception cref="ArgumentException">One or more of the file extension strings doesn't start with a '.' point character.</exception>
        /// <exception cref="ArgumentNullException">fileExtensions is null.</exception>
        public FileType(string _description, IEnumerable<string> _fileExtensions)
            : this(_description, string.Join(";*", CheckFileExtensions(_fileExtensions)))
        {
        }

        /// <summary> Gets the description of the file type. </summary>
        public string Description =>  m_Description;

        /// <summary> Gets the file extension. This string starts with a '.' point. Multiple file extensions are concatenated with the string ";*" as separator. </summary>
        public string FileExtension => m_FileExtension;

        static IEnumerable<string> CheckFileExtensions(IEnumerable<string> _fileExtensions)
        {
            if (null == _fileExtensions) { throw new ArgumentNullException(nameof(_fileExtensions)); }

            foreach (string _fileExtension in _fileExtensions)
            {
                if (string.IsNullOrEmpty(_fileExtension) || '.' != _fileExtension[0])
                    throw new ArgumentException("The argument fileExtension must start with the '.' character.");
            }
            return _fileExtensions;
        }
    }
}
