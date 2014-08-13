using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ImajeLib
{
    [Serializable]
    public class NackException : Exception
    {
        public NackException()
        {
        }

        public NackException(string message) : base(message)
        {
        }

        public NackException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NackException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }

    [Serializable]
    public class UnknownAckException : Exception
    {
        public UnknownAckException()
        {
        }

        public UnknownAckException(string message) : base(message)
        {
        }

        public UnknownAckException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UnknownAckException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
