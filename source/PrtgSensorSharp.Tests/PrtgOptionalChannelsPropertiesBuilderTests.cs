using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace PrtgSensorSharp.Tests
{
    public class PrtgOptionalChannelsPropertiesBuilderTests
    {
        [Test]
        public void can_build_optional_properties()
        {
            var sut = new PrtgOptionalChannelPropertiesBuilder()
                .WithLimitMaxWarning(10)
                .WithLimitMaxError(100.1M)
                .WithLimitMinError(-99.991M)
                .WithLimitMinWarning(0)
                .WithLimitsEnabled();
                
            var result = sut.Build();

            result.Length.Should().Be(5);

            result.Should().Contain(p => p.Name == "LimitMode" && p.Value == "1");
            result.Should().Contain(p => p.Name == "LimitMaxError" && p.Value == "100.1");
            result.Should().Contain(p => p.Name == "LimitMaxWarning" && p.Value == "10");
            result.Should().Contain(p => p.Name == "LimitMinError" && p.Value == "-99.991");
            result.Should().Contain(p => p.Name == "LimitMinWarning" && p.Value == "0");

        }

        [Test]
        public void can_create_LimitMode_property()
        {
            var sut = new PrtgOptionalChannelPropertiesBuilder()
                .WithLimitsEnabled();

            var result = new PrtgResult(channel: "TestChannel", value: 1, optionalProperties: sut.Build());

            result.Serialize().ToString().Should().Contain("<LimitMode>1</LimitMode>");
        }
        [Test]
        public void can_create_LimitMaxError_property()
        {
            var sut = new PrtgOptionalChannelPropertiesBuilder()
                .WithLimitMaxError(100.923m);

            var result = new PrtgResult(channel: "TestChannel", value: 1, optionalProperties: sut.Build());

            result.Serialize().ToString().Should().Contain("<LimitMaxError>100.923</LimitMaxError>");
        }

        [Test]
        public void can_create_LimitMaxWarning_property()
        {
            var sut = new PrtgOptionalChannelPropertiesBuilder()
                .WithLimitMaxWarning(10);

            var result = new PrtgResult(channel: "TestChannel", value: 1, optionalProperties: sut.Build());

            result.Serialize().ToString().Should().Contain("<LimitMaxWarning>10</LimitMaxWarning>");
        }

        [Test]
        public void can_create_LimitMinError_property()
        {
            var sut = new PrtgOptionalChannelPropertiesBuilder()
                .WithLimitMinError(-100.234m);

            var result = new PrtgResult(channel: "TestChannel", value: 1, optionalProperties: sut.Build());

            result.Serialize().ToString().Should().Contain("<LimitMinError>-100.234</LimitMinError>");
        }

        [Test]
        public void can_create_LimitMinWarning_property()
        {
            var sut = new PrtgOptionalChannelPropertiesBuilder()
                .WithLimitMinWarning(999999999999999999999999.9999m);

            var result = new PrtgResult(channel: "TestChannel", value: 1, optionalProperties: sut.Build());

            result.Serialize().ToString().Should().Contain("<LimitMinWarning>999999999999999999999999.9999</LimitMinWarning>");
        }
    }
}
