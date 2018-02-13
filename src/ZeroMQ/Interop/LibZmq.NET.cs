#if !MONO

namespace ZeroMQ.Interop
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Runtime.InteropServices;

    // ReSharper disable InconsistentNaming
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Compatibility with native headers.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter", Justification = "Compatibility with native headers.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Compatibility with native headers.")]
    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "Compatibility with native headers.")]
    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1202:ElementsMustBeOrderedByAccess", Justification = "Reviewed. Suppression is OK here.")]
    internal static class LibZmq
    {
#if WindowsCE
        public const string LibraryName = "libzmqCE";
#else
        public const string LibraryName = "libzmq";
#endif

        private static readonly int Zmq3MsgTSize = 32 * Marshal.SizeOf(typeof(byte));

        private static readonly int ZmqMaxVsmSize = 30 * Marshal.SizeOf(typeof(byte));
        private static readonly int Zmq2MsgTSize = IntPtr.Size + (Marshal.SizeOf(typeof(byte)) * 2) + ZmqMaxVsmSize;

        public static readonly int ZmqMsgTSize;

        public static readonly int MajorVersion;
        public static readonly int MinorVersion;
        public static readonly int PatchVersion;


        public static readonly long PollTimeoutRatio;

        static LibZmq()
        {
            AssignCurrentVersion(out MajorVersion, out MinorVersion, out PatchVersion);
            ZmqMsgTSize = Zmq3MsgTSize;

        }

        private static void AssignCommonDelegates()
        {
          
        }

        private static void AssignCurrentVersion(out int majorVersion, out int minorVersion, out int patchVersion)
        {
            int sizeofInt32 = Marshal.SizeOf(typeof(int));

            IntPtr majorPointer = Marshal.AllocHGlobal(sizeofInt32);
            IntPtr minorPointer = Marshal.AllocHGlobal(sizeofInt32);
            IntPtr patchPointer = Marshal.AllocHGlobal(sizeofInt32);

            zmq_version(majorPointer, minorPointer, patchPointer);

            majorVersion = Marshal.ReadInt32(majorPointer);
            minorVersion = Marshal.ReadInt32(minorPointer);
            patchVersion = Marshal.ReadInt32(patchPointer);

            Marshal.FreeHGlobal(majorPointer);
            Marshal.FreeHGlobal(minorPointer);
            Marshal.FreeHGlobal(patchPointer);
        }

        [DllImport(LibraryName)]
        public static extern int zmq_close(IntPtr socket);

        [DllImport(LibraryName)]
        public static extern int zmq_setsockopt(IntPtr socket, int option, IntPtr optval, int optvallen);

        [DllImport(LibraryName)]
        public static extern int zmq_getsockopt(IntPtr socket, int option, IntPtr optval, IntPtr optvallen);

        [DllImport(LibraryName)]
        public static extern int zmq_bind(IntPtr socket, byte[] addr);

        [DllImport(LibraryName, CharSet = CharSet.Unicode)]
        public static extern int zmq_connect(IntPtr socket,  byte[] addr);

        [DllImport(LibraryName)]
        public static extern IntPtr zmq_socket(IntPtr context, int type);

        [DllImport(LibraryName)]
        public static extern int zmq_msg_close(IntPtr msg);

        [DllImport(LibraryName)]
        public static extern int zmq_msg_copy(IntPtr destmsg, IntPtr srcmsg);

        [DllImport(LibraryName)]
        public static extern IntPtr zmq_msg_data(IntPtr msg);

        [DllImport(LibraryName)]
        public static extern int zmq_msg_init(IntPtr msg);

        [DllImport(LibraryName)]
        public static extern int zmq_msg_init_size(IntPtr msg, int size);

        [DllImport(LibraryName)]
        public static extern int zmq_msg_size(IntPtr msg);

        [DllImport(LibraryName)]
        public static extern int zmq_errno();

        [DllImport(LibraryName)]
        public static extern IntPtr zmq_strerror(int errnum);

        [DllImport(LibraryName)]
        public static extern void zmq_version(IntPtr major, IntPtr minor, IntPtr patch);

        [DllImport(LibraryName)]
        public static extern int zmq_poll([In] [Out] PollItem[] items, int numItems, long timeoutMsec);

        [DllImport(LibraryName)]
        public static extern int zmq_msg_move(IntPtr destmsg, IntPtr srcmsg);

        [DllImport(LibraryName, EntryPoint = "zmq_msg_send")]
        public static extern Int32 zmq_msg_send(IntPtr msg, IntPtr socket, Int32 flags);
        
        [DllImport(LibraryName, EntryPoint = "zmq_msg_recv")]
        public static extern int zmq_msg_recv_v3(IntPtr msg, IntPtr socket, int flags);

        public delegate int ZmqMsgRecvProc(IntPtr msg, IntPtr socket, int flags);
        public static ZmqMsgRecvProc zmq_msg_recv = zmq_msg_recv_v3;



        [DllImport(LibraryName, EntryPoint = "zmq_ctx_new")]
        public static extern IntPtr zmq_ctx_new();

        [DllImport(LibraryName)]
        public static extern IntPtr zmq_init(int io_threads);

        [DllImport(LibraryName, EntryPoint = "zmq_ctx_destroy")]
        public static extern int zmq_ctx_destroy(IntPtr context);

        [DllImport(LibraryName)]
        public static extern int zmq_term(IntPtr context);

        [DllImport(LibraryName, EntryPoint = "zmq_ctx_get")]
        public static extern int zmq_ctx_get(IntPtr context, int option);

        [DllImport(LibraryName, EntryPoint = "zmq_ctx_set")]
        public static extern int zmq_ctx_set(IntPtr context, int option, int optval);

        [DllImport(LibraryName, EntryPoint = "zmq_socket_monitor")]
        public static extern int zmq_socket_monitor(IntPtr socket, byte[] addr, int events);

        [DllImport(LibraryName, EntryPoint = "zmq_recv")]
        public static extern int zmq_buffer_recv2(IntPtr socket, IntPtr buf, int size, int flags);

        public delegate int ZmqBufferRecvProc(IntPtr socket, IntPtr buf, int size, int flags);
        public static ZmqBufferRecvProc zmq_buffer_recv = zmq_buffer_recv2;


        [DllImport(LibraryName, EntryPoint = "zmq_send")]
        public static extern int zmq_sendbuf_v3(IntPtr socket, IntPtr buf, int size, int flags);

        public delegate int ZmqBufferSendProc(IntPtr socket, IntPtr buf, int size, int flags);
        public static ZmqBufferSendProc zmq_buffer_send = zmq_sendbuf_v3;

        [DllImport(LibraryName, EntryPoint = "zmq_unbind")]
        public static extern Int32 zmq_unbind(IntPtr socket, byte[] addr);

        [DllImport(LibraryName, EntryPoint = "zmq_disconnect")]
        public static extern Int32 zmq_disconnect(IntPtr socket, byte[] addr);

    }
}

#endif