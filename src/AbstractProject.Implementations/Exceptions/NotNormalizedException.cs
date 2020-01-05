using System;

namespace AbstractProject.Implementations.Exceptions
{
    [Serializable]
    public sealed class NotNormalizedException : Exception
    {
        public NotNormalizedException(object value) : base(message: $"{value} is not normalized")
        {
        }
    }
}