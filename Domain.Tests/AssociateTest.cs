using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Tests
{
    public class AssociateTest
    {
    [Fact]
    public void WhenPassingAColaboratorAndProject_ThenAssociateIsInstantiated()
    {
        DateOnly initialDate = DateOnly.MinValue;
        DateOnly FinalDate = DateOnly.MaxValue;
        
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Mock<IProject> projDouble = new Mock<IProject>();

        Associate associate = new Associate(colabDouble.Object, initialDate, FinalDate );

        Assert.NotNull(associate);

    }

    [Fact]
    public void WhenPassingNullAsColaborator_ThenThrowsException()
    {
        DateOnly initialDate = DateOnly.MinValue;
        DateOnly FinalDate = DateOnly.MaxValue;
        Mock<IProject> projDouble = new Mock<IProject>();
        Assert.Throws<ArgumentNullException>(() => new Associate(null, FinalDate, initialDate));
    }

    [Fact]
    public void IsColaboratorInProject_ReturnsFalse_WhenColaboratorIsDifferent()
    {
        // Arrange
        DateOnly initialDate = DateOnly.MinValue;
        DateOnly FinalDate = DateOnly.MaxValue;
        var colabDouble = new Mock<IColaborator>();
        var colabDouble2 = new Mock<IColaborator>();
        Associate associate = new Associate(colabDouble2.Object, initialDate, FinalDate );


        // Act
        var result = associate.IsColaboratorInProject(colabDouble.Object);

        // Assert
        Assert.False(result);
    }
    [Fact]
    public void AddColaboratorIfInPeriod_AddsColaboratorToList_WhenAssociateIsInPeriod()
    {
        // Arrange
        var startDate = new DateOnly(2024, 1, 1); 
        var endDate = new DateOnly(2024, 1, 31); 
        var colaborators = new List<IColaborator>();
        var mockColaborator = new Mock<IColaborator>();
        var associate = new Associate(mockColaborator.Object, startDate,endDate);


        // Act
        var result = associate.AddColaboratorIfInPeriod(colaborators, startDate, endDate);

        // Assert
        Assert.Single(result); 
        Assert.Contains(mockColaborator.Object, result); 
    }

    [Fact]
    public void AddColaboratorIfInPeriod_DoesNotAddColaboratorToList_WhenAssociateIsNotInPeriod()
    {
        // Arrange
        var startDate = new DateOnly(2024, 1, 1); 
        var endDate = new DateOnly(2024, 1, 31); 
        var colaborators = new List<IColaborator>();
        var mockColaborator = new Mock<IColaborator>();
        var associate = new Associate(mockColaborator.Object, startDate.AddDays(80),endDate.AddDays(90));
       

        // Act
        var result = associate.AddColaboratorIfInPeriod(colaborators, startDate, endDate);

        // Assert
        Assert.Empty(result); 
    }

    [Theory]
    [InlineData("2024-07-01", "2024-07-15")] // _startDate = dataInicio _endDate = dataFim
    [InlineData("2024-07-01", "2024-07-07")] //_startDate = dataInicio _endDate > dataFim
    [InlineData("2024-06-30", "2024-07-07")] //_startDate < dataInicio _endDate > dataInicio
    [InlineData("2024-07-07", "2024-07-15")] // _startDate < dataFim _endDate = dataFim
    [InlineData("2024-07-07", "2024-07-10")] // _startDate < dataFim _endDate > dataFim
    public void WhenRequestingIsAssociacaoEmPeriodo_ThenReturnTrue(string inicio, string fim)
    {
        // arrange
        DateOnly dataInicio = DateOnly.Parse(inicio);
        DateOnly dataFim = DateOnly.Parse(fim);

        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        DateOnly inicioAssociacao = new DateOnly(2024, 7, 1);
        DateOnly fimAssociacao = new DateOnly(2024, 7, 15);
        Associate associacao = new Associate(colabDouble.Object, inicioAssociacao, fimAssociacao);

        // act
        bool actual = associacao.IsAssociateInPeriod(dataInicio, dataFim);

        // assert
        Assert.True(actual);
    }

    [Theory]
    [InlineData("2024-01-01", "2024-01-10", "2024-01-05", "2024-01-15", true)] // Completely within the period
    [InlineData("2024-01-01", "2024-01-10", "2023-12-28", "2024-01-05", true)] // Start before, end within
    [InlineData("2024-01-01", "2024-01-10", "2024-01-05", "2024-01-20", true)] // Start within, end after
    [InlineData("2024-01-01", "2024-01-10", "2023-12-28", "2024-01-20", true)] // Completely outside the period
    [InlineData("2024-01-01", "2024-01-10", "2024-01-10", "2024-01-20", true)] // Start at the end of the period
    [InlineData("2024-01-01", "2024-01-10", "2023-12-28", "2024-01-10", true)] // End at the start of the period
    [InlineData("2024-01-01", "2024-01-10", "2023-12-28", "2023-12-31", false)] // Completely before the period
    [InlineData("2024-01-01", "2024-01-10", "2024-01-15", "2024-01-20", false)] // Completely after the period
    public void IsAssociateInPeriod_ReturnsExpectedResult(
        string startDateStr, string endDateStr, string testStartStr, string testEndStr, bool expectedResult)
    {
        // Arrange
        var startDate = DateOnly.Parse(startDateStr);
        var endDate = DateOnly.Parse(endDateStr);
        var testStart = DateOnly.Parse(testStartStr);
        var testEnd = DateOnly.Parse(testEndStr);
        var colabDouble = new Mock<IColaborator>();
        var associate = new Associate(colabDouble.Object,startDate, endDate);

        // Act
        var result = associate.IsAssociateInPeriod(testStart, testEnd);

        // Assert
        Assert.Equal(expectedResult, result);
    }





    

    }
}