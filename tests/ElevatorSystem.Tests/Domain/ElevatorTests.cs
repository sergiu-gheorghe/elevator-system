using ElevatorSystem.Domain;
using FluentAssertions;
using Moq;

namespace ElevatorSystem.Tests.Domain;

public class ElevatorTests
{
    [Theory]
    [InlineData(1, 5, 6)]
    [InlineData(10, 4, 17)]
    public void ShouldMoveToCalledFloor(int currentFloor, int floorNumber, int expectedMovements)
    {
        // Arrange
        var elevatorScreen = new Mock<IElevatorScreen>();
        var elevator = new Elevator("test", 5, elevatorScreen.Object);
        elevator.Call(currentFloor);
        elevator.Move();

        // Act
        elevator.Call(floorNumber);
        elevator.Move();
        
        // Assert
        elevator.CurrentFloor.Should().Be(floorNumber);
        elevator.MovingDirection.Should().Be(MovingDirection.Stopped);
        elevator.CurrentNumberOfPeople.Should().Be(0);
        elevator.MaxAllowedPeople.Should().Be(5);
        
        elevatorScreen.Verify(
            x => x.Display(It.IsAny<ElevatorStatus>()), 
            Times.Exactly(expectedMovements));
    }
    
    [Fact]
    public void ShouldSetPeopleInElevator()
    {
        // Arrange
        var elevatorScreen = new Mock<IElevatorScreen>();
        var elevator = new Elevator("test", 5, elevatorScreen.Object);

        // Act
        var executionResult = elevator.Set(3, 4);
        
        // Assert
        executionResult.IsSuccess.Should().BeTrue();
        elevator.CurrentFloor.Should().Be(elevator.CurrentFloor);
        elevator.MovingDirection.Should().Be(MovingDirection.Stopped);
        elevator.CurrentNumberOfPeople.Should().Be(4);
        elevator.MaxAllowedPeople.Should().Be(5);
    }
    
    [Fact]
    public void ShouldNotAllowMoreThenMaxPeopleInElevator()
    {
        // Arrange
        var elevatorScreen = new Mock<IElevatorScreen>();
        var elevator = new Elevator("test", 5, elevatorScreen.Object);

        // Act
        var executionResult = elevator.Set(3, 6);
        
        // Assert
        executionResult.IsSuccess.Should().BeFalse();
        executionResult.Message.Should().Be("Cannot move, number of people is more then 5");
        elevator.CurrentFloor.Should().Be(elevator.CurrentFloor);
        elevator.MovingDirection.Should().Be(MovingDirection.Stopped);
        elevator.CurrentNumberOfPeople.Should().Be(0);
        elevator.MaxAllowedPeople.Should().Be(5);
    }
    
    [Fact]
    public void ShouldMovePeopleSetInElevator()
    {
        // Arrange
        var elevatorScreen = new Mock<IElevatorScreen>();
        var elevator = new Elevator("test", 5, elevatorScreen.Object);

        // Act
        var executionResult = elevator.Set(3, 4);
        
        // Assert
        elevator.CurrentNumberOfPeople.Should().Be(4);
        
        elevator.Move();
        
        executionResult.IsSuccess.Should().BeTrue();
        elevator.CurrentFloor.Should().Be(3);
        elevator.MovingDirection.Should().Be(MovingDirection.Stopped);
        elevator.CurrentNumberOfPeople.Should().Be(0);
        elevator.MaxAllowedPeople.Should().Be(5);
    }
}