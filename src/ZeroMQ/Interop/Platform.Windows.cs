#if !UNIX

namespace ZeroMQ.Interop
{
    using System;
    using System.Runtime.InteropServices;

    internal static partial class Platform
    {
        public const string LibSuffix = ".dll";

        private const string KernelLib = "coredll";

        public static IntPtr OpenHandle(string filename)
        {
            IntPtr lib = LoadLibraryCE(@"libzmqCE.dll");
            return lib;
        }

        public static IntPtr LoadProcedure(IntPtr handle, string functionName)
        {
            return GetProcAddress(handle, functionName);
        }

        public static bool ReleaseHandle(IntPtr handle)
        {
            return FreeLibrary(handle);
        }

        public static Exception GetLastLibraryError()
        {
            return new Exception();
        }
      
        [DllImport("coredll.dll", EntryPoint = "LoadLibrary", SetLastError = true)]
        internal static extern IntPtr LoadLibraryCE(string lpszLib);

        [DllImport(KernelLib, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool FreeLibrary(IntPtr moduleHandle);

        [DllImport(KernelLib)]
        private static extern IntPtr GetProcAddress(IntPtr moduleHandle, string procname);
    }
}

#endif