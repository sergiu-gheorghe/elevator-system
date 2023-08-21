// See https://aka.ms/new-console-template for more information

using ElevatorSystem.Console;
using ElevatorSystem.Domain;

var elevatorPool = new ElevatorsPool();
elevatorPool.AddRange(new List<IElevator>()
{
    new Elevator("el_1", 5, new ElevatorScreen()),
    new Elevator("el_2",5, new ElevatorScreen()),
    new Elevator("el_3",5, new ElevatorScreen())
});

Console.WriteLine($"There are currently {elevatorPool.Count} elevators available");

while (true)
{
    Console.Write("Call the elevator, input floor number:");
    var floor = Console.ReadLine();

    if (!int.TryParse(floor, out var floorNumber))
    {
        Console.WriteLine("Floor number must be a number");
        break;
    }

    var elevator = elevatorPool.GetAvailableElevator(floorNumber);
    elevator.Call(floorNumber);
    elevator.Move();

    Console.Write("Set number of people to enter in elevator:");
    var people = Console.ReadLine();
    if (!int.TryParse(people, out var numberOfPeople))
    {
        Console.WriteLine("Number of people must be a number");
        break;
    }

    Console.Write("Set floor number to go:");
    floor = Console.ReadLine();
    if (!int.TryParse(floor, out floorNumber))
    {
        Console.WriteLine("Floor number must be a number");
        break;
    }

    var executionResult = elevator.Set(floorNumber, numberOfPeople);
    if (executionResult.IsSuccess)
    {
        elevator.Move();
    }
    else
    {
        Console.WriteLine(executionResult.Message);
    }
    
    Console.Write("Do you want to continue y/n?");
    var input = Console.ReadLine();
    if (input == "n")
    {
        Console.WriteLine("Exiting the application...");
        break;
    }
}


