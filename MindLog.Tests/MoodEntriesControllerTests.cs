using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using MindLog.Controllers.Api;
using MindLog.Services.Interfaces;
using MindLog.Models.DTOs;

public class MoodEntriesApiControllerTests
{
    [Fact]
    public async Task GetAll_ReturnsOk_WithListOfEntries()
    {
        // Arrange
        var mockService = new Mock<IMoodEntryService>();
        mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<MoodEntryDto>
        {
            new MoodEntryDto { Id = 1, Mood = "Happy" },
            new MoodEntryDto { Id = 2, Mood = "Sad" }
        });

        var controller = new MoodEntriesApiController(mockService.Object);

        // Act
        var result = await controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var entries = Assert.IsType<List<MoodEntryDto>>(okResult.Value);
        Assert.Equal(2, entries.Count); 
    }

    [Fact]
    public async Task GetById_ReturnsNotFound_WhenEntryDoesNotExist()
    {
        // Arrange
        var mockService = new Mock<IMoodEntryService>();
        mockService.Setup(s => s.GetByIdAsync(99)).ReturnsAsync((MoodEntryDto)null);

        var controller = new MoodEntriesApiController(mockService.Object);

        // Act
        var result = await controller.GetById(99);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task GetById_ReturnsOk_WhenEntryExists()
    {
        // Arrange
        var mockService = new Mock<IMoodEntryService>();
        var entry = new MoodEntryDto { Id = 1, Mood = "Happy" };
        mockService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(entry);

        var controller = new MoodEntriesApiController(mockService.Object);

        // Act
        var result = await controller.GetById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedEntry = Assert.IsType<MoodEntryDto>(okResult.Value);
        Assert.Equal(entry.Id, returnedEntry.Id);
        Assert.Equal(entry.Mood, returnedEntry.Mood);
    }

    [Fact]
    public async Task Create_ReturnsCreatedAtAction()
    {
        // Arrange
        var mockService = new Mock<IMoodEntryService>();
        var dto = new MoodEntryDto { Id = 1, Mood = "Happy" };
        mockService.Setup(s => s.CreateAsync(dto)).ReturnsAsync(dto);

        var controller = new MoodEntriesApiController(mockService.Object);

        // Act
        var result = await controller.Create(dto);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedDto = Assert.IsType<MoodEntryDto>(createdResult.Value);
        Assert.Equal(dto.Id, returnedDto.Id);
    }

    [Fact]
    public async Task Update_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var mockService = new Mock<IMoodEntryService>();
        var controller = new MoodEntriesApiController(mockService.Object);
        var dto = new MoodEntryDto { Id = 2, Mood = "Happy" };

        // Act
        var result = await controller.Update(1, dto);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task Update_ReturnsNoContent_WhenUpdateSucceeds()
    {
        // Arrange
        var mockService = new Mock<IMoodEntryService>();
        mockService.Setup(s => s.UpdateAsync(It.IsAny<MoodEntryDto>())).ReturnsAsync(true);

        var controller = new MoodEntriesApiController(mockService.Object);
        var dto = new MoodEntryDto { Id = 1, Mood = "Happy" };

        // Act
        var result = await controller.Update(1, dto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNoContent_WhenDeleteSucceeds()
    {
        // Arrange
        var mockService = new Mock<IMoodEntryService>();
        mockService.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);

        var controller = new MoodEntriesApiController(mockService.Object);

        // Act
        var result = await controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Delete_ReturnsNotFound_WhenDeleteFails()
    {
        // Arrange
        var mockService = new Mock<IMoodEntryService>();
        mockService.Setup(s => s.DeleteAsync(99)).ReturnsAsync(false);

        var controller = new MoodEntriesApiController(mockService.Object);

        // Act
        var result = await controller.Delete(99);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
