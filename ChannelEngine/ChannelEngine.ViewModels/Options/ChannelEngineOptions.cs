using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChannelEngine.ViewModels.Options
{
    public class ChannelEngineOptions
    {
        public const string ChannelEngine = "ChannelEngine";

        public string ApiUri { get; set; }
        public string ApiKey { get; set; }
    }
}
