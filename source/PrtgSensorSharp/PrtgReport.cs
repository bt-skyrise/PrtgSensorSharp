using System.Collections.Generic;
using System.Linq;
//using System.Security.Policy;
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
        public IPrtgText Text { get; }
        public IEnumerable<PrtgResult> Results { get; }

        public PrtgSuccess(IPrtgText text, IEnumerable<PrtgResult> results)
        {
            Text = text;
            Results = results;
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
                Results.Select(result => result.Serialize()),
                Text.Serialize()
            );
        }

        private List<string> GetDuplicateChannels() => Results
            .GroupBy(result => result.ChannelName)
            .Where(resultGroup => resultGroup.Count() > 1)
            .Select(resultGroup => resultGroup.Key)
            .ToList();
    }

    public class PrtgFailure : IPrtgReport
    {
        public IPrtgText Text { get; }

        public PrtgFailure(IPrtgText text)
        {
            Text = text;
        }

        public XElement Serialize() => new XElement("prtg",
            new XElement("error", "1"),
            Text.Serialize());
    }
}