namespace ElevatorSystem.Domain;

public class Elevator : IElevator
{
    private readonly IElevatorScreen elevatorScreen;
    private readonly int maxNumberOfFloors;

    private Dictionary<int, int> elevatorCalls = new();

    public Elevator(string name, int maxNumberOfPeople, IElevatorScreen elevatorScreen)
    {
        Name = name;
        MaxAllowedPeople = maxNumberOfPeople;
        
        this.elevatorScreen = elevatorScreen;
        CurrentFloor = 1;
    }

    public string Name { get; }
    public int CurrentNumberOfPeople { get; private set; }
    public int MaxAllowedPeople { get; }
    public MovingDirection MovingDirection { get; private set; }
    public int CurrentFloor { get; private set; }

    public bool IsMoving => MovingDirection != MovingDirection.Stopped;

    // NOTE: an improvement to this will be adding moving direction similar to elevators that 
    // have two buttons up and down
    public void Call(int floorNumber)
    {
        elevatorCalls[floorNumber] = 0;
    }

    public ExecutionResult Set(int floorNumber, int numberOfPeople)
    {
        if (CurrentNumberOfPeople + numberOfPeople > MaxAllowedPeople)
        {
            return ExecutionResult.Failed($"Cannot move, number of people is more then {MaxAllowedPeople}");
        }
        
        CurrentNumberOfPeople += numberOfPeople;
        
        if (elevatorCalls.ContainsKey(floorNumber))
        {
            elevatorCalls[floorNumber] += numberOfPeople;
        }
        else
        {
            elevatorCalls[floorNumber] = numberOfPeople;
        }
        
        return ExecutionResult.Success();
    }
    
    public void Move()
    {
        CheckAllowedNumberOfPeople();

        while (elevatorCalls.Any())
        {
            var (floorNumber, numberOfPersons) = elevatorCalls.First();
            if (CurrentFloor < floorNumber)
            {
                MovingDirection = MovingDirection.Up;
                DisplayCurrentStatus();
                CurrentFloor++;
            }
            else if(CurrentFloor > floorNumber)
            {
                MovingDirection = MovingDirection.Down;
                DisplayCurrentStatus();
                CurrentFloor--;
            }
            else
            {
                elevatorCalls.Remove(floorNumber);
                MovingDirection = MovingDirection.Stopped;
                CurrentNumberOfPeople -= numberOfPersons;
                
                DisplayCurrentStatus(numberOfPersons);
            }
        }
    }
    
    private void CheckAllowedNumberOfPeople()
    {
        if (CurrentNumberOfPeople > MaxAllowedPeople)
        {
            throw new ElevatorException($"Cannot move, number of people is more then {MaxAllowedPeople}");
        }
    }
    
    private void DisplayCurrentStatus(int numberOfPersonsLeavingTheElevator = 0)
    {
        var elevatorStatus = new ElevatorStatus(Name, MovingDirection, CurrentFloor, CurrentNumberOfPeople, numberOfPersonsLeavingTheElevator);
        elevatorScreen.Display(elevatorStatus);
    }
}