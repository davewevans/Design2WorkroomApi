using System.Net.Http.Headers;
using Design2WorkroomApi.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Design2WorkroomApi.Controllers
{
    [Route("api/upload")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IUploadService uploadService;
        private readonly string _imageBlobContainerName;
        public UploadController(IUploadService uploadService, IConfiguration configuration)
        {
            this.uploadService = uploadService ?? throw new ArgumentNullException(nameof(uploadService));
            _imageBlobContainerName = configuration["ImageBlobContainerName"];
        }
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> UploadAsync()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    // use guid for file name
                    var fileExtension = Path.GetExtension(fileName);
                    var blobFileName = $"{ Guid.NewGuid() }{ fileExtension.ToLower() }";

                    //  TODO save file name to database

                    string fileURL = await uploadService.UploadAsync(file.OpenReadStream(), blobFileName, file.ContentType, _imageBlobContainerName);
                    return Ok(new { fileURL });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
