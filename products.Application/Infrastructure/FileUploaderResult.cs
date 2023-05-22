namespace products.Application.Infrastructure;

public class FileUploaderResult
{
	public bool Status { get; set; }
	public string FileName { get; set; }
	public string Error { get; set; }
	public string Folder { get; set; }
}
