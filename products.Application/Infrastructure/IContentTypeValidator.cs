namespace products.Application.Infrastructure;

public interface IContentTypeValidator
{
	string Path { get; }
	bool IsValidContentType(string ContentType);
}
