using System.Runtime.Serialization;

namespace BLL.Exceptions;

[Serializable]
public class ResourceConflictException : BaseCustomApplicationException
{
    public ResourceConflictException()
    {
    }

    public ResourceConflictException(string message)
        : base(message)
    {
    }

    public ResourceConflictException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    // Without this constructor, deserialization will fail
    protected ResourceConflictException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}