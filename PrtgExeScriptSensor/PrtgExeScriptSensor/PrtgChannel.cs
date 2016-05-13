using System.Xml.Linq;

namespace PrtgExeScriptSensor
{
    public class PrtgChannel
    {
        private readonly string _channelName;

        public PrtgChannel(string channelName)
        {
            _channelName = channelName;
        }

        public XElement Serialize() => new XElement("channel", _channelName);

        public static implicit operator PrtgChannel(string channelName) => new PrtgChannel(channelName);
    }
}