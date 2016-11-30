using System.Linq;
using System.Xml.Linq;

namespace PrtgSensorSharp
{
    public class PrtgResult
    {
        public string ChannelName { get; }

        private readonly PrtgValue _value;
        private readonly PrtgOptionalChannelProperty[] _optionalProperties;
        
        public PrtgResult(string channel, PrtgValue value, params PrtgOptionalChannelProperty[] optionalProperties)
        {
            ChannelName = channel;
            _value = value;
            _optionalProperties = optionalProperties;
        }

        public XElement Serialize() => new XElement("result",
            new XElement("channel", ChannelName),
            _value.Serialize(),
            _optionalProperties.Select(property => property.Serialize()));
    }
}