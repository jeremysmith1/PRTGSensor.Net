using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using PrtgSensorNet.Model;
using static System.Console;

namespace PrtgSensorNet
{

    [DataContract(Name = "prtg")]
    public class PrtgSensorMessage
    {
        private JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore
        };

        /// <summary>
        /// Single Message for Custom Sensor
        /// </summary>
        /// <param name="channelResults">Collection of Results For Channels</param>
        /// <param name="text">Text the sensor returns in the Message field with every scanning interval. There can be one message per sensor, regardless of the number of channels. Default is OK. </param>
        /// <param name="error">If enabled, the sensor will return an error status.</param>
        public PrtgSensorMessage(IEnumerable<ChannelResult> channelResults, string text = "OK", YesNoEnum error = YesNoEnum.No)
        {
            ResultCollection = channelResults.ToArray();
            Text = text;
            Error = error;
        }

        /// <summary>
        /// Creates and sends a prtg message
        /// </summary>
        /// <param name="channelResults">Collection of Results For Channels</param>
        /// <param name="text">Text the sensor returns in the Message field with every scanning interval. There can be one message per sensor, regardless of the number of channels. Default is OK. </param>
        /// <param name="error">If enabled, the sensor will return an error status.</param>
        /// <returns></returns>
        public void CreateAndSendPrtgMessage(IEnumerable<ChannelResult> channelResults, string text = "OK",YesNoEnum error = YesNoEnum.No) =>
            WriteLine(JsonConvert.SerializeObject(new PrtgSensorMessage(channelResults, text, error),
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                }));

        /// <summary>
        /// Send Single PRTG Message
        /// </summary>
        /// <param name="message">PRTG Message</param>
        public void SendPrtgMessage() => WriteLine(JsonConvert.SerializeObject(this, _jsonSettings));

        /// <summary>
        /// Send Collection of PRTG Messages
        /// </summary>
        /// <param name="messages"></param>
        public static void SendPrtgMessage(IEnumerable<PrtgSensorMessage> messages) => messages.ToList()
            .ForEach(message => WriteLine(JsonConvert.SerializeObject(message,
                Formatting.None,
                new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                })));

        [DataMember(Name = "result", IsRequired = true)]
        private ChannelResult[] ResultCollection { get; }

        /// <summary>
        ///  Text the sensor returns in the Message field with every scanning interval.
        ///  There can be one message per sensor, regardless of the number of channels. Default is OK. 
        /// </summary>
        [DataMember(Name = "text", IsRequired = false, EmitDefaultValue = false)]
        public string Text {
            get => _text;
            set
            {
                if (value.Length >= 2000)
                {
                    throw new ArgumentException("Max. length: 2000 characters");
                }
                _text = value;
            }
        }
        private string _text;

        /// <summary>
        /// If enabled, the sensor will return an error status.
        /// This element can be combined with the Text element in order to show an error message.
        /// Default is 0. 
        /// Note: This element has to be provided outside of the result element. A sensor in this error status 
        /// cannot return any data in its channels; if used, all channel values in the result section will be ignored. 
        /// </summary>
        [DataMember(Name = "error", IsRequired = false, EmitDefaultValue = false)]
        public YesNoEnum Error { get; set; }

        internal sealed class FormatNumbersAsTextConverter : JsonConverter
        {
            public override bool CanRead => false;
            public override bool CanWrite => true;
            public override bool CanConvert(Type type) => true;

            public override void WriteJson(
                JsonWriter writer, object value, JsonSerializer serializer)
            {
                if(value is Enum)
                {
                    var test = (int)value;
                    serializer.Serialize(writer, test.ToString());
                }
                else
                {
                    serializer.Serialize(writer, value.ToString());

                }
            }

            public override object ReadJson(
                JsonReader reader, Type type, object existingValue, JsonSerializer serializer)
            {
                throw new NotSupportedException();
            }
        }
    }
}
