using System.Linq;
using System.Xml.Linq;

namespace PrtgSensorSharp
{
    public class PrtgResult
    {
        private readonly string _channelName;
        private readonly PrtgValue _value;
        private readonly PrtgOptionalChannelProperty[] _optionalProperties;

        // todo: optional properties should be unique
        
        public PrtgResult(string channel, PrtgValue value, params PrtgOptionalChannelProperty[] optionalProperties)
        {
            _channelName = channel;
            _value = value;
            _optionalProperties = optionalProperties;
        }

        public XElement Serialize() => new XElement("result",
            new XElement("channel", _channelName),
            _value.Serialize(),
            _optionalProperties.Select(property => property.Serialize()));
    }
}