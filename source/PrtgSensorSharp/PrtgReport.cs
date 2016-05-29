using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PrtgSensorSharp
{
    public interface IPrtgReport
    {
        XElement Serialize();
    }

    public class PrtgReport
    {
        public static IPrtgReport Successful(PrtgText message, IEnumerable<PrtgResult> results) =>
            new PrtgSuccessfulReport(message, results);

        public static IPrtgReport Successful(IEnumerable<PrtgResult> results) =>
            new PrtgSuccessfulReport(PrtgText.None, results);

        public static IPrtgReport Failed(PrtgText message) =>
            new PrtgFailedReport(message);
    }

    public class PrtgSuccessfulReport : IPrtgReport
    {
        private readonly IPrtgText _text;
        private readonly IEnumerable<PrtgResult> _results;

        public PrtgSuccessfulReport(IPrtgText text, IEnumerable<PrtgResult> results)
        {
            _text = text;
            _results = results;
        }

        public XElement Serialize() => new XElement("prtg",
            _results.Select(result => result.Serialize()),
            _text.Serialize()
        );
    }

    public class PrtgFailedReport : IPrtgReport
    {
        private readonly IPrtgText _text;

        public PrtgFailedReport(IPrtgText text)
        {
            _text = text;
        }

        public XElement Serialize() => new XElement("prtg",
            new XElement("error", "1"),
            _text.Serialize());
    }
}