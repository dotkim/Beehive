using System;

namespace Registry
{
    public class RequestedBindingException : Exception
    {
        public RequestedBindingException() { }
        public RequestedBindingException(string message) : base(message) { }
        public RequestedBindingException(string message, Exception inner) : base(message, inner) { }
    }
}
