using System.Xml.Linq;
using FluentAssertions;
using NUnit.Framework;

// ReSharper disable UnusedMember.Local

namespace PrtgSensorSharp.Tests
{
    public class PrtgOptionalChannelPropertyTests
    {
        private enum SomeUnitName { SomeValue, OtherValue, YetAnotherValue }
        
        [Test]
        public void can_create_custom_unit_out_of_enum()
        {
            var customUnit = (PrtgOptionalChannelProperty) SomeUnitName.OtherValue;

            customUnit.Serialize().Should().Be(new XElement("SomeUnitName", "OtherValue"));
        }
    }
}