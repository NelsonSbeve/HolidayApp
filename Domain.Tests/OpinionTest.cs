using Xunit;

namespace Domain.Tests
{
    public class OpinionTests
    {
        [Fact]
        public void ValidDescription_ShouldNotThrowException()
        {
            // Arrange
            var date = DateTime.Now;
            var decision = true;
            var description = "Sample opinion description";
            Mock<IColaborator> colabDouble = new Mock<IColaborator>();
            var colaborator = colabDouble.Object;

            // Act
            var opinion = new Opinion(date, decision, description, colaborator);

            // Assert
            Assert.Equal(date, opinion.Date);
            Assert.Equal(decision, opinion.Decision);
            Assert.Equal(description, opinion.Description);
            Assert.Equal(colaborator, opinion.colaborator);

        }
        
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Opinion_Constructor_InvalidDescription_ThrowsArgumentNullException(
            string description)
        {
            // Arrange
            DateTime date = DateTime.Now;
            bool decision=true;
            Mock<IColaborator> colabDouble = new Mock<IColaborator>();
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var opinion = new Opinion(date, decision, description, colabDouble.Object);
            });
        }

        [Fact]
        public void WhenNullColaborator_ThrowsArgumentNullException()
        {
            // Arrange
            DateTime date = DateTime.Now;
            bool decision=true;
            string description = "Test";
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                var opinion = new Opinion(date, decision, description, null);
            });
        }

        
    }
}
