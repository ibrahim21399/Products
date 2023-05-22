namespace products.Models;

public class JsonResponse<T>
{
	public string Message {		get; set; }
	public T Data { get; set; }
	public bool Success { get; set; }
	public int StatusCode { get; set; }
}