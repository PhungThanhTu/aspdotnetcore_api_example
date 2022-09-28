using System.Runtime.Serialization;

namespace BLL.Exceptions;

[Serializable]
public class NotAllowedException : BaseCustomApplicationException
{
    public NotAllowedException()
    {
    }

    public NotAllowedException(string message)
        : base(message)
    {
    }

    public NotAllowedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    // Without this constructor, deserialization will fail
    protected NotAllowedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}