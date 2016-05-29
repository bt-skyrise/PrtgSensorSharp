using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PrtgSensorSharp
{
    public class PrtgResult
    {
        private readonly PrtgChannel _channel;
        private readonly PrtgValue _value;
        private readonly PrtgOptionalChannelProperty[] _optionalProperties;
        
        public PrtgResult(PrtgChannel channel, PrtgValue value,
            params PrtgOptionalChannelProperty[] optionalProperties)
        {
            _optionalProperties = optionalProperties;
            _channel = channel;
            _value = value;
        }

        public XElement Serialize() => new XElement("result",
            _channel.Serialize(),
            _value.Serialize(),
            SerializeOptionalProperties());

        private IEnumerable<XElement> SerializeOptionalProperties() => _optionalProperties
            .Select(property => property.Serialize());
    }
}