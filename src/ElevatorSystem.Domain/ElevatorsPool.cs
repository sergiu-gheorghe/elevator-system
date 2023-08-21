namespace ElevatorSystem.Domain;

public class ElevatorsPool
{
    private List<IElevator> Elevators { get; } = new();

    public void AddRange(IEnumerable<IElevator> elevators)
    {
        foreach (var elevator in elevators)
        {
            Elevators.Add(elevator);
        }
    }

    public IElevator GetAvailableElevator(int floorNumber)
    {
        return Elevators.MinBy(e => Math.Abs(e.CurrentFloor - floorNumber));
    }

    public int Count => Elevators.Count;
}