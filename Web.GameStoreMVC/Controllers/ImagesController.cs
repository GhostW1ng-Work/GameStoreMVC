﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Web.GameStoreMVC.Repositories;

namespace Web.GameStoreMVC.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase
	{
		private readonly IImageRepository _imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            var imageUrl = await _imageRepository.UploadAsync(file);

            if(imageUrl == null)
            {
                return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new {link = imageUrl });
        }
    }
}
