using System.Runtime.Serialization;

namespace System.Mvvm.Services.Properties
{
    [DataContract]
    public sealed class AppSettings : IExtensibleDataObject
    {
        public const string c_AppSettingsFileName = "Settings.xml";

        public AppSettings() { }
        
        [DataMember] public double Left { get; set; }
        [DataMember] public double Top { get; set; }
        [DataMember] public double Height { get; set; }
        [DataMember] public double Width { get; set; }
        [DataMember] public bool IsMaximized { get; set; }
        [DataMember] public string CurrentPath { get; set; }

        ExtensionDataObject IExtensibleDataObject.ExtensionData { get; set; }

        [OnDeserializing] private void OnDeserializing(StreamingContext context) { }
    }
}
