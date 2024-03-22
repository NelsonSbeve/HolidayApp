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





    

    }
}