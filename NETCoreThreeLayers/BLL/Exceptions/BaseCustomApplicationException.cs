using System.Runtime.Serialization;

namespace BLL.Exceptions;

[Serializable]
public class BaseCustomApplicationException : Exception
{
    public BaseCustomApplicationException()
    {
    }

    public BaseCustomApplicationException(string message)
        : base(message)
    {
    }

    public BaseCustomApplicationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    // Without this constructor, deserialization will fail
    protected BaseCustomApplicationException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}