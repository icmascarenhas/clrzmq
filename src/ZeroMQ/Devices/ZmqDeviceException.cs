namespace ZeroMQ.Devices
{
    using System;
    using System.Runtime.Serialization;

    using Interop;

    /// <summary>
    /// The exception that is thrown when a ZeroMQ device error occurs.
    /// </summary>
    [Serializable]
    public class ZmqDeviceException : ZmqException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZmqDeviceException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code returned by the ZeroMQ library call.</param>
        public ZmqDeviceException(int errorCode)
            : base(errorCode)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmqDeviceException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code returned by the ZeroMQ library call.</param>
        /// <param name="message">The message that describes the error</param>
        public ZmqDeviceException(int errorCode, string message)
            : base(errorCode, message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmqDeviceException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code returned by the ZeroMQ library call.</param>
        /// <param name="message">The message that describes the error</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
        public ZmqDeviceException(int errorCode, string message, Exception inner)
            : base(errorCode, message, inner)
        {
        }

        internal ZmqDeviceException(ErrorDetails errorDetails)
            : base(errorDetails)
        {
        }
    }
}
