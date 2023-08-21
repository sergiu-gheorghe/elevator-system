using ElevatorSystem.Domain;
using FluentAssertions;
using Moq;

namespace ElevatorSystem.Tests.Domain;

public class ElevatorPoolTests
{
    [Fact]
    public void ShouldGetAvailableElevator()
    {
        // Arrange
        var elevators = new List<IElevator>()
        {
            new Elevator("el_1", 5, Mock.Of<IElevatorScreen>()),
            new Elevator("el_2", 5, Mock.Of<IElevatorScreen>()),
            new Elevator("el_3", 5, Mock.Of<IElevatorScreen>())
        };
        
        elevators[0].Call(4);
        elevators[0].Move();
        elevators[1].Call(5);
        elevators[1].Move();
        elevators[2].Call(6);
        elevators[2].Move();
            
        var elevatorPool = new ElevatorsPool();
        elevatorPool.AddRange(elevators);
        
        // Act
        var elevator = elevatorPool.GetAvailableElevator(3);
        
        // Assert
        elevator.Name.Should().Be("el_1");
    }
}