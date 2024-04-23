using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventScheduleApp.Models
{
    public class EventData
    {
        public string Event { get; set; }
        public string Date { get; set; }
        public string EventID { get; internal set; }

        public override string ToString()
        {
            return $"Event: {Event}, Date: {Date}";
        }
    }
}
