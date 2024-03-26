using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Tests
{
public class TrainingTest
    {
        [Theory]
        [InlineData(null)]
        public void WhenPassingInvalidColaborator_ThenThrowsException(IColaborator colab)
        {
            // Arrange
            string description = "Valid Description";
 
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Training(description, colab!));
            Assert.Equal("Invalid argument: colaborator must be non null", ex.Message);
        }
 
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void WhenPassingInvalidDescription_ThenThrowsException(string description)
        {
            // Arrange
            var colabMock = new Mock<IColaborator>();
 
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Training(description!, colabMock.Object));
            Assert.Equal("Invalid description.", ex.Message);
        }

        [Theory]
        [InlineData("A valid description")]
        [InlineData("Another valid description")]
        public void WhenPassingValidDescriptionAndColaborator_ThenCreatesInstanceSuccessfully(string validDescription)
        {
            // Arrange
            var colabMock = new Mock<IColaborator>();
 
            // Act
            var training = new Training(validDescription, colabMock.Object);
 
            // Assert
            Assert.NotNull(training);
            Assert.Equal(validDescription, training.Description);
        }
    }
}