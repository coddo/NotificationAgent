using System;

namespace NotificationAgent.Exceptions
{
    public class NotConfiguredException : Exception
    {
        public NotConfiguredException() : base()
        {

        }

        public NotConfiguredException(string message) : base(message)
        {

        }
    }
}
