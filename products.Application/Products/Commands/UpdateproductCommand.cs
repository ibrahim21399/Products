using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using products.Application.Interfaces;

namespace products.Application.Products.Commands;

public class UpdateproductCommand : IRequest<CommandResponse<string>>
{
	public string Id { get; set; }
	public string Description { get; set; }
	public string Name { get; set; }
	public decimal price { get; set; }
	public IFormFile Image { get; set; }
	public string WebRootPath { get; set; }
	public string UserId { get; set; }
	public class handler : IRequestHandler<UpdateproductCommand, CommandResponse<string>>
	{
		private readonly IAppDbContext _Context;

		public handler(IAppDbContext context)
		{
			_Context = context;

		}
		public async Task<CommandResponse<string>> Handle(UpdateproductCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var B = await _Context.Products.AsTracking().Where(a => a.Id.ToString() == request.Id && !a.Deleted).FirstOrDefaultAsync();

				if (B == null)
					return new CommandResponse<string> { Success = false, Message = "No Product Found" };

				B.Name = request.Name;
				B.Price = request.price;
				B.ModificationDate = DateTime.Now;
				B.Description = request.Description;
				B.ModifiedById = request.UserId;



				await _Context.SaveChangesAsync(cancellationToken);


				return new CommandResponse<string> { Success = true, Message = "updated" };
			}
			catch (Exception ex)
			{
				return new CommandResponse<string> { Success = false, Message = ex.Message }; ;

			}
		}
	}
}
