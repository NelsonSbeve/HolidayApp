using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Domain.Tests
{
    public class SkillsTest
    {
    [Theory]
    [InlineData("Description of something", 5)]
    [InlineData("a", 1)]
    public void WhenPassingCorrectData_ThenSkillIsInstantiated(string Description, int Level)
    {
        var skills = new Skills( Level, Description);
        Assert.Equal(Description, skills.getDescription());
    }

    [Theory]
    [InlineData(1)] // Valid level 1
    [InlineData(3)] // Valid level 3
    [InlineData(5)] // Valid level 5
    public void LevelRange_ValidLevels_ReturnsLevel(int level)
    {
        // Arrange
        var skills = new Skills(level, "descrição" );

        // Act
        var result = skills.LevelRange(level);

        // Assert
        Assert.Equal(level, result);
    }


    [Theory]
    [InlineData("Description of something", 0)]
    [InlineData("a", 10)]
    public void WhenPassingInvalidLevel_ThenThrowsException(string Description, int Level)
    {
        var ex = Assert.Throws<ArgumentException>(() =>new Skills( Level, Description));
        Assert.Equal("Invalid arguments.", ex.Message);
    }


    [Theory]
    [InlineData("", 4)]
    [InlineData("abasdfsc 12", 3)]
    [InlineData(null, 2)]
    public void WhenPassingInvalidDescription_ThenThrowsException(string Description, int Level)
    {
         var ex = Assert.Throws<ArgumentException>(() =>new Skills(Level, Description));
         Assert.Equal("Invalid arguments.", ex.Message);
    }

    



    }
}