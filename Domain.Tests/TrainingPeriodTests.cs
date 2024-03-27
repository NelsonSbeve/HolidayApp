using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Tests.TestResults
{
public class TrainingPeriodTests
    {
        [Theory]
        [InlineData(2024, 1, 1, 2024, 1, 31)]
        public void Constructor_ValidStartAndEndDate_Instantiates(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            // Arrange
            var startDate = new DateOnly(startYear, startMonth, startDay);
            var endDate = new DateOnly(endYear, endMonth, endDay);
 
            // Act
            var trainingPeriod = new TrainingPeriod(startDate, endDate);
            var endD = trainingPeriod.getEndDate();
            var startD = trainingPeriod.getStartDate();
 
            // Assert
            Assert.NotNull(trainingPeriod);
            Assert.Equal(endDate, endD);
            Assert.Equal(startDate, startD);
        }
 
        [Theory]
        [InlineData(2024, 1, 31, 2024, 1, 1)]
        [InlineData(2024, 1, 31, 2024, 1, 31)]
        public void Constructor_InvalidStartOrEndDate_ThrowsArgumentException(int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {
            // Arrange
            var startDate = new DateOnly(startYear, startMonth, startDay);
            var endDate = new DateOnly(endYear, endMonth, endDay);
 
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new TrainingPeriod(startDate, endDate));
            Assert.Equal("invalid arguments: start date >= end date.", ex.Message);
        }


    }
}