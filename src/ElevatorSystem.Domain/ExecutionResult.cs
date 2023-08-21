namespace ElevatorSystem.Domain;

public class ExecutionResult
{
    public bool IsSuccess { get; private init; }
    public string? Message { get; private init; }

    private ExecutionResult() { }
    
    public static ExecutionResult Success()
    {
        return new ExecutionResult { IsSuccess = true };
    }
    
    public static ExecutionResult Failed(string message)
    {
        return new ExecutionResult { Message = message};
    }
}