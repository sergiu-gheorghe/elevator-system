namespace ElevatorSystem.Domain;

public interface IElevator
{
    public string Name { get; }
    int CurrentNumberOfPeople { get; }
    int MaxAllowedPeople { get; }
    MovingDirection MovingDirection { get; }
    int CurrentFloor { get; }
    bool IsMoving { get; }
    void Call(int floorNumber);
    ExecutionResult Set(int floorNumber, int numberOfPeople);
    void Move();
}