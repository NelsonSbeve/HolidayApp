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
        Assert.Throws<ArgumentException>(() => new Holiday(null));
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
        Assert.Equal(initialDate,result._startDate);
        Assert.Equal(FinalDate,result._endDate);

    }

    [Fact]

    public void WhenGetPeriod_ThenReturnPeriod(){
        
        // arrange
        DateOnly initialDate = DateOnly.MinValue;
        DateOnly FinalDate = DateOnly.MaxValue;
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        var holiday = new Holiday(colabDouble.Object);
        List<HolidayPeriod> result = [holiday.addHolidayPeriod(initialDate, FinalDate)];


        

       //act
        List<HolidayPeriod> holidayPeriods = holiday.GetHolidayPeriod(initialDate, FinalDate);
       


        //assert
        Assert.Equivalent(result, holidayPeriods);

    }

    [Fact]
    public void GetHolidayPeriods_Should_Return_Correct_List()
    {


        //Arrange

        DateOnly initialDate = DateOnly.MinValue;
        DateOnly FinalDate = DateOnly.MaxValue;
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        var holiday = new Holiday(colabDouble.Object);
        List<HolidayPeriod> holidayPeriods = [holiday.addHolidayPeriod(initialDate, FinalDate)];

        

        // Act
        var result = holiday.GetHolidayPeriod(initialDate, FinalDate);

        // Assert
        Assert.Equal(holidayPeriods, result);
    }

    [Fact]
    public void GetHolidayPeriod_Should_Return_Empty_List_When_No_Periods_Exist()
    {
        // Arrange
        var mockColaborator = new Mock<IColaborator>();
        var holiday = new Holiday(mockColaborator.Object);

        // Act
        var result = holiday.GetHolidayPeriod(DateOnly.MinValue, DateOnly.MaxValue);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetHolidayPeriod_Should_Return_Empty_List_When_No_Periods_Match()
    {
        // Arrange
        var mockColaborator = new Mock<IColaborator>();
        var holiday = new Holiday(mockColaborator.Object);
        holiday.addHolidayPeriod(DateOnly.MinValue, DateOnly.MaxValue); 

        // Act
        var result = holiday.GetHolidayPeriod(new DateOnly(2024, 3, 18), new DateOnly(2024, 3, 25));

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetHolidayPeriod_Should_Return_Matching_Periods()
    {
        // Arrange
        var mockColaborator = new Mock<IColaborator>();
        var holiday = new Holiday(mockColaborator.Object);
        var startDate = new DateOnly(2024, 3, 18);
        var endDate = new DateOnly(2024, 3, 25);
        var matchingPeriod = new HolidayPeriod(startDate, endDate);
        var nonMatchingPeriod = new HolidayPeriod(DateOnly.MinValue, DateOnly.MaxValue);
        holiday.addHolidayPeriod(startDate, endDate);
        holiday.addHolidayPeriod(DateOnly.MinValue, DateOnly.MaxValue);

        // Act
        var result = holiday.GetHolidayPeriod(startDate, endDate);

        // Assert
        Assert.Single(result);
        Assert.Equivalent(matchingPeriod, result[0]);
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

        // Act
        var result = holiday.GetColaboratorwithMoreThen(xDays);
        var result2 = holiday.GetColaboratorwithMoreThen(Xdays2);

        // Assert
        Assert.Null(result);
        Assert.NotNull(result2);
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
