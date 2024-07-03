using System;
using System.Collections.Generic;
using TcOpen.Inxton.Logging;

namespace TcOpen.Inxton.Logging
{
    /// <summary>
    /// Default logger implementation with no real logging capability.
    /// Provides an empty implementation of logging for the framework when no other logger created.
    /// </summary>
    public class DummyLoggerAdapter : ILogger
    {
        [Obsolete("This property is only for testing. Do not queue messages in production!")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public System.Collections.Concurrent.ConcurrentQueue<(
            string message,
            object payload,
            string serverity
        )> MessageQueue;

        [Obsolete("This method is only for testing. Do not queue messages in production!")]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Advanced)]
        public void QueueMessages()
        {
            MessageQueue = new System.Collections.Concurrent.ConcurrentQueue<(
                string message,
                object payload,
                string serverity
            )>();
        }

        private (string message, object payload, string serverity) lastMessage;

#pragma warning disable CS0618 // Type or member is obsolete
        public (string message, object payload, string serverity) LastMessage
        {
            get => lastMessage;
            private set
            {
                lastMessage = value;
                MessageQueue?.Enqueue(lastMessage);
            }
        }
#pragma warning restore CS0618 // Type or member is obsolete

        public DummyLoggerAdapter() { }

        public void ClearLastMessage()
        {
            LastMessage = (string.Empty, null, string.Empty);
        }

        public bool IsLastMessageEmpty()
        {
            return LastMessage.message == string.Empty
                && LastMessage.payload == null
                && LastMessage.serverity == string.Empty;
        }

        public void Debug<T>(string stringTemplate, T payload = default)
        {
            LastMessage = (stringTemplate, payload, "Debug");
        }

        public void Error<T>(string stringTemplate, T payload = default)
        {
            LastMessage = (stringTemplate, payload, "Error");
        }

        public void Fatal<T>(string stringTemplate, T payload = default)
        {
            LastMessage = (stringTemplate, payload, "Fatal");
        }

        public void Information<T>(string stringTemplate, T payload = default)
        {
            LastMessage = (stringTemplate, payload, "Information");
        }

        public void Verbose<T>(string stringTemplate, T payload = default)
        {
            LastMessage = (stringTemplate, payload, "Verbose");
        }

        public void Warning<T>(string stringTemplate, T payload = default)
        {
            LastMessage = (stringTemplate, payload, "Warning");
        }
    }
}
