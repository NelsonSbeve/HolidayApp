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
            var dateEnd = holidayPeriod.getEndDate();
            var dateStart = holidayPeriod.getStartDate();
 
            // Assert
            Assert.Equal(startDate, dateStart);
            Assert.Equal(endDate,dateEnd);
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
            var ex = Assert.Throws<ArgumentException>(() => new HolidayPeriod(startDate, endDate));
            Assert.Equal("invalid arguments: start date >= end date.", ex.Message);
        }

        [Fact]

        public void WhenGetStartDate_ThenReturnsStartDate(){

            DateOnly initialDate = DateOnly.MinValue;
            DateOnly FinalDate = DateOnly.MaxValue;

            HolidayPeriod holidayP = new HolidayPeriod(initialDate, FinalDate);

            var startDate = holidayP.getStartDate();
            var endDate = holidayP.getEndDate();

            Assert.Equal(initialDate, startDate);
            Assert.Equal(FinalDate, endDate);
        }

        [Theory]
        // [InlineData("2024-01-01", "2024-01-10", "2024-01-10")] // Given date before start date
        // [InlineData("2024-01-10", "2024-01-10", "2024-01-10")] // Given date same as start date
        [InlineData("2024-01-11", "2024-01-10", "2024-01-11")]
        // [InlineData("2024-01-10", "2024-01-11", "2024-01-11")]
        [InlineData("2024-01-15", "2024-01-10", "2024-01-15")] // Given date after start date

        public void ValidateInitialDate_ReturnsCorrectStartDate(string startDateStr, string givenDateStr, string expectedStartDateStr)
        {
            // Arrange
            var startDate = DateOnly.Parse(startDateStr);
            var givenDate = DateOnly.Parse(givenDateStr);
            var expectedStartDate = DateOnly.Parse(expectedStartDateStr);
            var holidayPeriod = new HolidayPeriod(startDate, DateOnly.MaxValue);

            // Act
            var result = holidayPeriod.ValidateInitialDate(givenDate);

            // Assert
            Assert.Equal(expectedStartDate, result);
            Assert.True(result >= startDate);
        }

        [Fact]
        public void ValidateInitialDate_ReturnsStartDate_WhenGivenDateEqualsStartDate()
        {
            // Arrange
            var startDate = new DateOnly(2024, 1, 1); // Example start date
            var holidayPeriod = new HolidayPeriod(startDate, DateOnly.MaxValue);

            // Act
            var result = holidayPeriod.ValidateInitialDate(startDate);

            // Assert
            Assert.Equal(startDate, result);

            // Additional assertion to kill the mutation
            Assert.True(result >= startDate, "Result must be greater than or equal to the start date.");
        }

        [Theory]
        [InlineData("2024-01-01", "2024-01-10", "2024-01-01")] // Given date before start date
        [InlineData("2024-01-10", "2024-01-10", "2024-01-10")] // Given date same as start date
        [InlineData("2024-01-11", "2024-01-10", "2024-01-10")]
        [InlineData("2024-01-10", "2024-01-11", "2024-01-10")]
        [InlineData("2024-01-15", "2024-01-10", "2024-01-10")] // Given date after start date
        public void ValidateFinalDate_ReturnsCorrectStartDate(string endDateStr, string givenDateStr, string expectedStartDateStr)
        {
            // Arrange
            var endDate = DateOnly.Parse(endDateStr);
            var givenDate = DateOnly.Parse(givenDateStr);
            var expectedStartDate = DateOnly.Parse(expectedStartDateStr);
            var holidayPeriod = new HolidayPeriod(DateOnly.MinValue, endDate);

            // Act
            var result = holidayPeriod.ValidateFinalDate(givenDate);

            // Assert
            Assert.Equal(expectedStartDate, result);
            Assert.True(result <= endDate);
        }//Mutação persiste independemente dos testes pois a validação >ou >= o resultado será o mesmo pois eu retornar 10-10-2024 do startDateou retornar 10-10-2024 do givenDate é o mesmo.
    }
}
