using Microsoft.AspNetCore.Http;
using products.Domain.Enums;

namespace products.Application.Infrastructure;

public interface IFileUploader
{
	Task<FileUploaderResult> Save(IFormFile file, IContentTypeValidator contentTypeValidator, FileType type = FileType.Image);
	FileUploaderResult Delete(string wwwroot, string FolderName, string FileName);
}
