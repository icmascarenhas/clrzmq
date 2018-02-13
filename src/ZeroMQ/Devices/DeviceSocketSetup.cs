namespace ZeroMQ.Devices
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Defines a fluent interface for configuring device sockets.
    /// </summary>
    public class DeviceSocketSetup
    {
        private readonly ZmqSocket _socket;
        private readonly List<Action<ZmqSocket>> _socketInitializers;
        private readonly List<string> _bindings;
        private readonly List<string> _connections;

        private bool _isConfigured;

        internal DeviceSocketSetup(ZmqSocket socket)
        {
            if (socket == null)
            {
                throw new ArgumentNullException("socket");
            }

            _socket = socket;
            _socketInitializers = new List<Action<ZmqSocket>>();
            _bindings = new List<string>();
            _connections = new List<string>();
        }

        /// <summary>
        /// Configure the socket to bind to a given endpoint. See <see cref="ZmqSocket.Bind"/> for details.
        /// </summary>
        /// <param name="endpoint">A string representing the endpoint to which the socket will bind.</param>
        /// <returns>The current <see cref="DeviceSocketSetup"/> object.</returns>
        public DeviceSocketSetup Bind(string endpoint)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException("endpoint");
            }

            _bindings.Add(endpoint);

            return this;
        }

        /// <summary>
        /// Configure the socket to connect to a given endpoint. See <see cref="ZmqSocket.Connect"/> for details.
        /// </summary>
        /// <param name="endpoint">A string representing the endpoint to which the socket will connect.</param>
        /// <returns>The current <see cref="DeviceSocketSetup"/> object.</returns>
        public DeviceSocketSetup Connect(string endpoint)
        {
            if (endpoint == null)
            {
                throw new ArgumentNullException("endpoint");
            }

            _connections.Add(endpoint);

            return this;
        }

      
        /// <summary>
        /// Configure the socket to subscribe to a specific prefix. See <see cref="ZmqSocket.Subscribe"/> for details.
        /// </summary>
        /// <param name="prefix">A byte array containing the prefix to which the socket will subscribe.</param>
        /// <returns>The current <see cref="DeviceSocketSetup"/> object.</returns>
        public DeviceSocketSetup Subscribe(byte[] prefix)
        {
            return AddSocketInitializer(s => s.Subscribe(prefix));
        }

        /// <summary>
        /// Configure the socket to subscribe to all incoming messages. See <see cref="ZmqSocket.SubscribeAll"/> for details.
        /// </summary>
        /// <returns>The current <see cref="DeviceSocketSetup"/> object.</returns>
        public DeviceSocketSetup SubscribeAll()
        {
            return AddSocketInitializer(s => s.SubscribeAll());
        }

        internal DeviceSocketSetup AddSocketInitializer(Action<ZmqSocket> setupMethod)
        {
            _socketInitializers.Add(setupMethod);

            return this;
        }

        internal void Configure()
        {
            if (_isConfigured)
            {
                return;
            }

            if (_bindings.Count == 0 && _connections.Count == 0)
            {
                throw new InvalidOperationException("Device sockets must bind or connect to at least one endpoint.");
            }

            foreach (Action<ZmqSocket> initializer in _socketInitializers)
            {
                initializer.Invoke(_socket);
            }

            foreach (string endpoint in _bindings)
            {
                _socket.Bind(endpoint);
            }

            foreach (string endpoint in _connections)
            {
                _socket.Connect(endpoint);
            }

            _isConfigured = true;
        }

    }
}
