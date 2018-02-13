namespace ZeroMQ.Interop
{
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct MonitorEventData
    {
        [MarshalAs(UnmanagedType.U4)]
        public int Event;

        [MarshalAs(UnmanagedType.LPWStr)]
        public string Address;
       

        [MarshalAs(UnmanagedType.U4)]
        public int Value;
    }
}