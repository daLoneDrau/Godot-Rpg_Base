using Base.Resources.Services;
using System;

namespace Base.Resources.Events
{
    public class Channel<SIGNAL>
    {
        /// <summary>
        /// the list of subscribers.
        /// </summary>
        private Action<SIGNAL>[] subscribers = new Action<SIGNAL>[0];
        /// <summary>
        /// Adds a non persistent subscriber to the Channel.
        /// </summary>
        /// <param name="call">a callback function</param>
        public void AddSubscriber(Action<SIGNAL> call)
        {
            subscribers = ArrayUtilities.Instance.ExtendArray(call, subscribers);
        }
        /// <summary>
        /// Broadcasts a signal to all subscribers.
        /// </summary>
        /// <param name="signal"></param>
        public void Broadcast(SIGNAL signal)
        {
            for (int i = 0, li = subscribers.Length; i < li; i++)
            {
                subscribers[i](signal);
            }
        }
        /// <summary>
        /// Removes a non persistent subscriber from the Channel.  If you have added the same subscriber multiple times, this method will remove all occurrences of it.
        /// </summary>
        /// <param name="call">a callback function</param>
        public void RemoveSubscriber(Action<SIGNAL> call)
        {
            for (int i = subscribers.Length - 1; i >= 0; i--)
            {
                if (subscribers[i].Equals(call))
                {
                    subscribers = ArrayUtilities.Instance.RemoveIndex(i, subscribers);
                }
            }
        }
    }
}
