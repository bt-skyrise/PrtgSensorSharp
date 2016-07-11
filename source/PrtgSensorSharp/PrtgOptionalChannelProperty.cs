using System;
using System.Xml.Linq;

// ReSharper disable InconsistentNaming - naming should be closer to PRTG domain

namespace PrtgSensorSharp
{
    public class PrtgOptionalChannelProperty
    {
        private readonly string _name;
        private readonly string _value;

        public static implicit operator PrtgOptionalChannelProperty(Enum @enum)
        {
            // todo: a different implementation? Like EnumChannelProperty : IChannelProperty?

            return new PrtgOptionalChannelProperty(
                @enum.TypeName(),
                @enum.ValueName());
        }

        // these are for static imports

        public static PrtgOptionalChannelProperty CustomUnit(string customUnit) =>
            new PrtgOptionalChannelProperty("CustomUnit", customUnit);

        public static PrtgOptionalChannelProperty Float(bool value) =>
            YesNoProperty("Float", value);

        private static PrtgOptionalChannelProperty YesNoProperty(string name, bool value) =>
            new PrtgOptionalChannelProperty(name, value ? "1" : "0");

        // builder + static import

        public static YesNoProperty Warning => new YesNoProperty("Warning");

        public PrtgOptionalChannelProperty(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public XElement Serialize() => new XElement(_name, _value);
    }

    public enum Unit
    {
        BytesBandwidth,
        BytesMemory,
        BytesDisk,
        Temperature,
        Percent,
        TimeResponse,
        TimeSeconds,
        Custom,
        Count,
        CPU,
        BytesFile,
        SpeedDisk,
        SpeedNet,
        TimeHours
    }

    public static class PrtgCustomUnit
    {
        public static PrtgOptionalChannelProperty Named(string customUnit) =>
            new PrtgOptionalChannelProperty("CustomUnit", customUnit);
    }

    public enum SpeedSize
    {
        One,
        Kilo,
        Mega,
        Giga,
        Tera,
        Byte,
        KiloByte,
        MegaByte,
        GigaByte,
        TeraByte,
        Bit,
        KiloBit,
        MegaBit,
        GigaBit,
        TeraBit
    }

    public enum VolumeSize
    {
        One,
        Kilo,
        Mega,
        Giga,
        Tera,
        Byte,
        KiloByte,
        MegaByte,
        GigaByte,
        TeraByte,
        Bit,
        KiloBit,
        MegaBit,
        GigaBit,
        TeraBit
    }

    public enum SpeedTime
    {
        Second,
        Minute,
        Hour,
        Day
    }

    public enum Mode
    {
        Absolute,
        Difference
    }

    public static class Float
    {
        public static PrtgOptionalChannelProperty Yes => new PrtgOptionalChannelProperty("Float", "1");
        public static PrtgOptionalChannelProperty No => new PrtgOptionalChannelProperty("Float", "0");
    }

    public enum DecimalMode
    {
        Auto,
        All
    }

    public static class Warning
    {
        public static PrtgOptionalChannelProperty Yes => new PrtgOptionalChannelProperty("Warning", "1");
        public static PrtgOptionalChannelProperty No => new PrtgOptionalChannelProperty("Warning", "0");
    }

    // ShowChart - yes/no

    // ShowTable - yes/no

    public static class LimitMaxError
    {
        // ???
    }

    public static class Warning_Builder
    {
        public static PrtgOptionalChannelProperty Yes => Property.Yes;
        public static PrtgOptionalChannelProperty No => Property.No;

        private static YesNoProperty Property => new YesNoProperty("Warning");
    }

    public class YesNoProperty
    {
        private readonly string _name;

        public YesNoProperty(string name)
        {
            _name = name;
        }

        public PrtgOptionalChannelProperty Yes => WithValue("1");
        public PrtgOptionalChannelProperty No => WithValue("0");

        private PrtgOptionalChannelProperty WithValue(string value) =>
            new PrtgOptionalChannelProperty(_name, value);
    }
}