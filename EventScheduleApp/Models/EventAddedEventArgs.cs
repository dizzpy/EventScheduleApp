using System;

namespace EventScheduleApp
{
    public class EventAddedEventArgs : EventArgs
    {
        public string EventName { get; }
        public string Date { get; }

        public EventAddedEventArgs(string eventName, string date)
        {
            EventName = eventName;
            Date = date;
        }
    }
}
