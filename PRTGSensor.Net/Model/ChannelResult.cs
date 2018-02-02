using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;
using static PrtgSensorNet.PrtgSensorMessage;

namespace PrtgSensorNet.Model
{
    public class ChannelResult
    {
        /// <summary>
        /// The returned data for the EXE/Script Advanced.
        /// Parameters have a default value and are not required.
        /// </summary>
        /// <param name="channelName">Name of the channel as displayed in user interfaces. This parameter is required and must be unique for the sensor.</param>
        /// <param name="channelValue">The value as integer or float. Please make sure the Float setting matches the kind of value provided. Otherwise PRTG will show 0 values</param>
        public ChannelResult(string channelName, float channelValue)
        {
            Channel = channelName;
            Value = channelValue;
        }

        /// <summary>
        /// Name of the channel as displayed in user interfaces. This parameter is required and must be unique for the sensor.
        /// </summary>
        [DataMember(Name = "channel", IsRequired = true, EmitDefaultValue = false)]
        public string Channel { get; set; }

        /// <summary>
        /// The value as integer or float. Please make sure the Float setting matches the kind of value provided. Otherwise PRTG will show 0 values
        /// </summary>
        [DataMember(Name = "value", IsRequired = true, EmitDefaultValue = false), JsonConverter(typeof(FormatNumbersAsTextConverter))]
        public float Value { get; set; }

        /// <summary>
        /// The unit of the value. Default is Custom. Useful for PRTG to be able to convert volumes and times.
        /// </summary>
        [DataMember(Name = "Unit", IsRequired = false, EmitDefaultValue = false)]
        public Unit? Unit { get; set; }

        /// <summary>
        /// If Custom is used as unit this is the text displayed behind the value.
        /// </summary>
        [DataMember(Name = "customunit", IsRequired = false, EmitDefaultValue = false)]
        public string CustomUnit
        {
            get => _customUnit;
            set
            {
                if (value.Length > 10)
                {
                    throw new ArgumentException("Must be shorter than 10 char");
                }
                _customUnit = value;
            }
        }
        private string _customUnit;

        /// <summary>
        /// Size used for the display value. For example, if you have a value of 50000 and use Kilo as size the display is
        /// 50 kilo #. Default is One (value used as returned). For the Bytes and 
        /// Speed units this is overridden by the setting in the user interface. 
        /// </summary>
        [DataMember(Name = "speedsize", IsRequired = false, EmitDefaultValue = false), JsonConverter(typeof(FormatNumbersAsTextConverter))]
        public SpeedSize? SpeedSize { get; set; }

        /// <summary>
        /// Used when displaying the speed. Default is Second.
        /// </summary>
        [DataMember(Name = "speedtime", IsRequired = false, EmitDefaultValue = false), JsonConverter(typeof(FormatNumbersAsTextConverter))]
        public SpeedTime? SpeedTime { get; set; }

        /// <summary>
        /// Selects if the value is a absolute value or counter. Default is Absolute.
        /// </summary>
        [DataMember(Name = "mode", IsRequired = false, EmitDefaultValue = false), JsonConverter(typeof(FormatNumbersAsTextConverter))]
        public Mode? Mode { get; set; }

        /// <summary>
        /// Define if the value is a float. Default is 0 (no).
        /// If set to 1 (yes), use a dot as decimal separator in values. 
        /// </summary>
        //[DataMember(Name = "float", IsRequired = false, EmitDefaultValue = false), JsonConverter(typeof(FormatNumbersAsTextConverter))]
        //public int? Float
        //{
        //    get => (_floatValue == 0 || Value % 1 == 0) ? 0 : 1;
        //    set => _floatValue = value;
        //}
        //private int? _floatValue;

        /// <summary>
        /// Init value for the Decimal Places
        /// option. If 0 is used in the &lt;Float&gt; element (i.e. use integer),
        /// the default is Auto; otherwise (i.e. for float) default is All. 
        /// </summary>
        //[DataMember(Name = "decimalmode", IsRequired = false, EmitDefaultValue = false), JsonConverter(typeof(FormatNumbersAsTextConverter))]
        //public DecimalMode DecimalMode
        //{
        //    get => (_decimalMode == null && Float == 0) ? DecimalMode.Auto : DecimalMode.All;
        //    set => _decimalMode = value;
        //}
        //private DecimalMode? _decimalMode;

        /// <summary>
        /// If enabled for at least one channel, the entire sensor is set to warning status. Default is 0 (no).
        /// </summary>
        [DataMember(Name = "warning", IsRequired = false, EmitDefaultValue = false), JsonConverter(typeof(FormatNumbersAsTextConverter))]
        public YesNoEnum? Warning { get; set; }

