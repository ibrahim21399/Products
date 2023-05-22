namespace products.Application.Infrastructure;

public class ImageContentTypeValidator : IContentTypeValidator
{
	private readonly string _WebRootPath;
	private readonly string _Folder;

	public ImageContentTypeValidator(string WebRootPath, string Folder)
	{
		_WebRootPath = WebRootPath;
		_Folder = Folder;
	}
	public string Path => $"{_WebRootPath}{_Folder}";
	public string Folder => _Folder;
	public bool IsValidContentType(string ContentType)
	{
		if (string.IsNullOrEmpty(ContentType)) return false;
		ContentType = ContentType.ToLower();
		var mimeTypes = "image/*";
		return mimeTypes.Contains(ContentType);
	}
}
