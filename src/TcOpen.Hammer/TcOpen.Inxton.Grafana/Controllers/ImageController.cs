using System;
using System.IO;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TcOpenHammer.Grafana.API.Controllers
{
    /// <summary>
    /// When you make a request to this endpoint, it will search through the .\images folder and return the first
    /// image (regardless of its extension) with the name.
    /// <example>
    ///     localhost:5000/image/fileNameOfTheImage
    /// </example>
    /// </summary>
    [ApiController]
    [Route("/image")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly FileInfo NotFoundImage = new FileInfo("images/NoImageFound.png");

        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{fileName}")]
        public IActionResult Get(string fileName)
        {
            var imagesPath = Path.Join(Environment.CurrentDirectory, "images");
            if (Directory.Exists(imagesPath))
            {
                var foundFile = Directory
                    .EnumerateFiles(imagesPath)
                    .Select(x => new FileInfo(x))
                    .FirstOrDefault(x => x.Name.Replace(x.Extension, "") == fileName);

                if (foundFile is null)
                {
                    _logger.LogWarning($"Image with name {fileName} not found in {imagesPath}.");
                    return Image(NotFoundImage);
                }

                return Image(foundFile);
            }

            var errMsg = $"Directory {imagesPath} doesn't exist";
            _logger.LogError(errMsg);
            return Error(errMsg, HttpStatusCode.BadRequest);
        }

        private FileStreamResult Image(FileInfo imageInfo) =>
            File(System.IO.File.OpenRead(imageInfo.FullName), $"image/{TrimExtension(imageInfo)}");

        private static string TrimExtension(FileInfo fileInfo) =>
            fileInfo.Extension.Replace(".", "");

        private static IActionResult Error(string message, HttpStatusCode statusCode) =>
            new JsonResult(new { Message = message }) { StatusCode = (int)statusCode };
    }
}
