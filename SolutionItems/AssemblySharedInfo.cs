using System;
using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following set of attributes. 
// Change these attribute values to modify the information associated with an assembly. 

[assembly: AssemblyProduct("System.Mvvm")]
[assembly: AssemblyDescription("System.Mvvm")]
[assembly: AssemblyConfiguration("net-4.6.win32; alpha")]
[assembly: AssemblyCompany("ekut")]
[assembly: AssemblyCopyright("Copyright © ekut 2016")]
[assembly: AssemblyTrademark("idinahu@ya.ru")]

// [assembly: AssemblyCulture("")]
// [assembly: NeutralResourcesLanguage("ru-RU", UltimateResourceFallbackLocation.MainAssembly)]

[assembly: CLSCompliant(false)]  // The WinRT API is not CLS-compliant (e.g. usage of uint)
#if !PORTABLE 
[assembly: ComVisible(false)]
#endif

#if NET4 || PORTABLE
namespace System.Reflection
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    internal sealed class AssemblyMetadataAttribute : Attribute
    {
        public AssemblyMetadataAttribute(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public string Value { get; set; }
    }
}
#endif