        /// <summary>
        /// Init value for the Show in Graph option. Default is 1 (yes).
        /// Note: The values defined with this element will be considered only
        /// on the first sensor scan, when the channel is newly created;
        /// they are ignored on all further sensor scans (and may be omitted). You can change this initial setting later in the 
        /// </summary>
        [DataMember(Name = "showchart", IsRequired = false, EmitDefaultValue = false), JsonConverter(typeof(FormatNumbersAsTextConverter))]
        public YesNoEnum? ShowChart { get; set; }

        /// <summary>
        /// Init value for the Show in Table option. Default is 1 (yes).
        /// Note: The values defined with this element will be considered only
        /// on the first sensor scan, when the channel is newly created;
        /// they are ignored on all further sensor scans (and may be omitted).
        /// </summary>
        [DataMember(Name = "showtable", IsRequired = false, EmitDefaultValue = false), JsonConverter(typeof(FormatNumbersAsTextConverter))]
        public YesNoEnum? ShowTable { get; set; }

        /// <summary>
        /// Define an upper error limit for the channel. If enabled, the sensor will be set to a "Down"
        /// status if this value is overrun and the LimitMode is activated. 
        /// </summary>
        [DataMember(Name = "limitmaxerror", IsRequired = false, EmitDefaultValue = false), JsonConverter(typeof(FormatNumbersAsTextConverter))]
        public int? LimitMaxError { get; set; }

        /// <summary>
        /// Define an upper warning limit for the channel. 
        /// If enabled, the sensor will be set to a "Warning" status if this value is overrun and the LimitMode is activated.
        /// </summary>
        [DataMember(Name = "limitmaxwarnings", IsRequired = false, EmitDefaultValue = false), JsonConverter(typeof(FormatNumbersAsTextConverter))]
        public int? LimitMaxWarnings { get; set; }

        /// <summary>
        /// Define a lower warning limit for the channel. If enabled, the sensor 
        /// will be set to a "Warning" status if this value is undercut and the LimitMode is activated.
        /// </summary>
        [DataMember(Name = "limitminwarning", IsRequired = false, EmitDefaultValue = false), JsonConverter(typeof(FormatNumbersAsTextConverter))]
        public int? LimitMinWarning { get; set; }

        /// <summary>
        /// Define a lower error limit for the channel. If enabled, the sensor will be set 
        /// to a "Down" status if this value is undercut and the LimitMode is activated. 
        /// </summary>
        [DataMember(Name = "limitminerror", IsRequired = false, EmitDefaultValue = false), JsonConverter(typeof(FormatNumbersAsTextConverter))]
        public int? LimitMinError { get; set; }

        /// <summary>
        /// Define an additional message. It will be added to the sensor's message when 
        /// entering a "Down" status that is triggered by a limit.
        /// </summary>
        [DataMember(Name = "limiterrormsg", IsRequired = false, EmitDefaultValue = false)]
        public string LimitErrorMsg { get; set; }

        /// <summary>
        /// Define an additional message. It will be added to the sensor's message when 
        /// entering a "Warning" status that is triggered by a limit. 
        /// </summary>
        [DataMember(Name = "limitwarningmsg", IsRequired = false, EmitDefaultValue = false)]
        public string LimitWarningMsg { get; set; }

        /// <summary>
        /// Define if the limit settings defined above will be active. Default is 0 (no; limits inactive).
        /// If 0 is used the limits will be written to the sensor channel settings as predefined values,
        /// but limits will be disabled. 
        /// </summary>
        [DataMember(Name = "limitmode", IsRequired = false, EmitDefaultValue = false), JsonConverter(typeof(FormatNumbersAsTextConverter))]
        public YesNoEnum? LimitMode { get; set; }

        /// <summary>
        /// Define if you want to use a lookup file (e.g. to view integer values as status texts).
        ///  Please enter the ID of the lookup file you want to use, or omit this element to not use lookups. 
        /// </summary>
        [DataMember(Name = "valuelookup", IsRequired = false, EmitDefaultValue = false)]
        public string ValueLookup { get; set; }

        /// <summary>
        /// If a returned channel contains this tag, it will trigger a change notification that you
        /// can use with the Change Trigger to send a notification.
        /// </summary>
        [DataMember(Name = "notifychanged", IsRequired = false, EmitDefaultValue = false), JsonConverter(typeof(FormatNumbersAsTextConverter))]
        public bool? NotifyChanged { get; set; }
    }
}
