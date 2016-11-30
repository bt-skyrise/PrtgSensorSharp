using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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

        public static IPrtgReport Successful(IEnumerable<PrtgResult> results)
        {
            var resultsList = results.ToList();

            var successMessage = resultsList.Any()
                ? PrtgText.Default
                : new PrtgText("No channels");

            return new PrtgSuccess(successMessage, resultsList);
        }

        public static IPrtgReport Failed(PrtgText message) =>
            new PrtgFailure(message);
    }

    public class PrtgSuccess : IPrtgReport
    {
        private readonly IPrtgText _text;
        private readonly IEnumerable<PrtgResult> _results;

        public PrtgSuccess(IPrtgText text, IEnumerable<PrtgResult> results)
        {
            _text = text;
            _results = results;
        }

        public XElement Serialize()
        {
            var duplicates = GetDuplicateChannels();

            if (duplicates.Any())
            {
                var joinedDuplicates = string.Join(", ", duplicates);

                return PrtgReport
                    .Failed(new PrtgText($"Duplicate channels: {joinedDuplicates}"))
                    .Serialize();
            }

            return new XElement("prtg",
                _results.Select(result => result.Serialize()),
                _text.Serialize()
            );
        }

        private List<string> GetDuplicateChannels() => _results
            .GroupBy(result => result.ChannelName)
            .Where(resultGroup => resultGroup.Count() > 1)
            .Select(resultGroup => resultGroup.Key)
            .ToList();
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