using System.Net;
using FluentAssertions;
using Moq;
using Moq.Protected;
using SmokeballAPI.Managers;

namespace Smokeball.API.Test.Managers;

public class GoogleSearchManagerTest
{
    [Test]
    public async Task GetSearchResultPositions_Should_Return_Correct_Result()
    {
        // Arrange
        var searchManager = GetSUT();
        
        // Act
        var result = await searchManager.GetSearchResultPositions(["Smokeball", "Test"], "www.smokeball.com", 10);
        // Assert
        result.Should().Equal([1]);
    }

    [Test]
    public async Task GetSearchResultPositions_Returns_ZeroWhenNotFound()
    {
        // Arrange
        var searchManager = GetSUT();
        
        // Act
        var result = await searchManager.GetSearchResultPositions(["Smokeball", "Test"], "www.test.com", 10);
        
        // Assert
        result.Should().Equal([0]);
    }


    private GoogleSearchManager GetSUT()
    {
        var htmlSample = @"<div class=""Gx5Zad xpd EtOod pkphOe""><div class=""egMi0 kCrYT""><a href=""/url?q=https://www.smokeball.com.au/&amp;sa=U&amp;ved=2ahUKEwiD4L_PqauJAxW-JDQIHUUVHO8QFnoECF8QAg&amp;usg=AOvVaw0zi5-0Eg4GfwX3OjvoElzl"" data-ved=""2ahUKEwiD4L_PqauJAxW-JDQIHUUVHO8QFnoECF8QAg""><div class=""DnJfK"">";
        
        var mockHttpFactory = new Mock<IHttpClientFactory>();
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(htmlSample)
            });
        mockHttpFactory.Setup(x => x.CreateClient(It.IsAny<string>()))
            .Returns(new HttpClient(mockHttpMessageHandler.Object));
        
        return new GoogleSearchManager(mockHttpFactory.Object);
    }
}