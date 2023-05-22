using Microsoft.AspNetCore.Http;
using products.Domain.Enums;

namespace products.Application.Infrastructure;

public class FileUploader : IFileUploader
{
	public FileUploader()
	{
	}

	public async Task<FileUploaderResult> Save(IFormFile file, IContentTypeValidator contentTypeValidator, FileType type)
	{
		if (file == null || contentTypeValidator == null) return new FileUploaderResult { Status = false };

		string folderPath = contentTypeValidator.Path;
		CreateIfMissing(folderPath);
		string fileName = $"{DateTime.Now:yyyymmddhhmmssffff}.{file.FileName.Split(".").Last()}";
		string path = $"{folderPath}/{fileName}";
		if (file.Length > 100000)
			return new FileUploaderResult
			{
				Status = false,
				Error = "Invalid File Length please upload file less than 30 MB"
			};


		try
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}

			using (FileStream fs = new FileStream(path, FileMode.Create))
			{
				await file.CopyToAsync(fs);
			}
			return new FileUploaderResult { Status = true, FileName = fileName, Folder = path };

		}
		catch (Exception ex)
		{
			return new FileUploaderResult { Status = false, Error = ex.Message };
		}
	}

	
	public FileUploaderResult Delete(string wwwroot, string FolderName, string FileName)
	{
		try
		{
			File.Delete($"{wwwroot}/{FolderName}/{FileName}");
			return new FileUploaderResult { Status = true, FileName = FileName };
		}
		catch //(Exception exp)
		{
			return new FileUploaderResult { Status = false };
		}
	}
	/// <summary>
	/// create folder if not exist
	/// </summary>
	/// <param name="path"></param>
	private void CreateIfMissing(string path)
	{
		if (!Directory.Exists(path)) Directory.CreateDirectory(path);
	}
}
