using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PrtgSensorSharp
{
    public class PrtgResult
    {
        public string ChannelName { get; }
        public PrtgValue Value { get; }
        public IEnumerable<PrtgOptionalChannelProperty> OptionalProperties { get; }

        public PrtgResult(string channel, PrtgValue value, params PrtgOptionalChannelProperty[] optionalProperties)
        {
            ChannelName = channel;
            Value = value;
            OptionalProperties = optionalProperties;
        }

        public XElement Serialize() => new XElement("result",
            new XElement("channel", ChannelName),
            Value.Serialize(),
            OptionalProperties.Select(property => property.Serialize()));
    }
}