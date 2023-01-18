using Base.Pooled;
using System;

/// <summary>
/// 
/// </summary>
namespace Base.Exceptions
{
    public sealed class RPGException : Exception
    {
        /// <summary>
        /// Gets the formatted message string.
        /// </summary>
        /// <param name="message">the error message</param>
        /// <param name="devMsg">the developer's message</param>
        /// <returns></returns>
        private static String GetMessageString(ErrorMessage message, string devMsg)
        {
            PooledStringBuilder sb = StringBuilderPool.Instance.GetStringBuilder();
            try
            {
                sb.Append("ErrorMessage [");
                sb.Append(message);
                sb.Append(" user_message = ");
                sb.Append(message.GetUserMessage());
                sb.Append(", developer_message = ");
                sb.Append(devMsg);
                sb.Append("]");
            }
            catch (PooledException e)
            {
                // TODO Auto-generated catch block
                Console.WriteLine(e.ToString());
            }
            String s = sb.ToString();
            sb.ReturnToPool();
            return s;
        }
        /// <summary>
        ///  the developer message.
        /// </summary>
        private readonly string developerMessage;
        /// <summary>
        /// the error message.
        /// </summary>
        private readonly ErrorMessage errorMessage;
        /// <summary>
        /// Creates a new instance of <see cref="RPGException"/>.
        /// </summary>
        /// <param name="message">the <see cref="ErrorMessage"/></param>
        /// <param name="ex">the cause (which is saved for later retrieval by the <see cref="Exception.Message"/> method). (A <tt>null</tt> value is permitted, and indicates that the cause is nonexistent or  unknown.)</param>
        public RPGException(ErrorMessage message, Exception ex) : base(RPGException.GetMessageString(message, ex.Message))
        {
            errorMessage = message;
            developerMessage = ex.Message;
        }
        public RPGException(ErrorMessage message, String devMsg) : base(RPGException.GetMessageString(message, devMsg))
        {
            errorMessage = message;
            developerMessage = devMsg;
        }
        public RPGException(ErrorMessage message, String devMsg, Exception ex) : base(RPGException.GetMessageString(message, devMsg), ex)
        {
            errorMessage = message;
            developerMessage = ex.Message;
        }
        /// <summary>
        /// Gets the message from the developer.
        /// </summary>
        /// <returns>string</returns>
        public string GetDeveloperMessage()
        {
            return developerMessage;
        }
        /// <summary>
        /// Gets the <see cref="ErrorMessage"/>.
        /// </summary>
        /// <returns></returns>
        public ErrorMessage GetErrorMessage()
        {
            return errorMessage;
        }
    }
}