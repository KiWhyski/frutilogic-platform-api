using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FruitFreshness.Controllers
{
    [ApiController]
    [Route("api/v1/classify")]
    public class ClassifyController : ControllerBase
    {
        private readonly FruitFreshness.Services.FruitClassifierService _classifier;

        public ClassifyController(FruitFreshness.Services.FruitClassifierService classifier)
        {
            _classifier = classifier;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] Microsoft.AspNetCore.Http.IFormFile imageFile)
        {
            if (imageFile == null)
                return BadRequest(new { error = "No file provided" });

            var tempPath = Path.ChangeExtension(Path.GetTempFileName(), Path.GetExtension(imageFile.FileName));
            await using (var fs = System.IO.File.Create(tempPath))
            {
                await imageFile.CopyToAsync(fs);
            }

            try
            {
                var result = _classifier.PredictFromFile(tempPath);
                return Ok(new
                {
                    label = result.Label,
                    confidence = result.Confidence,
                    scores = result.Scores.Select(s => new { label = s.label, score = s.score }),
                    modelVersion = result.ModelVersion
                });
            }
            catch (System.InvalidOperationException ex)
            {
                return StatusCode(503, new { error = ex.Message });
            }
            finally
            {
                try { System.IO.File.Delete(tempPath); } catch { }
            }
        }
    }
}

