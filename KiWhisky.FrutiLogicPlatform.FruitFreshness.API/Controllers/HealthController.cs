using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace FruitFreshness.Controllers
{
    [ApiController]
    [Route("api/v1/health")]
    public class HealthController : ControllerBase
    {
        private readonly FruitFreshness.Services.FruitClassifierService _classifier;

        public HealthController(FruitFreshness.Services.FruitClassifierService classifier)
        {
            _classifier = classifier;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "ok",
                model_loaded = _classifier.ModelLoaded,
                model_version = _classifier.ModelVersion
            });
        }
    }
}
