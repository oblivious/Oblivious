using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class ErrorMessage
    {
        private string _message;
        private string _source;
        
        public ErrorMessage(string message,
            string source)
        {
            _message = message;
            _source = source;
        }

        public ErrorMessage(string message)
        {
            _message = message;
        }

        public ErrorMessage()
        {
        }

        public string Message
        {
            get
            {
                return _message;
            }

            set
            {
                _message = value;
            }
        }

        public string Source
        {
            get
            {
                return _source;
            }

            set
            {
                _source = value;
            }
        }

        public override string ToString()
        {
            return _message;
        }
    }
}
