using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace PrtgSensorSharp.Tests
{
    public class PrtgTextTests
    {
        [Test]
        public void message_should_be_shortened_when_too_long()
        {
            var veryLongMessage = string.Concat(
                Enumerable.Repeat("x", 2005));

            var prtgText = new PrtgText(veryLongMessage);

            prtgText.Serialize().Value
                .Should().EndWith("xxx...")
                .And.HaveLength(2000);
        }
    }
}