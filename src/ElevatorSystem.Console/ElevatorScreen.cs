using ElevatorSystem.Domain;

namespace ElevatorSystem.Console;

public class ElevatorScreen : IElevatorScreen
{
    public void Display(ElevatorStatus elevatorStatus)
    {
        System.Console.ForegroundColor = elevatorStatus.IsMoving ? ConsoleColor.White : ConsoleColor.Red;
        System.Console.WriteLine(
            $"Elevator: {elevatorStatus.ElevatorName}, " +
            $"Floor: {elevatorStatus.CurrentFloor}, " +
            $"Moving: {elevatorStatus.MovingDirection}, " +
            $"People in elevator: {elevatorStatus.NumberOfPeopleInElevator}, " +
            $"Getting off the elevator: {elevatorStatus.NumberOfPeopleLeavingElevator} ");

        System.Console.ForegroundColor = ConsoleColor.White;
    }
}