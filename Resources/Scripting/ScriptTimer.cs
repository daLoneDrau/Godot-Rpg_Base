using Base.Bus;
using Base.Resources.Data;
using Godot;
using System;

namespace Base.Resources.Scripting
{
    public class ScriptTimer : IoData
    {
        public int EventId { get; set; }
        public string EventName { get; set; }
        /// <summary>
        /// flag indicating whether the timer exists.
        /// </summary>
        /// <value></value>
        public bool Exists { get; set; }
        /// <summary>
        /// any flags set on the timer.
        /// </summary>
        public FlagSet flags { get; private set; } = new FlagSet();
        /// <summary>
        /// the timer interval (msecs).
        /// </summary>
        /// <value></value>
        public int Interval { get; set; }
        /// <summary>
        /// the Script Timer's name.
        /// </summary>
        /// <value></value>
        public string Name { get; set; }
        /// <summary>
        /// the script associated with the timer (es).
        /// </summary>
        public IoScript Script { get; set; }
        /// <summary>
        /// the last time the timer was executed.
        /// </summary>
        /// <value></value>
        public int Tim { get; set; }
        /// <summary>
        /// the # of times the timer should run.
        /// </summary>
        /// <value></value>
        public int Times { get; set; } = 1;
        /// <summary>
        /// Clears a timer.
        /// </summary>
        public void Clear()
        {
            this.Name = "";
            this.Exists = false;
        }
        public override bool Equals(object obj)
        {
            bool b = false;
            if (obj != null && GetType() == obj.GetType())
            {
                b = this.Name.ToLower() == ((ScriptTimer)obj).Name.ToLower();
            }
            return b;
        }
        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            // throw new System.NotImplementedException();
            return base.GetHashCode();
        }
    }
}
