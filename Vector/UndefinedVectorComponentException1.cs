/*
 * Author: @Lion_Kor
 */

using System;
using System.Runtime.Serialization;

namespace Lutil.Vectors
{
    [Serializable]
    internal class UndefinedVectorComponentException : Exception
    {
        public UndefinedVectorComponentException ()
        {
        }

        public UndefinedVectorComponentException (string message) : base (message)
        {
        }

        public UndefinedVectorComponentException (string message, Exception innerException) : base (message, innerException)
        {
        }

        protected UndefinedVectorComponentException (SerializationInfo info, StreamingContext context) : base (info, context)
        {
        }
    }
}