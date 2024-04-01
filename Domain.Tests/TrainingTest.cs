using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Domain.Interfaces;
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
            Mock<List<ISkills>> doubleSkillsPrevias = new Mock<List<ISkills>>();
            Mock<List<ISkills>> doubleSkillsFinals = new Mock<List<ISkills>>();
 
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Training(description, colab!, doubleSkillsPrevias.Object, doubleSkillsFinals.Object));
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
            Mock<List<ISkills>> doubleSkillsPrevias = new Mock<List<ISkills>>();
            Mock<List<ISkills>> doubleSkillsFinals = new Mock<List<ISkills>>();
 
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Training(description!, colabMock.Object, doubleSkillsPrevias.Object,doubleSkillsFinals.Object));
            Assert.Equal("Invalid description.", ex.Message);
        }

        [Theory]
        [InlineData("A valid description")]
        [InlineData("Another valid description")]
        public void WhenPassingValidDescriptionAndColaborator_ThenCreatesInstanceSuccessfully(string validDescription)
        {
            // Arrange
            var colabMock = new Mock<IColaborator>();
            Mock<List<ISkills>> doubleSkillsPrevias = new Mock<List<ISkills>>();
            Mock<List<ISkills>> doubleSkillsFinals = new Mock<List<ISkills>>();
            
 
            // Act
            var training = new Training(validDescription, colabMock.Object, doubleSkillsPrevias.Object, doubleSkillsFinals.Object);
 
            // Assert
            Assert.NotNull(training);
            Assert.Equal(validDescription, training.Description);
        }


        [Fact]
        public void WhenRequestingAddTrainingPeriod_ThenFactoryIsCalled()
        {
            // arrange
            string descricao = "teste";
            var colabMock = new Mock<IColaborator>();
            List<ISkills> cPrevias = [];
            List<ISkills> cFinals = [];

            DateOnly dataInicio = new DateOnly(2024, 1, 1);
            DateOnly dataFim = new DateOnly(2024, 3, 31);

            Mock<ITrainingPeriodFactory> pfFactoryDouble = new Mock<ITrainingPeriodFactory>();
            Mock<TrainingPeriod> pfDouble = new Mock<TrainingPeriod>(dataInicio, dataFim);

            pfFactoryDouble.Setup(pfF => pfF.NewTrainingPeriod(dataInicio, dataFim)).Returns(pfDouble.Object);

            Training training = new Training(descricao, colabMock.Object, cPrevias, cFinals);

            Mock<List<ITrainingPeriod>> pflDouble = new Mock<List<ITrainingPeriod>>();
            typeof(Training).GetField("_trainingPeriods", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(training, pflDouble.Object);

            // act
            training.addTrainingPeriod(pfFactoryDouble.Object, dataInicio, dataFim);

            // assert
            pfFactoryDouble.Verify(pfF => pfF.NewTrainingPeriod(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()), Times.Once);
            Assert.Single(pflDouble.Object);
        }


        [Fact]
        public void WhenRequestingAddSkillsFinal_ThenFactoryIsCalled()
        {
            // arrange
            string descricao = "teste";
            var colabMock = new Mock<IColaborator>();
            List<ISkills> cPrevias = [];
            List<ISkills> cFinals = [];

            string cDescricao = "skills";
            int nivel = 1;

            Mock<ISkillsFactory> cFactory = new Mock<ISkillsFactory>();
            Mock<Skills> skillsDouble = new Mock<Skills>(nivel, cDescricao);

            cFactory.Setup(cF => cF.NewSkills(nivel, descricao)).Returns(skillsDouble.Object);

            Training training = new Training(descricao,colabMock.Object, cPrevias, cFinals);

            Mock<List<ISkills>> cfDouble = new Mock<List<ISkills>>();
            typeof(Training).GetField("_skillsFinal", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(training, cfDouble.Object);

            // act
            training.AddFinalSkills(cFactory.Object, nivel, cDescricao);

            // assert
            cFactory.Verify(cF => cF.NewSkills(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            Assert.Single(cfDouble.Object);
        }


        [Theory]
        [InlineData(6, "skills")]
        [InlineData(2, null)]
        [InlineData(-1, null)]
        public void WhenRequestingAddSkillsFinalWithInvalidParameters_ThenThrowException(int nivel, string cDescricao)
        {
            // arrange
            string descricao = "teste";
            var colabMock = new Mock<IColaborator>();
            List<ISkills> cPrevias = [];
            List<ISkills> cFinals = [];

            Mock<ISkillsFactory> cFactory = new Mock<ISkillsFactory>();
            Mock<Skills> skillsDouble = new Mock<Skills>(nivel, cDescricao);

            cFactory.Setup(x => x.NewSkills(It.Is<int>(nivel => nivel < 0 || nivel > 5), It.IsAny<string>()))
                .Throws<ArgumentException>();

            cFactory.Setup(x => x.NewSkills(It.IsAny<int>(), It.Is<string>(descricao => string.IsNullOrEmpty(descricao))))
                .Throws<ArgumentException>();

            Training training = new Training(descricao, colabMock.Object, cPrevias, cFinals);

            // assert
            Assert.Throws<ArgumentException>(() =>

            // act
            training.AddFinalSkills(cFactory.Object, nivel, cDescricao)

            );
        }


        [Fact]
        public void WhenRequestingAddSkillsPrevia_ThenFactoryIsCalled()
        {
            // arrange
            string descricao = "teste";
            var colabMock = new Mock<IColaborator>();
            List<ISkills> cPrevias = [];
            List<ISkills> cFinals = [];

            string cDescricao = "skills";
            int nivel = 1;

            Mock<ISkillsFactory> cFactory = new Mock<ISkillsFactory>();
            Mock<Skills> skillsDouble = new Mock<Skills>(nivel, cDescricao);

            cFactory.Setup(cF => cF.NewSkills(nivel, descricao)).Returns(skillsDouble.Object);

            Training training = new Training(descricao, colabMock.Object, cPrevias, cFinals);

            Mock<List<ISkills>> cpDouble = new Mock<List<ISkills>>();
            typeof(Training).GetField("_skillsPrevious", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(training, cpDouble.Object);

            // act
            training.AddPreviousSkills(cFactory.Object, nivel, cDescricao);

            // assert
            cFactory.Verify(cF => cF.NewSkills(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
            Assert.Single(cpDouble.Object);
        }

        [Theory]
        [InlineData(6, "skills")]
        [InlineData(2, null)]
        [InlineData(-1, null)]
        public void WhenRequestingAddSkillsPreviaWithInvalidParameters_ThenThrowException(int nivel, string cDescricao)
        {
            // arrange
            string descricao = "teste";
            var colabMock = new Mock<IColaborator>();
            List<ISkills> cPrevias = [];
            List<ISkills> cFinals = [];

            Mock<ISkillsFactory> cFactory = new Mock<ISkillsFactory>();
            Mock<Skills> skillsDouble = new Mock<Skills>(nivel, cDescricao);

            cFactory.Setup(x => x.NewSkills(It.Is<int>(nivel => nivel < 0 || nivel > 5), It.IsAny<string>()))
                .Throws<ArgumentException>();

            cFactory.Setup(x => x.NewSkills(It.IsAny<int>(), It.Is<string>(descricao => string.IsNullOrEmpty(descricao))))
                .Throws<ArgumentException>();

            Training training = new Training(descricao,colabMock.Object, cPrevias, cFinals);

            // assert
            Assert.Throws<ArgumentException>(() =>

            // act
            training.AddPreviousSkills(cFactory.Object, nivel, cDescricao)

            );
        }

    }
}