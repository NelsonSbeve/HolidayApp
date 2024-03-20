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
        new Skills( Description, Level);
    }


    [Theory]
    [InlineData("Description of something", 0)]
    [InlineData("a", 10)]
    public void WhenPassingInvalidLevel_ThenThrowsException(string Description, int Level)
    {
        Assert.Throws<ArgumentException>(() =>new Skills(Description, Level));
    }


    [Theory]
    [InlineData("", 4)]
    [InlineData("abasdfsc 12", 3)]
    [InlineData(null, 2)]
    public void WhenPassingInvalidDescription_ThenThrowsException(string Description, int Level)
    {
        Assert.Throws<ArgumentException>(() =>new Skills(Description, Level));
    }

    }
}