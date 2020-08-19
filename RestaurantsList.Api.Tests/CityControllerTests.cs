using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RestaurantsList.Abstractions;
using RestaurantsList.Api.Controllers;
using RestaurantsList.Models;
using RestaurantsList.Requests;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RestaurantsList.Api.Tests
{
    public class CityControllerTests
    {
        private readonly IFixture _fixture;
        private readonly CityController _cityController;
        private readonly Mock<ICityService> _cityServiceMock;
        private Mock<IRestaurantService> _restaurantServiceMock;

        public CityControllerTests()
        {
            _fixture = new Fixture();

            SetupRestaurantService();

            _cityServiceMock = new Mock<ICityService>();
            _cityController = new CityController(_cityServiceMock.Object, _restaurantServiceMock.Object);
        }

        [Theory, InlineData(1)]
        public async Task GetRestaurantsByCityAsync_ValidData_Ok(long cityId)
        {
            var paginationRequest = new GetRestaurantsByCityRq();

            var result = await _cityController.GetRestaurantsByCityAsync(cityId, paginationRequest);

            _restaurantServiceMock.Verify(
                x => x.GetRestaurantsByCityAsync(It.Is<long>(v => v == cityId), It.IsAny<GetRestaurantsByCityRq>()),
                Times.Once);

            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Theory, InlineData(2)]
        public async Task GetRestaurantsByCityAsync_ValidData_NotFound(long cityId)
        {
            var paginationRequest = new GetRestaurantsByCityRq();

            var result = await _cityController.GetRestaurantsByCityAsync(cityId, paginationRequest);

            _restaurantServiceMock.Verify(
                x => x.GetRestaurantsByCityAsync(It.Is<long>(v => v == cityId), It.IsAny<GetRestaurantsByCityRq>()),
                Times.Once);

            result.Should().NotBeNull();
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        private void SetupRestaurantService()
        {
            _restaurantServiceMock = new Mock<IRestaurantService>();

            _restaurantServiceMock
                .Setup(x => x.GetRestaurantsByCityAsync(It.Is<long>(v => v == 1), It.IsAny<GetRestaurantsByCityRq>()))
                .Returns(() =>
                    Task.FromResult((_fixture.CreateMany<Restaurant>(2), 2))
                );

            _restaurantServiceMock
                .Setup(x => x.GetRestaurantsByCityAsync(It.Is<long>(v => v == 2), It.IsAny<GetRestaurantsByCityRq>()))
                .Returns(() =>
                    Task.FromResult<(IEnumerable<Restaurant>, int)>((default, 0))
                );
        }
    }
}
