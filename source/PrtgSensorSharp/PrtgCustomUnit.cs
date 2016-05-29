namespace PrtgSensorSharp
{
    public static class PrtgCustomUnit
    {
        public static PrtgOptionalChannelProperty Named(string customUnit) =>
            new PrtgOptionalChannelProperty("CustomUnit", customUnit);
    }
}