using FluentAssertions;
using NUnit.Framework;

namespace PrtgExeScriptSensor.Tests
{
    public class PrtgResultTests
    {
        [Test]
        public void result_can_include_some_optional_properties()
        {
            var result = new PrtgResult("a channel", 10,
                new PrtgOptionalChannelProperty("some-property", "some-value"),
                new PrtgOptionalChannelProperty("other-property", "other-value"));

            var serializedResult = result.Serialize();

            serializedResult
                .Should().HaveElement("some-property")
                .Which.Should().HaveValue("some-value");

            serializedResult
                .Should().HaveElement("other-property")
                .Which.Should().HaveValue("other-value");
        }
    }
}