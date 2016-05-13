namespace PrtgExeScriptSensor
{
    public static class PrtgUnit
    {
        public static PrtgOptionalChannelProperty BytesBandwidth => Unit("BytesBandwidth");
        public static PrtgOptionalChannelProperty BytesMemory => Unit("BytesMemory");
        public static PrtgOptionalChannelProperty BytesDisk => Unit("BytesDisk");
        public static PrtgOptionalChannelProperty Temperature => Unit("Temperature");
        public static PrtgOptionalChannelProperty Percent => Unit("Percent");
        public static PrtgOptionalChannelProperty TimeResponse => Unit("TimeResponse");
        public static PrtgOptionalChannelProperty TimeSeconds => Unit("TimeSeconds");
        public static PrtgOptionalChannelProperty Custom => Unit("Custom");
        public static PrtgOptionalChannelProperty Count => Unit("Count");
        public static PrtgOptionalChannelProperty CPU => Unit("CPU");
        public static PrtgOptionalChannelProperty BytesFile => Unit("BytesFile");
        public static PrtgOptionalChannelProperty SpeedDisk => Unit("SpeedDisk");
        public static PrtgOptionalChannelProperty SpeedNet => Unit("SpeedNet");
        public static PrtgOptionalChannelProperty TimeHours => Unit("TimeHours");

        private static PrtgOptionalChannelProperty Unit(string value) =>
            new PrtgOptionalChannelProperty("Unit", value);
    }
}