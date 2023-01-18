using Base.Exceptions;
using Base.Resources.Bus;
using Godot;
using System;

namespace Base.Resources.Data
{
    public abstract class IoData
    {
        /// <summary>
        /// private reference to the IO the data is set on.
        /// </summary>
        private InteractiveObject io;
        /// <summary>
        /// Public reference to the IoData's InteractiveObject owner.
        /// </summary>
        public InteractiveObject Io
        {
            get { return this.io; }
            set
            {
                if (value == null) {
                    throw new RPGException(ErrorMessage.BAD_PARAMETERS, "IoData.Io - cannot be null");
                }
                this.io = value;
                if (io.Data == null)
                {
                    io.Data = this;
                }
            }
        }     
    }
}
