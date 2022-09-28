using System.Runtime.Serialization;

namespace BLL.Exceptions;

[Serializable]
public class UnauthorizedException : BaseCustomApplicationException
{
    public UnauthorizedException()
    { }

    public UnauthorizedException(string message) : base(message)
    {
    }

    public UnauthorizedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected UnauthorizedException(SerializationInfo info, StreamingContext context)
       : base(info, context)
    {
    }
}