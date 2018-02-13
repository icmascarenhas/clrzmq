namespace ZeroMQ.Interop
{
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Text;

    internal static class ErrorProxy
    {
        private static readonly Dictionary<int, ErrorDetails> KnownErrors = new Dictionary<int, ErrorDetails>();

        public static bool ShouldTryAgain
        {
            get { return GetErrorCode() == ErrorCode.EAGAIN; }
        }

        public static bool ContextWasTerminated
        {
            get { return GetErrorCode() == ErrorCode.ETERM; }
        }

        public static bool ThreadWasInterrupted
        {
            get { return GetErrorCode() == ErrorCode.EINTR; }
        }

        public static int GetErrorCode()
        {
            return LibZmq.zmq_errno();
        }

        public static ErrorDetails GetLastError()
        {
            int errorCode = GetErrorCode();

            if (KnownErrors.ContainsKey(errorCode))
            {
                return KnownErrors[errorCode];
            }

            string message = Marshal.PtrToStringUni(LibZmq.zmq_strerror(errorCode));
            byte[] temp = Encoding.Unicode.GetBytes(message);

            message = Encoding.ASCII.GetString(temp, 0, temp.Length).Replace('\0', ' ').Trim(); ;
            var errorDetails = new ErrorDetails(errorCode, message);
            KnownErrors[errorCode] = errorDetails;

            return errorDetails;
        }
    }
}
