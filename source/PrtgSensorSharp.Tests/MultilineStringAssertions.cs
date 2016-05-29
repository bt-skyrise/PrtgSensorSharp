using System;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace PrtgSensorSharp.Tests
{
    public static class MultilineStringAssertions
    {
        public static AndConstraint<StringAssertions> BeLines(this StringAssertions stringAssertions, params string[] lines)
        {
            var expectedString = string.Join(Environment.NewLine, lines);

            return stringAssertions.Be(expectedString);
        }
    }
}