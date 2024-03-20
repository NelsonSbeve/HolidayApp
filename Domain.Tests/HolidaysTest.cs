using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Tests
{
    public class HolidaysTest
    {
        [Fact]
        public void AddHoliday_Should_Add_Holiday_To_Holidays_List()
        {
            // Arrange
            var holiday = new Mock<IHoliday>(); 
            var holidays = new Holidays(new List<IHoliday>());
            

            // Act
            holidays.addHoliday(holiday.Object);

            // Assert
            Assert.Single(holidays.GetHolidays());
            
        }

        [Fact]
        public void GetHolidaysOfColaborator_Should_Return_Holidays_Of_Specified_Colaborator()
        {
            // Arrange
            var colaborator = new Mock<IColaborator>().Object;
            var holiday1 = new Holiday(colaborator);
            var holiday2 = new Holiday(colaborator);
            var holidaysList = new List<IHoliday>() { holiday1, holiday2 };
            var holidays = new Holidays(holidaysList);

            // Act
            var result = holidays.GetHolidaysOfColaborator(colaborator);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(holiday1, result);
            Assert.Contains(holiday2, result);
        }

        [Fact]
        public void GetHolidaysByColaborator_Should_Return_Holidays_By_Specified_Colaborator()
        {
            // Arrange
            var colaborator = new Mock<IColaborator>().Object;
            var colaborator2 = new Mock<IColaborator>().Object;
            var holiday1 = new Holiday(colaborator);
            var holiday2 = new Holiday(colaborator);
            var holiday3 = new Holiday(colaborator2);
            var holidaysList = new List<IHoliday>() { holiday1, holiday2, holiday3 };
            var holidays = new Holidays(holidaysList);

            // Act
            var result = holidays.GetHolidaysByColaborator(colaborator);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(holiday1, result);
            Assert.Contains(holiday2, result);
            Assert.DoesNotContain(holiday3, result);
        }

        [Fact]
        public void IsColaboratorInProject_Should_Return_True_If_Colaborator_Is_In_Project()
        {
            // Arrange
            DateOnly initialDate = DateOnly.MinValue;
            DateOnly FinalDate = DateOnly.MaxValue;
            var project = new Mock<IProject>();
            var colaborator = new Mock<IColaborator>().Object;
            var holidays = new Holidays(new List<IHoliday>());
            var associateMock = new Mock<IAssociate>();
            associateMock.SetupGet(a => a.Colaborator).Returns(colaborator);
            var associates = new List<IAssociate> { associateMock.Object };
            project.Setup(p => p.GetAssociations()).Returns(associates);

            // Act
            var result = holidays.IsColaboratorInProject(colaborator, project.Object);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsColaboratorInProject_Should_Return_False_If_Colaborator_Is_Not_In_Project()
        {
            // Arrange
            DateOnly initialDate = DateOnly.MinValue;
            DateOnly FinalDate = DateOnly.MaxValue;
            var project = new Mock<IProject>();
            var colaborator = new Mock<IColaborator>().Object;
            var holidays = new Holidays(new List<IHoliday>());
            var associateMock = new Mock<IAssociate>();
            var associations = new List<IAssociate> { associateMock.Object };
            project.Setup(p => p.GetAssociations()).Returns(associations);
           

            // Act
            var result = holidays.IsColaboratorInProject(colaborator, project.Object);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GetDaysOfHolidayFromProjectOfColaborator_Should_Return_Correct_Days()
        {
            // Arrange
            
            var colaborator = new Mock<IColaborator>().Object;
            var holiday1 = new Holiday(colaborator);
            var holiday2 = new Holiday(colaborator);
            var holidaysList = new List<IHoliday>() { holiday1, holiday2 };
            var holidays = new Holidays(holidaysList);
            DateOnly initialDate = DateOnly.MinValue;
            DateOnly FinalDate = DateOnly.MaxValue;
            var project = new Mock<IProject>();
            var associateMock = new Mock<IAssociate>();
            associateMock.SetupGet(a => a.Colaborator).Returns(colaborator);
            var associates = new List<IAssociate> { associateMock.Object };
            project.Setup(p => p.GetAssociations()).Returns(associates);


            // Act
            var result = holidays.GetDaysOfHolidayFromProjectOfColaborator(DateOnly.MinValue, DateOnly.MaxValue, colaborator, project.Object);

            // Assert
            Assert.Equal(0, result); // Assuming no periods intersect with project dates
        }

        [Fact]
        public void GetDaysOfHolidayFromProjectOfAll_Should_Return_Correct_Days()
        {
            // Arrange
            var colaborator = new Mock<IColaborator>().Object;
            var project = new Mock<IProject>();
            var holiday1 = new Holiday(colaborator);
            var holiday2 = new Holiday(colaborator);
            var holidaysList = new List<IHoliday>() { holiday1, holiday2 };
            var associateMock = new Mock<IAssociate>();
            associateMock.SetupGet(a => a.Colaborator).Returns(colaborator);
            var holidays = new Holidays(holidaysList);
            var associates = new List<IAssociate> { associateMock.Object };
            project.Setup(p => p.GetAssociations()).Returns(associates);

            // Act
            var result = holidays.GetDaysOfHolidayFromProjectOfAll(DateOnly.MinValue, DateOnly.MaxValue, project.Object);

            // Assert
            Assert.Equal(0, result); // Assuming no periods intersect with project dates
        }

        [Fact]
        public void GetDaysOfHolidayFromProjectOfAll_Should_Return_Zero_When_No_Holidays_Overlap_Project_Period()
        {
            // Arrange
            DateOnly projectStartDate = new DateOnly(2024, 1, 1);
            DateOnly projectEndDate = new DateOnly(2024, 1, 10);
            var project = new Mock<IProject>();
            var associateMock = new Mock<IAssociate>();
            var associates = new List<IAssociate> { associateMock.Object };
            project.Setup(p => p.GetAssociations()).Returns(associates);
            project.SetupGet(p => p._dateStart).Returns(projectStartDate);
            project.SetupGet(p => p._dateEnd).Returns(projectEndDate);

            var holidaysList = new List<IHoliday>(); // Empty list of holidays
            var holidays = new Holidays(holidaysList);

            // Act
            var result = holidays.GetDaysOfHolidayFromProjectOfAll(projectStartDate, projectEndDate, project.Object);

            // Assert
            Assert.Equal(0, result);
        }

        


        [Fact]
        public void GetDaysOfHolidayFromProjectOfAll_Should_Return_Correct_Days_When_Holidays_Overlap_Project_Period2()
        {
            // Arrange
            var project = new Mock<IProject>();
            project.Setup(p => p._dateStart).Returns(new DateOnly(2024, 1, 1));
            project.Setup(p => p._dateEnd).Returns(new DateOnly(2024, 1, 10));
            var associateMock = new Mock<IAssociate>();
            var associates = new List<IAssociate> { associateMock.Object };
            project.Setup(p => p.GetAssociations()).Returns(associates);
            var holiday1 = new Mock<IHoliday>();
            var holiday2 = new Mock<IHoliday>();
            var holidayPeriod1 = new HolidayPeriod(new DateOnly(2023, 12, 30), new DateOnly(2024, 1, 2));
            var holidayPeriod2 = new HolidayPeriod(new DateOnly(2024, 1, 3), new DateOnly(2024, 1, 5));
            holiday1.Setup(h => h.GetHolidayPeriods()).Returns(new List<HolidayPeriod> { holidayPeriod1 });
            holiday2.Setup(h => h.GetHolidayPeriods()).Returns(new List<HolidayPeriod> { holidayPeriod2 });
            var holidaysList = new List<IHoliday> { holiday1.Object, holiday2.Object };
            var holidays = new Holidays(holidaysList);

            // Act
            var result = holidays.GetDaysOfHolidayFromProjectOfAll(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 12), project.Object);

            // Assert
            Assert.Equal(5, result);
        }
    }
}