namespace Domain.Tests;

public class HolidayTest
{

    [Fact]
    public void WhenPassingAColaborator_ThenHolidayIsInstantiated()
    {
        
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();

        new Holiday(colabDouble.Object);
  
    }


    [Fact]
    public void WhenPassingNullAsColaborator_ThenThrowsException()
    {
        var ex = Assert.Throws<ArgumentException>(() => new Holiday(null));
        Assert.Equal("Invalid argument: colaborator must be non null", ex.Message);
    }

    [Fact]
    public void WhenAddingNewPeriod_ThenPeriodIsAdded()
    {
        // arrange
        DateOnly initialDate = DateOnly.MinValue;
        DateOnly FinalDate = DateOnly.MaxValue;
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        var holiday = new Holiday(colabDouble.Object);

        //act
        HolidayPeriod result = holiday.addHolidayPeriod(initialDate, FinalDate);

        //assert
        Assert.NotNull(result);
        Assert.Equal(initialDate,result.getStartDate());
        Assert.Equal(FinalDate,result.getEndDate());

    }

    
    [Theory]
    [InlineData("2024-01-01", "2024-01-31", "2024-01-01", "2024-01-31", 31)] 
    [InlineData("2024-01-01", "2024-01-31", "2024-01-15", "2024-01-20", 6)] 
    [InlineData("2024-01-01", "2024-01-31", "2024-02-01", "2024-02-10", 0)]
    [InlineData("2024-01-01", "2024-01-31", "2023-12-15", "2024-02-10", 31)] 
    [InlineData("2024-01-01", "2024-01-31", "2023-12-15", "2024-01-10", 10)]
    [InlineData("2024-01-01", "2024-01-31", "2024-01-01", "2024-01-10", 10)] 
    [InlineData("2024-01-01", "2024-01-31", "2024-01-20", "2024-01-31", 12)] 
    public void GetDaysOfHolidayInsidePeriod_ReturnsCorrectNumberOfDays(string holidayStartString, string holidayEndString, string projectStartString, string projectEndString, int expectedDays)
    {
        // Arrange
        var holidayStart = DateOnly.Parse(holidayStartString);
        var holidayEnd = DateOnly.Parse(holidayEndString);
        var projectStart = DateOnly.Parse(projectStartString);
        var projectEnd = DateOnly.Parse(projectEndString);
        var colabMock = new Mock<IColaborator>();

        var holiday = new Holiday(colabMock.Object);

        // Add holiday period
        holiday.addHolidayPeriod(holidayStart, holidayEnd);

        // Act
        var result = holiday.GetDaysOfHolidayInsidePeriod(projectStart, projectEnd);

        // Assert
        Assert.Equal(expectedDays, result);
    }

    [Fact]
    public void GetDaysOfHolidayInsidePeriod_ReturnsZero_WhenNoHolidayPeriodsAdded()
    {
        // Arrange
        var colabMock = new Mock<IColaborator>();
        var holiday = new Holiday(colabMock.Object);
        var projectStart = new DateOnly(2024, 1, 1); 
        var projectEnd = new DateOnly(2024, 1, 10); 

        // Act
        var result = holiday.GetDaysOfHolidayInsidePeriod(projectStart, projectEnd);

        // Assert
        Assert.Equal(0, result);
    }
    [Fact]
    public void GetDaysOfHolidayInsidePeriod_ReturnsZero_WhenProjectPeriodHasZeroLength()
    {
        // Arrange
        var colabMock = new Mock<IColaborator>();
        var holiday = new Holiday(colabMock.Object);
        var projectStart = new DateOnly(2024, 1, 10); 
        var projectEnd = new DateOnly(2024, 1, 10); 

        // Act
        var result = holiday.GetDaysOfHolidayInsidePeriod(projectStart, projectEnd);

        // Assert
        Assert.Equal(0, result);
    }
    [Fact]
    public void GetColaboratorwithMoreThen_ReturnsColaboratorOrNull_WhenHolidayPeriodExceedsOrDoesNotExceedXDays()
    {
        // Arrange
        var colabMock = new Mock<IColaborator>();
        var holiday = new Holiday(colabMock.Object);

        var Start = new DateOnly(2024, 1, 1); 
        var End = new DateOnly(2024, 1, 10);

        holiday.addHolidayPeriod(Start, End);
        var xDays = 20; 
        var Xdays2= 5;
        var Xdays3 = 9;

        // Act
        var result = holiday.GetColaboratorwithMoreThen(xDays);
        var result2 = holiday.GetColaboratorwithMoreThen(Xdays2);
        var result3 = holiday.GetColaboratorwithMoreThen(Xdays3);

        // Assert
        Assert.Null(result);
        Assert.NotNull(result2);
        Assert.Null(result3);
    }
    [Theory]
    [InlineData(true)]
    [InlineData(false)] 
    public void IsColaboradorInHoliday_ReturnsExpectedValue(bool expectedResult)
    {
        // Arrange
        var colaboratorMock = new Mock<IColaborator>();
        var holiday = new Holiday(colaboratorMock.Object);

        
        colaboratorMock.Setup(c => c.Equals(It.IsAny<IColaborator>())).Returns(expectedResult);
        // Act
        var result = holiday.IsColaboradorInHoliday(colaboratorMock.Object);

        // Assert
        Assert.Equal(expectedResult, result);
    }


}
