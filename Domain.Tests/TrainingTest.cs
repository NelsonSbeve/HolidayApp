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
        // [Theory]
        // [InlineData(null)]
        // public void WhenPassingInvalidColaborator_ThenThrowsException(IColaborator colab)
        // {
        //     // Arrange
        //     string description = "Valid Description";
 
        //     // Act & Assert
        //     var ex = Assert.Throws<ArgumentException>(() => new Training(description, colab!));
        //     Assert.Equal("Invalid argument: colaborator must be non null", ex.Message);
        // }
 
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("  ")]
        public void WhenPassingInvalidDescription_ThenThrowsException(string description)
        {
            // Arrange
            // var colabMock = new Mock<IColaborator>();
            Mock<List<ISkills>> doubleSkillsPrevias = new Mock<List<ISkills>>();
            Mock<List<ISkills>> doubleSkillsFinais = new Mock<List<ISkills>>();
 
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => new Training(description!, doubleSkillsPrevias.Object,doubleSkillsFinais.Object));
            Assert.Equal("Invalid description.", ex.Message);
        }

        [Theory]
        [InlineData("A valid description")]
        [InlineData("Another valid description")]
        public void WhenPassingValidDescriptionAndColaborator_ThenCreatesInstanceSuccessfully(string validDescription)
        {
            // Arrange
            // var colabMock = new Mock<IColaborator>();
            Mock<List<ISkills>> doubleSkillsPrevias = new Mock<List<ISkills>>();
            Mock<List<ISkills>> doubleSkillsFinais = new Mock<List<ISkills>>();
            
 
            // Act
            var training = new Training(validDescription, doubleSkillsPrevias.Object, doubleSkillsFinais.Object);
 
            // Assert
            Assert.NotNull(training);
            Assert.Equal(validDescription, training.Description);
        }

        [Fact]
        public void WhenAddingNewPeriod_ThenPeriodIsAdded()
        {
            // arrange
            DateOnly initialDate = DateOnly.MinValue;
            DateOnly FinalDate = DateOnly.MaxValue;
            // Mock<IColaborator> colabDouble = new Mock<IColaborator>();
             Mock<List<ISkills>> doubleSkillsPrevias = new Mock<List<ISkills>>();
            Mock<List<ISkills>> doubleSkillsFinais = new Mock<List<ISkills>>();
            var training = new Training("string",doubleSkillsPrevias.Object, doubleSkillsFinais.Object);

            //act
            TrainingPeriod result = training.addTrainingPeriod(initialDate, FinalDate);

            //assert
            Assert.NotNull(result);
            Assert.Equal(1, training.ListCount());
            Assert.Equal(initialDate,result.getStartDate());
            Assert.Equal(FinalDate,result.getEndDate());

        }
        // [Fact]
        // public void WhenRequestingAddTrainingPeriod_ThenFactoryIsCalled()
        // {
        //     // arrange
        //     string descricao = "teste";
        //     List<ISkills> cPrevias = [];
        //     List<ISkills> cFinais = [];

        //     DateOnly dataInicio = new DateOnly(2024, 1, 1);
        //     DateOnly dataFim = new DateOnly(2024, 3, 31);

        //     Mock<ITrainingPeriodFactory> pfFactoryDouble = new Mock<ITrainingPeriodFactory>();
        //     Mock<TrainingPeriod> pfDouble = new Mock<TrainingPeriod>(dataInicio, dataFim);

        //     pfFactoryDouble.Setup(pfF => pfF.NewTrainingPeriod(dataInicio, dataFim)).Returns(pfDouble.Object);

        //     Training training = new Training(descricao, cPrevias, cFinais);

        //     Mock<List<TrainingPeriod>> pflDouble = new Mock<List<ITrainingPeriod>>();
        //     typeof(Training).GetField("_periodosTraining", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(training, pflDouble.Object);

        //     // act
        //     training.addTrainingPeriod(pfFactoryDouble.Object, dataInicio, dataFim);

        //     // assert
        //     pfFactoryDouble.Verify(pfF => pfF.NewTrainingPeriod(It.IsAny<DateOnly>(), It.IsAny<DateOnly>()), Times.Once);
        //     Assert.Single(pflDouble.Object);
        // }


        [Fact]
        public void WhenRequestingAddSkillsFinal_ThenFactoryIsCalled()
        {
            // arrange
            string descricao = "teste";
            List<ISkills> cPrevias = [];
            List<ISkills> cFinais = [];

            string cDescricao = "skills";
            int nivel = 1;

            Mock<ISkillsFactory> cFactory = new Mock<ISkillsFactory>();
            Mock<Skills> skillsDouble = new Mock<Skills>(nivel, cDescricao);

            cFactory.Setup(cF => cF.NewSkills(nivel, descricao)).Returns(skillsDouble.Object);

            Training training = new Training(descricao, cPrevias, cFinais);

            Mock<List<ISkills>> cfDouble = new Mock<List<ISkills>>();
            typeof(Training).GetField("_skillssFinais", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(training, cfDouble.Object);

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
            List<ISkills> cPrevias = [];
            List<ISkills> cFinais = [];

            Mock<ISkillsFactory> cFactory = new Mock<ISkillsFactory>();
            Mock<Skills> skillsDouble = new Mock<Skills>(nivel, cDescricao);

            cFactory.Setup(x => x.NewSkills(It.Is<int>(nivel => nivel < 0 || nivel > 5), It.IsAny<string>()))
                .Throws<ArgumentException>();

            cFactory.Setup(x => x.NewSkills(It.IsAny<int>(), It.Is<string>(descricao => string.IsNullOrEmpty(descricao))))
                .Throws<ArgumentException>();

            Training training = new Training(descricao, cPrevias, cFinais);

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
            List<ISkills> cPrevias = [];
            List<ISkills> cFinais = [];

            string cDescricao = "skills";
            int nivel = 1;

            Mock<ISkillsFactory> cFactory = new Mock<ISkillsFactory>();
            Mock<Skills> skillsDouble = new Mock<Skills>(nivel, cDescricao);

            cFactory.Setup(cF => cF.NewSkills(nivel, descricao)).Returns(skillsDouble.Object);

            Training training = new Training(descricao, cPrevias, cFinais);

            Mock<List<ISkills>> cpDouble = new Mock<List<ISkills>>();
            typeof(Training).GetField("_skillssPrevias", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(training, cpDouble.Object);

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
            List<ISkills> cPrevias = [];
            List<ISkills> cFinais = [];

            Mock<ISkillsFactory> cFactory = new Mock<ISkillsFactory>();
            Mock<Skills> skillsDouble = new Mock<Skills>(nivel, cDescricao);

            cFactory.Setup(x => x.NewSkills(It.Is<int>(nivel => nivel < 0 || nivel > 5), It.IsAny<string>()))
                .Throws<ArgumentException>();

            cFactory.Setup(x => x.NewSkills(It.IsAny<int>(), It.Is<string>(descricao => string.IsNullOrEmpty(descricao))))
                .Throws<ArgumentException>();

            Training training = new Training(descricao, cPrevias, cFinais);

            // assert
            Assert.Throws<ArgumentException>(() =>

            // act
            training.AddPreviousSkills(cFactory.Object, nivel, cDescricao)

            );
        }

    }
}