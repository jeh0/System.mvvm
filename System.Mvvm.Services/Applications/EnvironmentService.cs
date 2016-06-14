using System.ComponentModel.Composition;
using System.IO;

namespace System.Mvvm.Services
{
    [Export(typeof(IEnvironmentService))]
    public class EnvironmentService : IEnvironmentService
    {
        private readonly Lazy<string> m_AppSettingsPath;
        
        public EnvironmentService()
        {
            m_AppSettingsPath = new Lazy<string>(() =>
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ApplicationInfo.Company, ApplicationInfo.ProductName, "Settings"));
        }

        public string AppSettingsPath => m_AppSettingsPath.Value;
    }
}