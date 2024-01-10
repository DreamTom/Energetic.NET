using Energetic.NET.API.Dto;
using Energetic.NET.Basic.Domain.IRepositories;
using Energetic.NET.Basic.Domain.Models;
using Energetic.NET.Basic.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace Energetic.NET.API.Controllers.Basic
{
    /// <summary>
    /// 文件管理
    /// </summary>
    [AllowAnonymous]
    [Route("api/files")]
    public class FilesController(FileAttachmentDomainService fileAttachmentDomainService,
        IFileAttachmentDomainRepository fileAttachmentDomainRepository,
        IWebHostEnvironment webHostEnvironment) : BaseController
    {
        [UnitOfWork(typeof(BasicDbContext))]
        [HttpPost("uploadImage")]
        public async Task<ActionResult<UploadResponse>> UploadImage(IFormFile image, CancellationToken cancellationToken)
        {
            string fileName = image.FileName;
            using var stream = image.OpenReadStream();
            var attachment = await fileAttachmentDomainService.UploadAsync(stream, fileName, image.ContentType, cancellationToken);
            if (attachment.Id == default)
                await fileAttachmentDomainRepository.AddAsync(attachment);
            return Ok(new UploadResponse(attachment.Id, attachment.Name));
        }

        /// <summary>
        /// 获取文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:long}")]
        public async Task<IActionResult> GetFile(long id)
        {
            var attacment = await fileAttachmentDomainRepository.FindByIdAsync(id);
            if (attacment == null)
                return DataNotFound("文件不存在或已被删除");

            var fileProvider = webHostEnvironment.ContentRootFileProvider;
            var file = fileProvider.GetFileInfo(attacment.Path);
            if (file.Exists)
            {
                if (Request.Headers.IfNoneMatch == attacment.HashCode)
                    return StatusCode(StatusCodes.Status304NotModified);
                Response.Headers.ETag = attacment.HashCode;
                return File(file.CreateReadStream(), attacment.ContentType, attacment.Name);
            }
            else
                return DataNotFound("文件不存在或已被删除");
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [UnitOfWork(typeof(BasicDbContext))]
        [HttpDelete("{id:long}")]
        public async Task<ActionResult<long>> Delete(long id)
        {
            var attacment = await fileAttachmentDomainRepository.FindByIdAsync(id);
            if (attacment == null)
                return DataNotFound("文件不存在或已被删除");

            var fileProvider = webHostEnvironment.ContentRootFileProvider;
            var file = fileProvider.GetFileInfo(attacment.Path);
            if (file.Exists)
                System.IO.File.Delete(attacment.Path);
            fileAttachmentDomainRepository.Delete(attacment);
            return Ok(id);
        }
    }
}
