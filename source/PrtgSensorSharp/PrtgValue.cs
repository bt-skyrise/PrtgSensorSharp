using System.Globalization;
using System.Xml.Linq;

namespace PrtgSensorSharp
{
    public class PrtgValue
    {
        public static implicit operator PrtgValue(int value) => Integer(value);

        public static PrtgValue Integer(int value) => new PrtgValue(
            value.ToString(CultureInfo.InvariantCulture));

        public static implicit operator PrtgValue(float value) => Float(value);

        public static PrtgValue Float(float value) => new PrtgValue(
            value.ToString(CultureInfo.InvariantCulture));

        public string Value { get; }

        private PrtgValue(string value)
        {
            Value = value;
        }

        public XElement Serialize() => new XElement("value", Value);
    }
}