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
            Assert.Throws<ArgumentNullException>(() => project.AddColaborator(null, DateOnly.MinValue, DateOnly.MaxValue));
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




            
    }
}