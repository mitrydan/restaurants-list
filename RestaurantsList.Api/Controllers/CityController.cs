using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestaurantsList.Abstractions;
using RestaurantsList.Api.ApiResponses;
using RestaurantsList.Api.Extensions;
using RestaurantsList.Contracts;
using RestaurantsList.Models;
using RestaurantsList.Requests;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace RestaurantsList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IRestaurantService _restaurantService;

        public CityController(ICityService cityService, IRestaurantService restaurantService)
        {
            _cityService = cityService ?? throw new ArgumentNullException(nameof(ICityService));
            _restaurantService = restaurantService ?? throw new ArgumentNullException(nameof(IRestaurantService));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response<City>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateCityAsync([FromBody] CreateCityRq createCityRq)
        {
            var result = await _cityService.CreateCityAsync(createCityRq);
            return Created(
                new Uri(string.Empty, UriKind.Relative),
                result.CreateApiResponse());
        }

        [HttpGet("{cityId}/restaurants")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<IEnumerable<Restaurant>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetRestaurantsByCityAsync(
            [Required] long cityId,
            [FromQuery] GetRestaurantsByCityRq getRestaurantsByCityRq
        )
        {
            var result = await _restaurantService.GetRestaurantsByCityAsync(cityId, getRestaurantsByCityRq);
            return result.data.IsOk()
                ? Ok(result.data.CreatePagedApiResponse(getRestaurantsByCityRq, result.count, Request))
                : (IActionResult)NotFound(result.CreateNotFoundApiResponse());
        }

        [HttpPost("{cityId}/restaurants")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Response<Restaurant>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> CreateRestaurantAsync([Required] long cityId, [FromBody] CreateRestaurantRq createRestaurantRq)
        {
            var result = await _restaurantService.CreateRestaurantAsync(cityId, createRestaurantRq);
            return Created(
                new Uri(string.Empty, UriKind.Relative),
                result.CreateApiResponse());
        }
    }
}
