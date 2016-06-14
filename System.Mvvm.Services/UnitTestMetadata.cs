using System.ComponentModel.Composition.Primitives;

namespace System.Mvvm.Services
{
    public static class UnitTestMetadata
    {
        public const string c_Name = "UnitTest", c_Data = "Data";
        
        public static bool IsContained(ComposablePartDefinition _definition, object _value = null)
            => null == _value ? _definition.Metadata.ContainsKey(c_Name)
            : _definition.Metadata.ContainsKey(c_Name) && _definition.Metadata[c_Name].Equals(_value);
    }
}