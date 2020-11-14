using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Mongo.Models.Recipe;
using Mongo.Repositories;
using System;
using System.Linq;

namespace CookBackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeController : ControllerBase
    {

        private readonly ILogger<RecipeController> _logger;
        private readonly RecipesRepository _recipesRepository;

        public RecipeController(
            RecipesRepository recipesRepository,
            ILogger<RecipeController> logger)
        {
            _recipesRepository = recipesRepository;
            _logger = logger;
        }

        [HttpGet("[action]")]
        public IActionResult Get()
        {
            try
            {
                var res = _recipesRepository.Get();
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

        }
        
        [HttpGet("[action]/{Id}")]
        public IActionResult Get([FromRoute] string Id)
        {
            try
            {
                var res = _recipesRepository.GetByUid(Id);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }            
        }

        [HttpPost("[action]")]
        public IActionResult GetByIds([FromBody] IEnumerable<string> Ids)
        {
            try
            {
                var res = _recipesRepository.GetManyByIds(Ids.ToArray());
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        [HttpPost("[action]")]
        public IActionResult Create([FromBody] RecipeModel model)
        {
            try
            {
                var res = _recipesRepository.Create(model);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest();
            }

        }

    }
}
