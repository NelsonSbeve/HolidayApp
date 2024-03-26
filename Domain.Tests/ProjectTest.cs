using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Tests
{
    public class ProjectTest
    {
        [Fact]
    
        public void WhenPassingAllParametersCorrect_ThenProjectIsInstantiated()
        {
            
            Mock<IProject> projDouble = new Mock<IProject>();

            Assert.NotNull(projDouble);
        }
        
        
        [Theory]
        [InlineData("Catarina Moreira", 2023, 12, 31, 2023, 12, 31)]
        [InlineData("a", 2022, 10, 30, 2023, 12, 31)]
        public void WhenPassingCorrectData_ThenProjectIsInstantiated(string _strName, int startYear, int startMonth, int startDay, int endYear, int endMonth, int endDay)
        {

            var startDate = new DateOnly(startYear, startMonth, startDay);
            var endDate = new DateOnly(endYear, endMonth, endDay);
            new Project( _strName, startDate, endDate);
        }

        [Fact]
        public void AddColaborator_Should_Throw_Exception_When_Colaborator_Is_Null()
        {
            // Arrange
            var project = new Project("ProjectName", DateOnly.MinValue, DateOnly.MaxValue);

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => project.AddColaborator(null, DateOnly.MinValue, DateOnly.MaxValue));
            Assert.Equal("colaborator cant be null.", ex.Message);

        }

        [Fact]
        public void AddColaborator_Should_Add_Colaborator_To_Associations_List()
        {
            // Arrange
            var project = new Project("ProjectName", DateOnly.MinValue, DateOnly.MaxValue);
            var mockColaborator = new Mock<IColaborator>();
            DateOnly startDate = DateOnly.MinValue;
            DateOnly endDate = DateOnly.MaxValue;

            // Act
            project.AddColaborator(mockColaborator.Object, startDate, endDate);

            // Assert
            Assert.True(project.IsColaboratorInProject(mockColaborator.Object));
        }

        [Theory]
        [InlineData("2024-01-01", "2024-01-15", "2023-12-15", "2024-01-25", "2024-01-01", "2024-01-15")] // Start date and end date completely inside project period
        [InlineData("2024-01-01", "2024-01-15", "2023-12-01", "2024-01-10", "2024-01-01", "2024-01-10")] // Start date and end date partially overlap with project period
        [InlineData("2024-01-01", "2024-01-15", "2023-12-01", "2024-01-25", "2024-01-01", "2024-01-15")] // Start date inside, end date outside project period
        [InlineData("2024-01-01", "2024-01-15", "2024-01-05", "2024-01-10", "2024-01-05", "2024-01-10")] // Start date and end date completely inside project period
        [InlineData("2024-01-01", "2024-01-15", "2024-01-05", "2024-01-20", "2024-01-05", "2024-01-15")] // Start date inside, end date outside project period
        public void GetPeriodInsideProject_ReturnsCorrectPeriod(string projectStartDateStr, string projectEndDateStr, string startDateStr, string endDateStr, string expectedStartDateStr, string expectedEndDateStr)
        {
            // Arrange
            DateOnly projectStartDate = DateOnly.Parse(projectStartDateStr);
            DateOnly projectEndDate = DateOnly.Parse(projectEndDateStr);
            DateOnly startDate = DateOnly.Parse(startDateStr);
            DateOnly endDate = DateOnly.Parse(endDateStr);
            DateOnly expectedStartDate = DateOnly.Parse(expectedStartDateStr);
            DateOnly expectedEndDate = DateOnly.Parse(expectedEndDateStr);

            var project = new Project("Test Project", projectStartDate, projectEndDate);

            // Act
            var result = project.GetPeriodInsideProject(startDate, endDate);

            // Assert
            Assert.Collection(result,
                date => Assert.Equal(expectedStartDate, date),
                date => Assert.Equal(expectedEndDate, date)
            );
        }

        [Fact]
        public void GetColabortorInPeriod_ReturnsCollaborators()
        {
            // Arrange
            var startDate = new DateOnly(2024, 1, 1);
            var endDate = new DateOnly(2024, 1, 31);

            var associateMock1 = new Mock<IAssociate>();
            var associateMock2 = new Mock<IAssociate>();

            var colaborator1Mock = new Mock<IColaborator>();
            var colaborator2Mock = new Mock<IColaborator>();

            // Set up return values for AddColaboratorIfInPeriod method of each associate mock
            associateMock1.Setup(a => a.AddColaboratorIfInPeriod(It.IsAny<List<IColaborator>>(), startDate, endDate)).Returns(new List<IColaborator> { colaborator1Mock.Object });
            associateMock2.Setup(a => a.AddColaboratorIfInPeriod(It.IsAny<List<IColaborator>>(), startDate, endDate)).Returns(new List<IColaborator> { colaborator2Mock.Object });

            var project = new Project("Test Project", startDate, endDate);
            project.AddColaborator(colaborator1Mock.Object, startDate, endDate);
            project.AddColaborator(colaborator2Mock.Object, startDate, endDate);

            // Act
            var result = project.GetColabortorInPeriod(startDate, endDate);

            // Assert
            Assert.Collection(result,
                item => Assert.Same(colaborator1Mock.Object, item),
                item => Assert.Same(colaborator2Mock.Object, item)
            );
        }

        [Fact]
        public void IsColaboratorInProject_ReturnsTrue_WhenColaboratorIsFound()
        {
            // Arrange
            var startDate = new DateOnly(2024, 1, 1);
            var endDate = new DateOnly(2024, 1, 31);
            var colaboratorMock = new Mock<IColaborator>();
            var associateMock = new Mock<IAssociate>();
            associateMock.Setup(a => a.IsColaboratorInProject(colaboratorMock.Object)).Returns(true);

            var project = new Project("Test Project", DateOnly.MinValue, DateOnly.MaxValue);
            project.AddColaborator(colaboratorMock.Object, startDate, endDate);

            // Act
            var result = project.IsColaboratorInProject(colaboratorMock.Object);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsColaboratorInProject_ReturnsFalse_WhenColaboratorIsNotFound()
        {
            // Arrange
            var startDate = new DateOnly(2024, 1, 1);
            var endDate = new DateOnly(2024, 1, 31);
            var colaboratorMock = new Mock<IColaborator>();
            var associateMock = new Mock<IAssociate>();
            associateMock.Setup(a => a.IsColaboratorInProject(colaboratorMock.Object)).Returns(false);

            var project = new Project("Test Project", DateOnly.MinValue, DateOnly.MaxValue);
            

            // Act
            var result = project.IsColaboratorInProject(colaboratorMock.Object);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsColaboratorInProject_ReturnsFalse_WhenAssociationsListIsEmpty()
        {
            // Arrange
            var colaboratorMock = new Mock<IColaborator>();
            var project = new Project("Test Project", DateOnly.MinValue, DateOnly.MaxValue);

            // Act
            var result = project.IsColaboratorInProject(colaboratorMock.Object);

            // Assert
            Assert.False(result);
        }




            
    }
}