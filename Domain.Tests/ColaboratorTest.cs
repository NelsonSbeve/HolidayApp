namespace Domain.Tests;

public class ColaboradorTest
{
    [Theory]
    [InlineData("Catarina Moreira", "catarinamoreira@email.pt")]
    [InlineData("a", "catarinamoreira@email.pt")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "catarinamoreira@email.pt")]
    public void WhenPassingCorrectData_ThenColaboradorIsInstantiated(string strName, string strEmail)
    {
        Colaborator colaborator = new Colaborator( strName, strEmail);

        Assert.Equal(strName, colaborator.GetName());  //não percebi este exemplo, dado que você diz que nao podemos fazer gets diretos a campos privados sem fazer validações mas foi exatamente o que fez aqui.
    }

    [Theory]
    [InlineData("", "catarinamoreira@email.pt")]
    [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "catarinamoreira@email.pt")]
    [InlineData("abasdfsc 12", "catarinamoreira@email.pt")]
    //[InlineData("     ", "catarinamoreira@email.pt")]
    [InlineData("kasdjflkadjf lkasdfj laksdjf alkdsfjv alkdsfjv asldkfj asldkfvj asdlkvj asdlkfvj asdlkfvj asdflkfvj asfldkjfv jasdflkvj lasf", "catarinamoreira@email.pt")]
    [InlineData(null, "catarinamoreira@email.pt")]
    public void WhenPassingInvalidName_ThenThrowsException(string strName, string strEmail)
    {
        // arrange

        // assert
        Assert.Throws<ArgumentException>(() =>
        
            // act
            new Colaborator(strName, strEmail)
        );
       
        
    }

    [Theory]
    [InlineData("Catarina Moreira", "")]
    [InlineData("Catarina Moreira", null)]
    [InlineData("Catarina Moreira", "catarinamoreira.pt")]
    public void WhenPassingInvalidEmail_ThenThrowsException(string strName, string strEmail)
    {
        // assert
        Assert.Throws<ArgumentException>(() =>
        
            // act
            new Colaborator(strName, strEmail)
        );
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(null, null)]
    [InlineData("", null)]
    [InlineData(null, "")]
    [InlineData("", "catarinamoreira.pt")]
    public void WhenPassingInvalidNameAndInvalidEmail_Thenz(string strName, string strEmail)
    {
        Assert.Throws<ArgumentException>(() => new Colaborator(strName, strEmail));
    }
}