namespace ElevatorSystem.Domain;

public record ElevatorStatus(
    string ElevatorName,
    MovingDirection MovingDirection, 
    int CurrentFloor, 
    int NumberOfPeopleInElevator, 
    int NumberOfPeopleLeavingElevator = 0)
{
    public bool IsMoving => MovingDirection != MovingDirection.Stopped;
}