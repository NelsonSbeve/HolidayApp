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

        Associate associate = new Associate(colabDouble.Object, projDouble.Object,initialDate, FinalDate );

        Assert.NotNull(associate);

    }

    [Fact]
    public void WhenPassingNullAsColaborator_ThenThrowsException()
    {
        DateOnly initialDate = DateOnly.MinValue;
        DateOnly FinalDate = DateOnly.MaxValue;
        Mock<IProject> projDouble = new Mock<IProject>();
        Assert.Throws<ArgumentNullException>(() => new Associate(null, projDouble.Object, FinalDate, initialDate));
    }

    [Fact]
    public void WhenPassingNullAsProject_ThenThrowsException()
    {
        DateOnly initialDate = DateOnly.MinValue;
        DateOnly FinalDate = DateOnly.MaxValue;
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Assert.Throws<ArgumentNullException>(() => new Associate(colabDouble.Object, null, initialDate, FinalDate));
    }


    
    
    [Fact]
    public void Colaborator_Property_Should_Return_Correct_Value()
    {
        // Arrange
        var mockColaborator = new Mock<IColaborator>();
        var mockProject = new Mock<IProject>();
       

        Associate associate = new Associate(mockColaborator.Object, mockProject.Object, default, default); // Initialize Associate

        // Act
        IColaborator actualColaborator = associate.Colaborator;

        // Assert
        Assert.Equal(mockColaborator.Object, actualColaborator);
    }

    [Fact]
    public void GetAssociation_Method_Should_Return_Correct_Values()
    {
        // Arrange
        var mockColaborator = new Mock<IColaborator>();
        var mockProject = new Mock<IProject>();
        var dateStart = new DateOnly(2024, 3, 20); // Example date start
        var dateEnd = new DateOnly(2024, 3, 25); // Example date end

        Associate associate = new Associate(mockColaborator.Object, mockProject.Object, dateStart, dateEnd); // Initialize Associate

        // Act
        var association = associate.GetAssociation();

        // Assert
        Assert.Equal(mockColaborator.Object, association.Item1);
        Assert.Equal(mockProject.Object, association.Item2);
        Assert.Equal(dateStart, association.Item3);
        Assert.Equal(dateEnd, association.Item4);
    }
    }
}