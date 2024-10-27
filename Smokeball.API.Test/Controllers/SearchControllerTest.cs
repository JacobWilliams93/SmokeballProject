using FluentAssertions;
using Moq;
using SmokeballAPI.Controllers;
using SmokeballAPI.Enums;
using SmokeballAPI.Factories;
using SmokeballAPI.Interfaces;

namespace Smokeball.API.Test.Controllers;

public class SearchControllerTest
{
    [Test]
    public async Task Search_Should_Call_Manager()
    {
        // Arrange
        var mockManager = new Mock<ISearchManager>();
        mockManager.Setup(
                x => x.GetSearchResultPositions(It.IsAny<List<string>>(), It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync([1, 2, 3])
            .Verifiable();
        var mockFactory = new Mock<ISearchManagerFactory>();
        mockFactory.Setup(x => x.GetSearchManager(It.IsAny<SearchManagerEnum>()))
            .Returns(mockManager.Object);
        var controller = new SearchController(mockFactory.Object);
        
        // Act
        await controller.Search("test", "test", SearchManagerEnum.GoogleSearchManager);
        
        // Assert
        mockManager.Verify();
    }
}