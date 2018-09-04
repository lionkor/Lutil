/*
 * Author: @Lion_Kor
 */

using System;
using System.Runtime.Serialization;

namespace Lutil.Vectors
{
    /// <summary>
    /// Description of Exception.
    /// </summary>
    public class WrongVectorDimensionException : Exception, ISerializable
    {
        /// <summary>
        /// Indicates that a 2D-Vector is treated like a 3D-Vector, or vice-versa.
        /// </summary>
		public WrongVectorDimensionException ()
        {
        }
        /// <summary>
        /// Indicates that a 2D-Vector is treated like a 3D-Vector, or vice-versa.
        /// </summary>
        /// <param name="message">Inherited from System.Exception</param>
	 	public WrongVectorDimensionException (string message) : base (message)
        {
        }

        /// <summary>
        /// Indicates that a 2D-Vector is treated like a 3D-Vector, or vice-versa.
        /// </summary>
        /// <param name="message">Inherited from System.Exception</param>
        /// <param name="innerException">Inherited from System.Exception</param>
		public WrongVectorDimensionException (string message, Exception innerException) : base (message, innerException)
        {
        }

        /// <summary>
        /// Indicates that a 2D-Vector is treated like a 3D-Vector, or vice-versa.
        /// (this constructor is needed for serialization)
        /// </summary>
        /// <param name="info">Inherited from System.Runtime.Serialization.ISerializable</param>
        /// <param name="context">Inherited from System.Runtime.Serialization.ISerializable</param>
        // This constructor is needed for serialization.
        protected WrongVectorDimensionException (SerializationInfo info, StreamingContext context) : base (info, context)
        {
        }
    }
}