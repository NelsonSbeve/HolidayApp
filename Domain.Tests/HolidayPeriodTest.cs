using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Tests
{

    public class HolidayPeriodTest
    {
        [Theory]
        [InlineData(2023, 12, 1, 2023, 12, 31)] 
        public void Constructor_WithValidStartAndEndDate_CreatesInstance(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            // Arrange
            var startDate = new DateOnly(startYear, startMonth, startDay);
            var endDate = new DateOnly(endYear, endMonth, endDay);
 
            // Act
            var holidayPeriod = new HolidayPeriod(startDate, endDate);
 
            // Assert
            Assert.Equal(startDate, holidayPeriod._startDate);
            Assert.Equal(endDate, holidayPeriod._endDate);
        }
 
        [Theory]
        [InlineData(2023, 12, 31, 2023, 12, 1)] 
        [InlineData(2023, 12, 31, 2023, 12, 31)] 
        public void Constructor_WithInvalidStartOrEndDate_ThrowsArgumentException(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            // Arrange
            var startDate = new DateOnly(startYear, startMonth, startDay);
            var endDate = new DateOnly(endYear, endMonth, endDay);
 
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new HolidayPeriod(startDate, endDate));
        }

        [Fact]

        public void WhenGetStartDate_ThenReturnsStartDate(){

            DateOnly initialDate = DateOnly.MinValue;
            DateOnly FinalDate = DateOnly.MaxValue;

            HolidayPeriod holidayP = new HolidayPeriod(initialDate, FinalDate);

            var startDate = holidayP.getStartDate();
            var endDate = holidayP.getEndDate();

            Assert.Equal(holidayP._startDate, startDate);
            Assert.Equal(holidayP._endDate, endDate);
        }

        // [Theory]
        // [InlineData("2023-10-01", "2023-10-15")]
        // public void WhenRequestingGetPeriodoFerias_ThenReturnList(string inicio, string fim)
        // {
        //     // arrange
        //     DateOnly dataInicio = DateOnly.Parse(inicio);
        //     DateOnly dataFim = DateOnly.Parse(fim);
    
        //     Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        //     Holiday holiday = new Holiday(colabDouble.Object);
        
        //     List<HolidayPeriod> expected = new List<HolidayPeriod>()
        //     {
        //         new HolidayPeriod(new DateOnly(2023, 10, 01), new DateOnly(2023, 10, 15)),
        //     };
    
        //     Mock<HolidayPeriod> hpDouble1 = new Mock<HolidayPeriod>();
        
        //     Mock<HolidayPeriodFactory> hpFactoryDouble = new Mock<HolidayPeriodFactory>();
        //     hpFactoryDouble.Setup(hpF => hpF.NewHolidayPeriod(new DateOnly(2023, 10, 01), new DateOnly(2023, 10, 15))).Returns(hpDouble1.Object);
        
    
        //     holiday.AddHolidayPeriod(hpFactoryDouble.Object, new DateOnly(2023, 10, 01), new DateOnly(2023, 10, 15));  // dentro do periodo
        //     holiday.AddHolidayPeriod(hpFactoryDouble.Object, new DateOnly(2023, 01, 01), new DateOnly(2023, 01, 15));  // fora do periodo
    
        //     // act
        //     List<HolidayPeriod> actual = holiday.GetHolidayPeriod(dataInicio, dataFim);
    
    
        //     // assert
        //     Assert.Equivalent(expected, actual);
        // }
    }
}
