using System.Xml.Linq;

namespace PrtgSensorSharp
{
    public class PrtgOptionalChannelProperty
    {
        public string Name { get; }
        public string Value { get; }

        public PrtgOptionalChannelProperty(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public XElement Serialize() => new XElement(Name, Value);
    }
}