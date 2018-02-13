namespace ZeroMQ.Monitoring
{
    using System;
    using System.Text;
    using System.Runtime.InteropServices;

    /// <summary>
    /// A base class for the all ZmqMonitor events.
    /// </summary>
    public class ZmqMonitorEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZmqMonitorEventArgs"/> class.
        /// </summary>
        /// <param name="monitor">The <see cref="ZmqMonitor"/> that triggered the event.</param>
        /// <param name="address">The peer address.</param>
        public ZmqMonitorEventArgs(ZmqMonitor monitor, string address)
        {
            this.Monitor = monitor;
            byte[] temp = Encoding.Unicode.GetBytes(address);
            this.Address = Encoding.ASCII.GetString(temp, 0, temp.Length);               
            
        }

        /// <summary>
        /// Gets the monitor that triggered the event.
        /// </summary>
        public ZmqMonitor Monitor { get; private set; }

        /// <summary>
        /// Gets the peer address.
        /// </summary>
        public string Address { get; private set; }
    }
}