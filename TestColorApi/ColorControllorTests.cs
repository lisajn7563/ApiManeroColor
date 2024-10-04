using ApiManeroColor.Contexts;
using ApiManeroColor.Controllers;
using ApiManeroColor.Entites;
using ApiManeroColor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace TestColorApi;

public class ColorControllorTests 
{
    private DbContextOptions<DataContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    [Fact]
    public async Task Create_ShouldAddColor_IfModelStateIsValid()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using (var context = new DataContext(options))
        {
            var controller = new ColorController(context);
            var model = new ColorRegistration
            {
                id = "3",
                colorTitle = "Green"
            };

            // Act
            var result = await controller.Create(model);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var createdColor = Assert.IsType<ColorEntity>(okResult.Value);
            Assert.Equal("Green", createdColor.colorTitle);

            // Verify that the color was added to the database
            var colorInDb = await context.Color.FindAsync("3");
            Assert.NotNull(colorInDb);
            Assert.Equal("Green", colorInDb.colorTitle);
        }
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequest_IfModelStateIsInvalid()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using (var context = new DataContext(options))
        {
            var controller = new ColorController(context);
            controller.ModelState.AddModelError("Error", "ModelState is invalid");

            var model = new ColorRegistration
            {
                id = "3",
                colorTitle = "Green"
            };

            // Act
            var result = await controller.Create(model);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }

    [Fact]
    public async Task GetAll_ShouldReturnAllColors()
    {
        // Arrange
        var options = CreateNewContextOptions();
        using (var context = new DataContext(options))
        {
            context.Color.AddRange(new List<ColorEntity>
                {
                    new ColorEntity { id = "1", colorTitle = "Red" },
                    new ColorEntity { id = "2", colorTitle = "Blue" }
                });
            context.SaveChanges();

            var controller = new ColorController(context);

            // Act
            var result = await controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var colorList = Assert.IsType<List<ColorEntity>>(okResult.Value);
            Assert.Equal(2, colorList.Count);
        }
    }
}

