namespace Domain.Tests;

public class HolidayTest
{

    [Fact]
    public void WhenPassingAColaborator_ThenHolidayIsInstantiated()
    {
        //Mock<Colaborator> colabDouble = new Mock<Colaborator>("a", "b@b.pt");
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();

        new Holiday(colabDouble.Object);

        // isto não é um tewste unitário a Holiday, porque não isola do Colaborator
        // Colaborator colab = new Colaborator("a", "a@b.c");
        // IColaborator colab = new Colaborator("a", "a@b.c");
        // new Holiday(colab);
    }


    [Fact]
    public void WhenPassingNullAsColaborator_ThenThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Holiday(null));
    }


    // [Fact]
    // public void WhenRequestingName_ThenReturnColaboratorName()
    // {
    //     // arrange
    //     string NOME = "nome";
    //     Mock<IColaborator> colabDouble = new Mock<IColaborator>();
    //     colabDouble.Setup(p => p.getName()).Returns(NOME);

    //     Holiday holiday = new Holiday(colabDouble.Object); // SUT/OUT

    //     // act
    //     string nameResult = holiday.getName();

    //     // assert
    //     Assert.Equal(NOME, nameResult);
    // }

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
        var result = holiday.GetHolidayPeriods();

        // Assert
        Assert.Equal(holidayPeriods, result);
    }

    [Fact]
    public void GetDaysOfHoliday_Should_Return_Correct_Number_Of_Days()
    {
        // Arrange
        DateOnly initialDate = DateOnly.MinValue;
        DateOnly FinalDate = DateOnly.MaxValue;
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Holiday holiday = new Holiday(colabDouble.Object);

        // Act
        int numberOfDays = holiday.GetDaysOfHoliday(initialDate, FinalDate);

        // Assert
        Assert.Equal(3652058, numberOfDays);
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
        holiday.addHolidayPeriod(DateOnly.MinValue, DateOnly.MaxValue); // Adding a period that does not match

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

    [Fact]
    public void GetColaborator_Should_Return_Correct_Colaborator()
    {
        // Arrange
        var mockColaborator = new Mock<IColaborator>();
        

        Holiday holiday = new Holiday(mockColaborator.Object);
        

        // Act
        var result = holiday.GetColaborator();

        // Assert
        Assert.Equal(mockColaborator.Object, result);
    }




}