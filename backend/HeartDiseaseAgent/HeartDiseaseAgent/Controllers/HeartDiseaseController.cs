using Microsoft.AspNetCore.Mvc;

namespace HeartDiseaseAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeartDiseaseController : ControllerBase
    {
        private readonly HeartDiseaseService _heartDiseaseService;

        public HeartDiseaseController(HeartDiseaseService heartDiseaseService)
        {
            _heartDiseaseService = heartDiseaseService;
        }

        [HttpPost("train")]
        public IActionResult TrainModel()
        {
            try
            {
                string modelPath = "heart_model.zip";
                _heartDiseaseService.TrainModel(modelPath);
                return Ok("Model uspjesno istreniran.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška: {ex.Message}");
            }
        }

        [HttpPost("predict")]
        public IActionResult Predict([FromBody] HeartData input)
        {
            string modelPath = "heart_model.zip";
            var probability = _heartDiseaseService.Predict(modelPath, input);
            return Ok(new { Probability = $" {probability * 100}%" });
        }

    }
}

