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
        var FactoryHolidayPeriodMock = new Mock<IFactoryHolidayPeriod>();
        var holidayPeriodMock = new Mock<IHolidayPeriod>();
        FactoryHolidayPeriodMock.Setup(f => f.newHolidayPeriod(initialDate, FinalDate)).Returns(holidayPeriodMock.Object);

        //act
        IHolidayPeriod result = holiday.addHolidayPeriod(FactoryHolidayPeriodMock.Object, initialDate, FinalDate);

        //assert
        Assert.NotNull(result);
        Assert.Equal(result,holidayPeriodMock.Object);

    }

    [Fact]
    public void GetDaysofHolidaywhenDatesAreTheSame_ThenResultIsZero()
    {
        // arrange
        DateOnly initialDate = DateOnly.MinValue;
        DateOnly FinalDate = DateOnly.MaxValue;
        var returnDate = new DateOnly(2024, 1, 10);
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Mock<IHolidayPeriod> holidayPeriodMock = new Mock<IHolidayPeriod>();
        var holiday = new Holiday(colabDouble.Object );
        holidayPeriodMock.Setup(p => p.ValidateInitialDate(initialDate)).Returns(returnDate);
        holidayPeriodMock.Setup(p => p.ValidateFinalDate(FinalDate)).Returns(returnDate);


        //act
        int result = holiday.GetDaysOfHolidayInsidePeriod(initialDate, FinalDate);

        //assert
        Assert.Equal(0,result);
        

    }

    
    [Theory]
    [InlineData("2024-01-01", "2024-01-31", "2024-01-01", "2024-01-31", 31)] 
    public void GetDaysOfHolidayInsidePeriod_ReturnsCorrectNumberOfDays(string holidayStartString, string holidayEndString, string projectStartString, string projectEndString, int expectedDays)
    {
        // Arrange
        var holidayStart = DateOnly.Parse(holidayStartString);
        var holidayEnd = DateOnly.Parse(holidayEndString);
        var projectStart = DateOnly.Parse(projectStartString);
        var projectEnd = DateOnly.Parse(projectEndString);
        var colabMock = new Mock<IColaborator>();
        Mock<IHolidayPeriod> holidayPeriodMock = new Mock<IHolidayPeriod>();
        var FactoryHolidayPeriodMock = new Mock<IFactoryHolidayPeriod>();
        FactoryHolidayPeriodMock.Setup(f => f.newHolidayPeriod(holidayStart, holidayEnd)).Returns(holidayPeriodMock.Object);
        
        
        
        holidayPeriodMock.Setup(p => p.ValidateInitialDate(holidayStart)).Returns(holidayStart);
        holidayPeriodMock.Setup(p => p.ValidateFinalDate(holidayEnd)).Returns(holidayEnd);


        var holiday = new Holiday(colabMock.Object);

        // Add holiday period
        holiday.addHolidayPeriod(FactoryHolidayPeriodMock.Object, holidayStart, holidayEnd);

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
        // Arrange;
        var projectStart = new DateOnly(2024, 1, 10); 
        var projectEnd = new DateOnly(2024, 1, 10); 
        var colabMock = new Mock<IColaborator>();
        Mock<IHolidayPeriod> holidayPeriodMock = new Mock<IHolidayPeriod>();
        var FactoryHolidayPeriodMock = new Mock<IFactoryHolidayPeriod>();
        FactoryHolidayPeriodMock.Setup(f => f.newHolidayPeriod(projectStart, projectEnd)).Returns(holidayPeriodMock.Object);
        
        
        
        holidayPeriodMock.Setup(p => p.ValidateInitialDate(projectStart)).Returns(projectStart);
        holidayPeriodMock.Setup(p => p.ValidateFinalDate(projectEnd)).Returns(projectEnd);


        var holiday = new Holiday(colabMock.Object);

        // Add holiday period
        holiday.addHolidayPeriod(FactoryHolidayPeriodMock.Object, projectStart, projectEnd);
        
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
        Mock<IHolidayPeriod> holidayPeriodMock = new Mock<IHolidayPeriod>();

        var Start = new DateOnly(2024, 1, 1); 
        var End = new DateOnly(2024, 1, 10);

        var FactoryHolidayPeriodMock = new Mock<IFactoryHolidayPeriod>();
        FactoryHolidayPeriodMock.Setup(f => f.newHolidayPeriod(Start, End)).Returns(holidayPeriodMock.Object);

        holidayPeriodMock.Setup(p => p.getStartDate()).Returns(Start);
        holidayPeriodMock.Setup(p => p.getEndDate()).Returns(End);

        holiday.addHolidayPeriod(FactoryHolidayPeriodMock.Object,Start, End);
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
