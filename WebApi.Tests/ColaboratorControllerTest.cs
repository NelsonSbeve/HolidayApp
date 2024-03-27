

namespace WebApi.Tests
{


    public class ColaboratorControllerTest
    {
    ///////////////////////////////////////////////
    ///

    [Fact]
    public async Task CreateColaborator_ReturnsCreatedColaborator()
    {
        // Arrange
        var collaboratorData = new List<Colaborator>();
        var mockDbSet = collaboratorData.AsQueryable().BuildMockDbSet();
        var options = new DbContextOptionsBuilder<ColaboratorContext>()
        .UseInMemoryDatabase(databaseName: "TestDatabase")
        .Options;

        var mockContext = new Mock<TestColaboratorContext>(options);
        mockContext.Setup(c => c.Colaborators).Returns(mockDbSet.Object);

        mockContext.Setup(c => c.Colaborators).Returns(mockDbSet.Object);
        mockDbSet.Setup(d => d.Add(It.IsAny<Colaborator>())).Callback((Colaborator collaborator) => collaboratorData.Add(collaborator));
        mockContext.Setup(c => c.SaveChangesAsync(default)).Returns(Task.FromResult(1));

        var controller = new ColaboratorController(mockContext.Object);

        // Act
        var result = await controller.CreateColaborator("John Doe", "john.doe@example.com");

        // Assert
        var createdResult = Assert.IsType<OkObjectResult>(result.Result);
        var createdColaborator = Assert.IsType<Colaborator>(createdResult.Value);
        Assert.Equal("John Doe", createdColaborator._strName);
        Assert.Equal("john.doe@example.com", createdColaborator._strEmail);
    }


    // [Fact]
    // public async Task GetColaboratorByName_ReturnsColaboratorByName()
    // {
    //     // Arrange
    //     var options = new DbContextOptionsBuilder<ColaboratorContext>()
    //         .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique database name to avoid conflicts
    //         .Options;

    //     // Seed the in-memory database with test data
    //     using (var context = new ColaboratorContext(options))
    //     {
    //         context.Colaborators.Add(new Colaborator("John Doe", "john.doe@example.com"));
    //         context.Colaborators.Add(new Colaborator("Jane Smith", "jane.smith@example.com"));
    //         context.SaveChanges();
    //     }

    //     // Use the in-memory database context for testing
    //     using (var context = new ColaboratorContext(options))
    //     {
    //         var controller = new ColaboratorController(context);

    //         // Act
    //         var result = await controller.GetColaboratorByName("John Doe");

    //         // Assert
    //         var actionResult = Assert.IsType<OkObjectResult>(result.Result);
    //         var collaborator = Assert.IsType<Colaborator>(actionResult.Value);
    //         Assert.Equal("John Doe", collaborator._strName);
    //         Assert.Equal("john.doe@example.com", collaborator._strEmail);
    //     }
   
    // }
    [Fact]
    public async Task GetColaboratorById_ReturnsColaboratorById()
    {
        // Arrange

        var options = new DbContextOptionsBuilder<ColaboratorContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique database name to avoid conflicts
            .Options;

        using (var context = new ColaboratorContext(options))
            {
                context.Colaborators.Add(new Colaborator("John Doe", "john.doe@example.com"));
                context.Colaborators.Add(new Colaborator("Jane Smith", "jane.smith@example.com"));
                context.SaveChanges();
            }

        using (var context = new ColaboratorContext(options))
        {
            var controller = new ColaboratorController(context);

            // Act
            var result = await controller.GetColaboratorById(1);

            // Assert
            var actionResult = Assert.IsType<OkObjectResult>(result.Result);
            var collaborator = Assert.IsType<Colaborator>(actionResult.Value);

            Assert.Equal(1, collaborator.Id);
            Assert.Equal("John Doe", collaborator._strName);
            Assert.Equal("john.doe@example.com", collaborator._strEmail);
        } 
            
    }


    [Fact]
    public async Task PutColaborator_UpdatesExistingColaborator()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ColaboratorContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using (var context = new ColaboratorContext(options))
        {
            // Add a collaborator to the database
            var existingColaborator = new Colaborator("John Doe", "john.doe@example.com");
            context.Colaborators.Add(existingColaborator);
            context.SaveChanges();
        }

        using (var context = new ColaboratorContext(options))
        {
            var controller = new ColaboratorController(context);

            // Prepare the updated collaborator data
            var updatedColaboratorData = new Colaborator("Jane Smith", "jane.smith@example.com");

            // Act: Call the PutColaborator method to update the collaborator
            var result = await controller.PutColaborator(1, updatedColaboratorData._strName, updatedColaboratorData._strEmail);

            // Assert
            var actionResult = Assert.IsType<NoContentResult>(result);

            // Verify that the collaborator was updated
            using (var verificationContext = new ColaboratorContext(options))
            {
                var updatedColaborator = await verificationContext.Colaborators.FindAsync(1);
                Assert.NotNull(updatedColaborator);
                Assert.Equal("Jane Smith", updatedColaborator._strName);
                Assert.Equal("jane.smith@example.com", updatedColaborator._strEmail);
            }
        }
    }


    [Fact]
    public async Task DeleteColaborator_ReturnsNoContentResult()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<ColaboratorContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using (var context = new ColaboratorContext(options))
        {
            context.Colaborators.Add(new Colaborator ("John Doe","john.doe@example.com" ));
            await context.SaveChangesAsync();
        }

        using (var context = new ColaboratorContext(options))
        {
            var controller = new ColaboratorController(context);

            // Act
            var result = await controller.DeleteColaborator(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }


    }
}