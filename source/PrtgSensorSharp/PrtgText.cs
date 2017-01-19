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

        public static IPrtgText Default => new PrtgDefaultText();

        public string Message { get; }

        public PrtgText(string message)
        {
            Message = message;
        }

        public XElement Serialize() => new XElement("text", ShortenedMessage);

        private const int MaximumLength = 2000;

        private string ShortenedMessage => Message.Length > MaximumLength
            ? Message.Substring(0, MaximumLength - 3) + "..."
            : Message;
    }

    public class PrtgDefaultText : IPrtgText
    {
        public XElement Serialize() => null;
    }
}