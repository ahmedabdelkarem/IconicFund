using IconicFund.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IconicFund.Web.Models
{
    public class LoggingDataViewModel
    {
        public Dictionary<string , string> OldData { get; set; }

        public Dictionary<string, string> NewData { get; set; }

        public LoggingAction  ActionType { get; set; }

    }
}
