using System.Collections.Generic;
using PrtgSensorNet;
using PrtgSensorNet.Model;

namespace PRTGSensorNet.Example
{
    class Program
    {
        static void Main(string[] args)
        {

            var channelResultCollection = new List<ChannelResult>
            {
                new ChannelResult("eggs", 0),
                new ChannelResult("bacon", 3),
                new ChannelResult("waffle", 1),
                new ChannelResult("toast", 2)
            };


            var breakfastOrder = new PrtgSensorMessage(channelResultCollection, "Breakfast Order");
            breakfastOrder.SendPrtgMessage();
        }
    }
}
