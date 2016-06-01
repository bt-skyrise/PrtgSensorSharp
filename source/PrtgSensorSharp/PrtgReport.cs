using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PrtgSensorSharp
{
    public interface IPrtgReport
    {
        XElement Serialize();
    }

    public static class PrtgReport
    {
        public static IPrtgReport Successful(PrtgText message, IEnumerable<PrtgResult> results) =>
            new PrtgSuccess(message, results);

        public static IPrtgReport Successful(IEnumerable<PrtgResult> results) =>
            new PrtgSuccess(PrtgText.Default, results);

        public static IPrtgReport Failed(PrtgText message) =>
            new PrtgFailure(message);
    }

    public class PrtgSuccess : IPrtgReport
    {
        // todo: channel names should be unique

        private readonly IPrtgText _text;
        private readonly IEnumerable<PrtgResult> _results;

        public PrtgSuccess(IPrtgText text, IEnumerable<PrtgResult> results)
        {
            _text = text;
            _results = results;
        }

        public XElement Serialize() => new XElement("prtg",
            _results.Select(result => result.Serialize()),
            _text.Serialize()
        );
    }

    public class PrtgFailure : IPrtgReport
    {
        private readonly IPrtgText _text;

        public PrtgFailure(IPrtgText text)
        {
            _text = text;
        }

        public XElement Serialize() => new XElement("prtg",
            new XElement("error", "1"),
            _text.Serialize());
    }
}