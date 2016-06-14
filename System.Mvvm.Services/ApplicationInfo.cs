using System.IO;
using System.Reflection;

namespace System.Mvvm
{
    /// <summary> This class provides information about the running application. </summary>
    public static class ApplicationInfo
    {
        public static Assembly AssemblyEx => Assembly.GetEntryAssembly();

        /// <summary> Gets the product name of the application. </summary>
        public static string ProductName => m_ProductName.Value;
        static readonly Lazy<string> m_ProductName = new Lazy<string>(() => ((AssemblyProductAttribute)Attribute.GetCustomAttribute(AssemblyEx, typeof(AssemblyProductAttribute)))?.Product);

        /// <summary> Gets the version number of the application. </summary>
        public static string Version => m_Version.Value;
        static readonly Lazy<string> m_Version = new Lazy<string>(() => AssemblyEx?.GetName()?.Version.ToString());

        /// <summary> Gets the company of the application. </summary>
        public static string Company => m_Company.Value;
        static readonly Lazy<string> m_Company = new Lazy<string>(() => ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(AssemblyEx, typeof(AssemblyCompanyAttribute)))?.Company);

        /// <summary> Gets the copyright information of the application. </summary>
        public static string Copyright => m_Copyright.Value;
        static readonly Lazy<string> m_Copyright = new Lazy<string>(() => ((AssemblyCopyrightAttribute)Attribute.GetCustomAttribute(AssemblyEx, typeof(AssemblyCopyrightAttribute)))?.Copyright);

        /// <summary> Gets the path for the executable file that started the application, not including the executable name. </summary>
        public static string ApplicationPath => m_ApplicationPath.Value;
        static readonly Lazy<string> m_ApplicationPath = new Lazy<string>(()=> Path.GetDirectoryName(AssemblyEx?.Location));
    }
}
