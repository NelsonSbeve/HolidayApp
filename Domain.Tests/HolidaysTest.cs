using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Tests
{
    public class HolidaysTest
    {
        // [Fact]
        // public void AddHoliday_Should_Add_Holiday_To_Holidays_List()
        // {
        //     // Arrange
        //     Mock<IFactoryHoliday> holidayFactoryMock = new Mock<IFactoryHoliday>();
        //     var colabDouble = new Mock<IColaborator>().Object; 
        //     var holidays = new Holidays(holidayFactoryMock.Object);

        //     // Act
        //     holidays.addHoliday(colabDouble);

        //     // Assert
        //     var holidaysListProperty = typeof(Holidays).GetProperty("HolidaysList", BindingFlags.NonPublic | BindingFlags.Instance);
        //     var holidaysList = (List<IColaborator>)holidaysListProperty.GetValue(holidays);

        //     Assert.Contains(colabDouble, holidaysList);
        // }


        [Fact]
        public void GetDaysOfHolidayFromProjectOfColaborator_When_Colaborator_Not_In_Project_Should_Return_Zero()
        {
            // Arrange
            var project = new Mock<IProject>();
            project.Setup(p => p.IsColaboratorInProject(It.IsAny<IColaborator>())).Returns(false);
            var holidays = new Holidays(new Mock<IFactoryHoliday>().Object);

            // Act
            var result = holidays.GetDaysOfHolidayFromProjectOfColaborator(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 12), new Mock<IColaborator>().Object, project.Object);

            // Assert
            Assert.Equal(0, result);
        }
        [Fact]
        public void GetDaysOfHolidayFromProjectOfColaborator_When_Project_Period_Is_Null_Should_Return_Zero()
        {
            // Arrange
            var project = new Mock<IProject>();
            project.Setup(p => p.GetPeriodInsideProject(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns((List<DateOnly>)null);
            var holidays = new Holidays(new Mock<IFactoryHoliday>().Object);

            // Act
            var result = holidays.GetDaysOfHolidayFromProjectOfColaborator(new DateOnly(2024, 1, 1), new DateOnly(2024, 1, 12), new Mock<IColaborator>().Object, project.Object);

            // Assert
            Assert.Equal(0, result);
        }
                
        [Fact]
        public void GetDaysOfHolidayFromProjectOfColaborator_ColaboratorInProjectWithHolidays_ReturnsCorrectDays()
        {
            // Arrange
            var colabDouble = new Mock<IColaborator>();
            var projectMock = new Mock<IProject>();
            projectMock.Setup(p => p.IsColaboratorInProject(It.IsAny<IColaborator>())).Returns(true);
            projectMock.Setup(p => p.GetPeriodInsideProject(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(new List<DateOnly> { DateOnly.MinValue, DateOnly.MaxValue });

            var holidayMock = new Mock<IHoliday>();
            holidayMock.Setup(h => h.IsColaboradorInHoliday(It.IsAny<IColaborator>())).Returns(true);
            holidayMock.Setup(h => h.GetDaysOfHolidayInsidePeriod(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(5);

            var factoryMock = new Mock<IFactoryHoliday>();
            factoryMock.Setup(f => f.newHoliday(colabDouble.Object)).Returns(holidayMock.Object);


            var holidays = new Holidays(factoryMock.Object); 
            holidays.addHoliday(colabDouble.Object); // Add mocked holiday to the holidays list
            var result = holidays.GetDaysOfHolidayFromProjectOfColaborator(DateOnly.MinValue, DateOnly.MaxValue, null, projectMock.Object);

            // Assert
            Assert.Equal(5, result);
        }

        [Fact]
        public void GetDaysOfHolidayFromProjectOfAll_ReturnsTotalHolidayDays()
        {
            // Arrange
            var startDate = new DateOnly(2024, 1, 1);
            var endDate = new DateOnly(2024, 1, 31);
            var projectMock = new Mock<IProject>();
            var colaborator1Mock = new Mock<IColaborator>();
            var colaborator2Mock = new Mock<IColaborator>();
            var factoryMock = new Mock<IFactoryHoliday>();
          
           
            projectMock.Setup(p => p.IsColaboratorInProject(It.IsAny<IColaborator>())).Returns(true);
            projectMock.Setup(p => p.GetPeriodInsideProject(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(new List<DateOnly> { DateOnly.MinValue, DateOnly.MaxValue });

            // Set up return values for the collaborators
            var colaborators = new List<IColaborator> { colaborator1Mock.Object, colaborator2Mock.Object };
            projectMock.Setup(p => p.GetColabortorInPeriod(startDate, endDate)).Returns(colaborators);

            // Set up return values for the holidays
            var holiday1Mock = new Mock<IHoliday>();
            holiday1Mock.Setup(h => h.IsColaboradorInHoliday(colaborator1Mock.Object)).Returns(true);
            holiday1Mock.Setup(h => h.GetDaysOfHolidayInsidePeriod(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(5);
            var holiday2Mock = new Mock<IHoliday>();
            holiday2Mock.Setup(h => h.IsColaboradorInHoliday(colaborator2Mock.Object)).Returns(true);
            holiday2Mock.Setup(h => h.GetDaysOfHolidayInsidePeriod(It.IsAny<DateOnly>(), It.IsAny<DateOnly>())).Returns(10);
           
            factoryMock.Setup(f => f.newHoliday(colaborator1Mock.Object)).Returns(holiday1Mock.Object);
            factoryMock.Setup(f => f.newHoliday(colaborator2Mock.Object)).Returns(holiday2Mock.Object);

            // Set up return values for GetDaysOfHolidayFromProjectOfColaborator method
            
            var holidaysInstance = new Holidays(factoryMock.Object);
            holidaysInstance.addHoliday(colaborator1Mock.Object);
            holidaysInstance.addHoliday(colaborator2Mock.Object);


            // Act
            var result = holidaysInstance.GetDaysOfHolidayFromProjectOfAll(startDate, endDate, projectMock.Object);

            // Assert
            Assert.Equal(5 + 10, result);
        }

        [Fact]
        public void GetColaboratorsWithMoreThen_ReturnsColaboratorsWithMoreThanXDays()
        {
            // Arrange
            var XDays = 5;
            var colaborator1Mock = new Mock<IColaborator>();
            var colaborator2Mock = new Mock<IColaborator>();
            var holiday1Mock = new Mock<IHoliday>();
            var holiday2Mock = new Mock<IHoliday>();

            // Set up return values for holiday methods
            holiday1Mock.Setup(h => h.GetColaboratorwithMoreThen(XDays)).Returns(colaborator1Mock.Object);
            holiday2Mock.Setup(h => h.GetColaboratorwithMoreThen(XDays)).Returns(colaborator2Mock.Object);

            var holidays = new List<IHoliday> { holiday1Mock.Object, holiday2Mock.Object };
            var factoryMock = new Mock<IFactoryHoliday>();

            // Set up factory mock
            factoryMock.Setup(f => f.newHoliday(colaborator1Mock.Object)).Returns(holiday1Mock.Object);
            factoryMock.Setup(f => f.newHoliday(colaborator2Mock.Object)).Returns(holiday2Mock.Object);

            var holidaysInstance = new Holidays(factoryMock.Object);

            // Inject holidays into the holidays instance
            holidaysInstance.addHoliday(colaborator1Mock.Object);
            holidaysInstance.addHoliday(colaborator2Mock.Object);

            // Act
            var result = holidaysInstance.GetColaboratorsWithMoreThen(XDays);

            // Assert
            Assert.Collection(result,
                item => Assert.Same(colaborator1Mock.Object, item),
                item => Assert.Same(colaborator2Mock.Object, item)
            );
        }

        [Fact]
        public void GetColaboratorsWithMoreThen_ReturnsNoColaborators()
        {
            // Arrange
            var XDays = 5;
            var colaborator1Mock = new Mock<IColaborator>();
            var colaborator2Mock = new Mock<IColaborator>();
            var holiday1Mock = new Mock<IHoliday>();
            var holiday2Mock = new Mock<IHoliday>();

            // Set up return values for holiday methods
            holiday1Mock.Setup(h => h.GetColaboratorwithMoreThen(2)).Returns(colaborator1Mock.Object);
            holiday2Mock.Setup(h => h.GetColaboratorwithMoreThen(3)).Returns(colaborator2Mock.Object);

            var holidays = new List<IHoliday> { holiday1Mock.Object, holiday2Mock.Object };
            var factoryMock = new Mock<IFactoryHoliday>();

            // Set up factory mock
            factoryMock.Setup(f => f.newHoliday(colaborator1Mock.Object)).Returns(holiday1Mock.Object);
            factoryMock.Setup(f => f.newHoliday(colaborator2Mock.Object)).Returns(holiday2Mock.Object);

            var holidaysInstance = new Holidays(factoryMock.Object);

            // Inject holidays into the holidays instance
            holidaysInstance.addHoliday(colaborator1Mock.Object);
            holidaysInstance.addHoliday(colaborator2Mock.Object);

            // Act
            var result = holidaysInstance.GetColaboratorsWithMoreThen(XDays);

            // Assert
            Assert.Empty(result);
        }

        // [Fact]
        // public void GetPeriodsOfHolidaysOfColaboratorInPeriod_ReturnsHolidayPeriods()
        // {
        //     // Arrange
        //     var startDate = new DateOnly(2024, 1, 1);
        //     var endDate = new DateOnly(2024, 1, 31);
        //     var colaboratorMock = new Mock<IColaborator>();
        //     var holiday1Mock = new Mock<IHoliday>();
        //     // var holiday2Mock = new Mock<IHoliday>();
        //     var factoryMock = new Mock<IFactoryHoliday>();
        //     z
        //     // var holidayPeriodMock = new Mock<HolidayPeriod>();

        //     // var holidays = new List<IHoliday> { holiday1Mock.Object, holiday2Mock.Object };

            
            
        //     // factoryMock.Setup(f => f.newHoliday(colaboratorMock.Object)).Returns(holiday2Mock.Object);
        //     holiday1Mock.Setup(h => h.IsColaboradorInHoliday(colaboratorMock.Object)).Returns(true);
        //     factoryMock.Setup(f => f.newHoliday(colaboratorMock.Object)).Returns(holiday1Mock.Object);
        //     holiday1Mock.Setup(h => h.GetHolidayPeriod(startDate,endDate)).Returns(new List<HolidayPeriod>(holidayPeriod1Mock.Object));
        //     // holiday2Mock.Setup(h => h.IsColaboradorInHoliday(colaboratorMock.Object)).Returns(true);
        //     // holiday2Mock.Setup(h => h.IsColaboradorInHoliday(colaboratorMock.Object)).Returns(true);
        //     var holidaysInstance = new Holidays(factoryMock.Object);

        //     // // Set up return values for GetHolidayPeriod method of each holiday mock
            

        //     // holidaysInstance.addHoliday(colaboratorMock.Object);
        //     holidaysInstance.addHoliday(colaboratorMock.Object);

        //     //GetHolidaysOfColaborator

        //     // Act
        //     var result = holidaysInstance.GetPeriodsOfHolidaysOfColaboratorInPeriod(colaboratorMock.Object, startDate, endDate);

        //     // Assert
        //     Assert.Collection(result,
        //         item => Assert.Same(holidayPeriod1Mock.Object, item),
        //         item => Assert.Same(holidayPeriod2Mock.Object, item)
        //     );
        // }



    }
}