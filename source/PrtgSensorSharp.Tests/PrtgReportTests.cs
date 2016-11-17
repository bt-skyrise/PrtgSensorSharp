using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace PrtgSensorSharp.Tests
{
    public class PrtgReportTests
    {
        [Test]
        public void can_create_report_with_multiple_channels()
        {
            var report = PrtgReport.Successful(new[]
            {
                new PrtgResult("First channel", 10),
                new PrtgResult("Second channel", 1.5f)
            });

            report.Serialize().Should().BeEquivalentTo(new XElement("prtg",
                new XElement("result",
                    new XElement("channel", "First channel"),
                    new XElement("value", "10")
                ),
                new XElement("result",
                    new XElement("channel", "Second channel"),
                    new XElement("value", "1.5")
                )
            ));
        }

        [Test]
        public void report_can_have_additional_message()
        {
            var report = PrtgReport.Successful("it works", new[]
            {
                new PrtgResult("some channel", 10)
            });

            report.Serialize()
                .Should().HaveElement("text")
                .Which.Should().HaveValue("it works");
        }


        [Test]
        public void report_must_have_unique_channels()
        {
            var report = PrtgReport.Successful(new[]
            {
                new PrtgResult("First channel", 10),
                new PrtgResult("First channel", 1.5f),
                new PrtgResult("Second channel", 1)
            });

            report.Serialize().Should().BeEquivalentTo(new XElement("prtg",
                new XElement("error", "1"),
                new XElement("text", "Duplicate channels: First channel")
            ));
        }

        [Test]
        public void report_can_handle_empty_PrtgResults()
        {
            var report = PrtgReport.Successful(new List<PrtgResult>());

            report.Serialize()
                .Should().HaveElement("text")
                .Which.Should().HaveValue("No channels");
        }

        [Test]
        public void can_create_failed_report()
        {
            var report = PrtgReport.Failed("Your error message");

            report.Serialize().Should().BeEquivalentTo(new XElement("prtg",
                new XElement("error", "1"),
                new XElement("text", "Your error message")
            ));
        }
    }
}