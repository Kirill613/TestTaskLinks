using LinkShorteningApp.Controllers;
using LinkShorteningApp.Database;
using LinkShorteningApp.Models;
using LinkShorteningApp.Repositories;
using LinkShorteningApp.Services.ShortLinkService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace TestUrls
{
    [TestClass]
    public class UnitTestUrls
    {
        [TestMethod]
        public void GenerateShortLink_ReturnsShortHashCode()
        {
            // Arrange
            var service = new ShortLinkService(Mock.Of<IUrlRepository>(), Mock.Of<ApplicationDbContext>());

            // Act
            string url = "https://www.example.com";
            string shortLink = service.GenerateShortLink(url);

            // Assert
            Assert.IsNotNull(shortLink);
            Assert.AreEqual(8, shortLink.Length);
        }
        
        [TestMethod]
        public async Task RedirectToOriginalUrl_InvalidShortUrl_ReturnsNotFound()
        {
            // Arrange
            var shortUrl = "abc123";

            var shortLinkServiceMock = new Mock<IShortLinkService>();
            shortLinkServiceMock
                .Setup(s => s.GetRedirectUrl(shortUrl, CancellationToken.None))
                .ReturnsAsync((string)null);

            var controller = new UrlController(shortLinkServiceMock.Object);

            // Act
            var result = await controller.RedirectToOriginalUrl(shortUrl, CancellationToken.None);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task RedirectToOriginalUrl_ValidShortUrl_ReturnsRedirect()
        {
            // Arrange
            var shortUrl = "abc123";
            var originalUrl = "https://www.example.com";

            var shortLinkServiceMock = new Mock<IShortLinkService>();
            shortLinkServiceMock
                .Setup(s => s.GetRedirectUrl(shortUrl, CancellationToken.None))
                .ReturnsAsync(originalUrl);

            var controller = new UrlController(shortLinkServiceMock.Object);

            // Act
            var result = await controller.RedirectToOriginalUrl(shortUrl, CancellationToken.None);
            var redirectUrl = (result as RedirectResult)?.Url;

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual(originalUrl, redirectUrl);
        }
    }
}