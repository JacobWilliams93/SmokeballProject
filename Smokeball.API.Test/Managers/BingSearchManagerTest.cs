using System.Net;
using FluentAssertions;
using Moq;
using Moq.Protected;
using SmokeballAPI.Enums;
using SmokeballAPI.Managers;
using SmokeballAPI.Models;

namespace Smokeball.API.Test.Managers;

public class BingSearchManagerTest
{
    
 [Test]
    public async Task GetSearchResultPositions_Should_Return_Correct_Result()
    {
        // Arrange
        var searchManager = GetSUT();
        var expected = new SearchResultModel()
        {
            Keywords = "Smokeball Test",
            Url = "www.smokeball.com",
            SearchType = SearchManagerEnum.BingSearchManager,
            SearchPositions = [1]
        };
        // Act
        var result = await searchManager.GetSearchResultPositions(["Smokeball", "Test"], "www.smokeball.com", 10);
        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Test]
    public async Task GetSearchResultPositions_Returns_ZeroWhenNotFound()
    {
        // Arrange
        var searchManager = GetSUT();
        var expected = new SearchResultModel()
        {
            Keywords = "Smokeball Test",
            Url = "www.test.com",
            SearchType = SearchManagerEnum.BingSearchManager,
            SearchPositions = [0]
        };
        
        // Act
        var result = await searchManager.GetSearchResultPositions(["Smokeball", "Test"], "www.test.com", 10);
        
        // Assert
        result.Should().BeEquivalentTo(expected);
    }


    private BingSearchManager GetSUT()
    {
        var htmlSample = @"<a class=""b_algospacing_link"" href=""https://www.smokeball.com.au/"" h=""ID=SERP,5503.1"">See more</a></div></div><div class=""b_tpcn""><a class=""tilk"" aria-label=""Smokeball"" href=""https://www.smokeball.com.au/"" h=""ID=SERP,5207.1""><div class=""tpic""><div class=""wr_fav"" data-priority=""2""><div class=""cico siteicon"" style=""width:32px;height:32px;""><div class=""rms_iac"" style=""height:32px;line-height:32px;width:32px;"" data-height=""32"" data-width=""32"" data-alt=""Global web icon"" data-class=""rms_img"" data-src=""//th.bing.com/th?id=ODLS.dae3515b-9ec9-4064-8c0e-0ffdfa869654&amp;w=32&amp;h=32&amp;qlt=90&amp;pcl=fffffa&amp;o=6&amp;pid=1.2""></div></div></div></div><div class=""tptxt""><div class=""tptt"">Smokeball</div><div class=""tpmeta""><div class=""b_attribution b_nav"" u=""0N|5189|4535679201145000|THG5618XRawOlsASRopLCosk8ispi7fW"" tabindex=""0"">";
        
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
        
        return new BingSearchManager(mockHttpFactory.Object);
    }}