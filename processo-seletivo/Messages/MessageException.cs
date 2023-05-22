namespace processo_seletivo.Messages
{
    using System;

    public class MessageException : Exception
    {
        public int StatusCode { get; set; }

        public MessageException(string v)
        {
        }

        public MessageException(string message, int statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public MessageException(string message, int statusCode, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
