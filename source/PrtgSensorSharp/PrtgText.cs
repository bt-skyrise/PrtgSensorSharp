using System.Xml.Linq;

namespace PrtgSensorSharp
{
    public interface IPrtgText
    {
        XElement Serialize();
    }

    public class PrtgText : IPrtgText
    {
        public static implicit operator PrtgText(string message) => new PrtgText(message);

        public static IPrtgText None => new PrtgDefaultText();

        private readonly string _message;

        public PrtgText(string message)
        {
            _message = message;
        }

        public XElement Serialize() => new XElement("text", ShortenedMessage);

        private const int MaximumLength = 2000;

        private string ShortenedMessage => _message.Length > MaximumLength
            ? _message.Substring(0, MaximumLength - 3) + "..."
            : _message;
    }

    public class PrtgDefaultText : IPrtgText
    {
        public XElement Serialize() => null;
    }
}