using System.Xml.Linq;

namespace PrtgSensorSharp
{
    public class PrtgOptionalChannelProperty
    {
        private readonly string _name;
        private readonly string _value;

        public PrtgOptionalChannelProperty(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public XElement Serialize() => new XElement(_name, _value);
    }
}