using Godot;
using System;

namespace Base.Resources.Events
{
    public class PcEventSignal
    {
        public int RefId { get; set; } = -1;
        public int EventId { get; set; } = -1;
        public PcEventSignal()
        {
        }
        /// <summary>
        /// Creates a new instance of PcEventSignal.
        /// </summary>
        /// <param name="refId">the reference id of the PC involved in the event</param>
        /// <param name="eventId">the signal's id</param>
        public PcEventSignal(int refId, int signalId)
        {
            RefId = refId;
            EventId = signalId;
        }        
    }
}
