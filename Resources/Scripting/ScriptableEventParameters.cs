using Base.Resources.Bus;
using Bus.Services;
using System.Collections.Generic;

namespace Base.Resources.Scripting
{
    public class ScriptableEventParameters
    {
        /// <summary>
        /// the scriptable event's id. an event will have a hardcoded id or a name, but not both.
        /// </summary>
        /// <value></value>
        public int EventId { get; set; } = -1;
        /// <summary>
        /// the scriptable event's name. an event will have a hardcoded id or a name, but not both.
        /// </summary>
        /// <value></value>
        public string EventName { get; set; }
        /// <summary>
        /// the IO broadcasting the event.
        /// </summary>
        /// <value></value>
        public InteractiveObject EventSender { get; set; }
        /// <summary>
        /// any parameters included with the event.
        /// </summary>
        /// <value></value>
        public Dictionary<string, object> Parameters { get; set; }
        /// <summary>
        /// The return value.
        /// </summary>
        /// <value></value>
        public int RetValue { get; set; } = Globals.ACCEPT;
        /// <summary>
        /// the target IO's reference id. if no id is set, the event is meant to be processed by all listeners.
        /// </summary>
        /// <value></value>
        public int TargetIo { get; set; } = -1;
    }
}
