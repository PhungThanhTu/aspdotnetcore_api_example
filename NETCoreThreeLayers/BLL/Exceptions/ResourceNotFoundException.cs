using System.Runtime.Serialization;

namespace BLL.Exceptions;

[Serializable]
public class ResourceNotFoundException : BaseCustomApplicationException
{
    public ResourceNotFoundException()
    {
    }

    public ResourceNotFoundException(string message)
        : base(message)
    {
    }

    public ResourceNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    // Without this constructor, deserialization will fail
    protected ResourceNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}