using System.Runtime.Serialization;

namespace ElevatorSystem.Domain;

[Serializable]
public class ElevatorException : Exception
{
    protected ElevatorException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext) { }

    public ElevatorException(string message) : base(message) { }
}