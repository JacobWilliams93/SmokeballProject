using FluentAssertions;
using Moq;
using SmokeballAPI.Enums;
using SmokeballAPI.Factories;
using SmokeballAPI.Interfaces;
using SmokeballAPI.Managers;

namespace Smokeball.API.Test.Factories;

public class SearchManagerFactoryTest
{
    [TestCase(SearchManagerEnum.GoogleSearchManager)]
    [TestCase(SearchManagerEnum.BingSearchManager)]
    public void GetSearchManager_Should_Return_Correct_Manager(SearchManagerEnum searchManagerType)
    {
        // Arrange
        var mockGoogleSearchManager = new Mock<ISearchManager>();
        var mockBingSearchManager = new Mock<ISearchManager>();
        mockGoogleSearchManager.Setup(x => x.SearchManagerType()).Returns(SearchManagerEnum.GoogleSearchManager);
        mockBingSearchManager.Setup(x => x.SearchManagerType()).Returns(SearchManagerEnum.BingSearchManager);
        var factory = new SearchManagerFactory([mockGoogleSearchManager.Object, mockBingSearchManager.Object]);
        
        // Act
        var result = factory.GetSearchManager(searchManagerType);
        
        // Assert
        result.SearchManagerType().Should().Be(searchManagerType);
    }
}