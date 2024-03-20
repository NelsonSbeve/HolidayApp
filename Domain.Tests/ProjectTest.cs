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
            Assert.Single(project.GetAssociations());
        }

        [Fact]
        public void AddColaborator_Should_Add_Correct_Colaborator_And_Dates()
        {
            // Arrange
            var project = new Project("ProjectName", DateOnly.MinValue, DateOnly.MaxValue);
            var mockColaborator = new Mock<IColaborator>();
            DateOnly startDate = new DateOnly(2024, 3, 18);
            DateOnly endDate = new DateOnly(2024, 3, 25);

            // Act
            project.AddColaborator(mockColaborator.Object, startDate, endDate);

            // Assert
            IAssociate associate = project.GetAssociations()[0];
            Assert.Equal(mockColaborator.Object, associate.Colaborator);
            Assert.Equal(startDate, associate.DateStart);
            Assert.Equal(endDate, associate.DateEnd);
        }

        [Fact]
        public void GetAssociations_Should_Return_Associations_List()
        {
                    // Arrange
            DateOnly initialDate = DateOnly.MinValue;
            DateOnly finalDate = DateOnly.MaxValue;

            var colabDouble1 = new Mock<IColaborator>();
            var colabDouble2 = new Mock<IColaborator>();

            string projectName = "ProjectName";
            Project project = new Project(projectName, initialDate, finalDate);
            project.AddColaborator(colabDouble1.Object, initialDate, finalDate);
            project.AddColaborator(colabDouble2.Object, initialDate, finalDate);

            // Act
            List<IAssociate> actualAssociations = project.GetAssociations();

            // Assert
            Assert.Equal(2, actualAssociations.Count); // Assuming 2 collaborators were added
        }
        

        [Fact]
        public void DateStart_Should_Return_Correct_Value()
        {
            // Arrange
            DateOnly expectedDateStart = new DateOnly(2024, 3, 18);
            DateOnly expectedDateEnd = new DateOnly(2024, 3, 18); // Example date
            Project project = new Project("new",expectedDateStart, expectedDateEnd);
            

            // Act
            DateOnly actualDateStart = project.DateStart;

            // Assert
            Assert.Equal(expectedDateStart, actualDateStart);
        }

        [Fact]
        public void DateEnd_Should_Return_Correct_Value()
        {
            // Arrange
            DateOnly expectedDateStart = new DateOnly(2024, 3, 25);
            DateOnly expectedDateEnd = new DateOnly(2024, 3, 25); // Example date
            Project project = new Project("new", expectedDateStart, expectedDateEnd );
            

            // Act
            DateOnly? actualDateEnd = project.DateEnd;

            // Assert
            Assert.Equal(expectedDateEnd, actualDateEnd);
        }
            
    }
}