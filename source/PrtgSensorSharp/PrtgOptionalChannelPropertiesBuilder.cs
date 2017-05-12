using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace PrtgSensorSharp
{
    /// <summary>
    /// This class makes it easy to create most common optional properties for the prtg channel. It will be probably extended with all available optional properties in the future
    /// </summary>
    public class PrtgOptionalChannelPropertiesBuilder
    {
        private readonly List<PrtgOptionalChannelProperty> _properties = new List<PrtgOptionalChannelProperty>();

        /// <summary>
        /// Define if the limit settings will be active. By default they are disabled. This setting will be considered only on the first sensor scan, when the channel is newly created; it is ignored on all further sensor scans (and may be omitted). You can change this initial setting later in the Channel settings of the sensor.
        /// </summary>
        /// <returns></returns>
        public PrtgOptionalChannelPropertiesBuilder WithLimitsEnabled()
        {
            _properties.Add(new PrtgOptionalChannelProperty("LimitMode", "1"));
            return this;
        }

        /// <summary>
        /// The Limit will not take effect in the LimitMode is not set to 1. To set the LimitMode to 1 use the <see cref="WithLimitsEnabled"/> method
        /// Define an upper error limit for the channel. If enabled, the sensor will be set to a "Down" status if this value is overrun and the LimitMode is activated. Note: Please provide the limit value in the unit of the base data type, just as used in the Value of the result. While a sensor shows a "Down" status triggered by a limit, it will still receive data in its channels. The values defined with this element will be considered only on the first sensor scan, when the channel is newly created; they are ignored on all further sensor scans (and may be omitted). You can change this initial setting later in the Channel settings of the sensor.
        /// </summary>
        /// <param name="max">The upper error limit.</param>
        /// <returns></returns>
        public PrtgOptionalChannelPropertiesBuilder WithLimitMaxError(decimal max)
        {
            _properties.Add(new PrtgOptionalChannelProperty("LimitMaxError", max.ToString(CultureInfo.InvariantCulture)));
            return this;
        }

        /// <summary>
        /// The Limit will not take effect in the LimitMode is not set to 1. To set the LimitMode to 1 use the <see cref="WithLimitsEnabled"/> method
        /// Define an upper warning limit for the channel. If enabled, the sensor will be set to a "Warning" status if this value is overrun and the LimitMode is activated. Note: Please provide the limit value in the unit of the base data type, just as used in the the Value of the result. The values defined with this element will be considered only on the first sensor scan, when the channel is newly created; they are ignored on all further sensor scans (and may be omitted). You can change this initial setting later in the Channel settings of the sensor.
        /// </summary>
        /// <param name="max">The upper warning limit</param>
        /// <returns></returns>
        public PrtgOptionalChannelPropertiesBuilder WithLimitMaxWarning(decimal max)
        {
            _properties.Add(new PrtgOptionalChannelProperty("LimitMaxWarning", max.ToString(CultureInfo.InvariantCulture)));
            return this;
        }

        /// <summary>
        /// The Limit will not take effect in the LimitMode is not set to 1. To set the LimitMode to 1 use the <see cref="WithLimitsEnabled"/> method
        /// Define a lower warning limit for the channel. If enabled, the sensor will be set to a "Warning" status if this value is undercut and the LimitMode is activated. Note: Please provide the limit value in the unit of the base data type, just as used in the Value of the result. The values defined with this element will be considered only on the first sensor scan, when the channel is newly created; they are ignored on all further sensor scans (and may be omitted). You can change this initial setting later in the Channel settings of the sensor.
        /// </summary>
        /// <param name="min">The lower warning limit.</param>
        /// <returns></returns>
        public PrtgOptionalChannelPropertiesBuilder WithLimitMinWarning(decimal min)
        {
            _properties.Add(new PrtgOptionalChannelProperty("LimitMinWarning", min.ToString(CultureInfo.InvariantCulture)));
            return this;
        }

        /// <summary>
        /// The Limit will not take effect in the LimitMode is not set to 1. To set the LimitMode to 1 use the <see cref="WithLimitsEnabled"/> method
        /// Define a lower error limit for the channel. If enabled, the sensor will be set to a "Down" status if this value is undercut and the LimitMode is activated. Note: Please provide the limit value in the unit of the base data type, just as used in the Value of the result. While a sensor shows a "Down" status triggered by a limit, it will still receive data in its channels. The values defined with this element will be considered only on the first sensor scan, when the channel is newly created; they are ignored on all further sensor scans (and may be omitted). You can change this initial setting later in the Channel settings of the sensor.
        /// </summary>
        /// <param name="min">The lower error limit.</param>
        /// <returns></returns>
        public PrtgOptionalChannelPropertiesBuilder WithLimitMinError(decimal min)
        {
            _properties.Add(new PrtgOptionalChannelProperty("LimitMinError", min.ToString(CultureInfo.InvariantCulture)));
            return this;
        }

        public PrtgOptionalChannelProperty[] Build()
        {
            return _properties.ToArray();
        }
    }
}
