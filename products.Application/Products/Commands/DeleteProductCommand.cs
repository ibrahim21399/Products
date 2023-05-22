using Microsoft.EntityFrameworkCore;
using products.Application.Interfaces;

namespace products.Application.Products.Commands;

public class DeleteProductCommand : IRequest<CommandResponse<string>>
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public class handler : IRequestHandler<DeleteProductCommand, CommandResponse<string>>
    {
        private readonly IAppDbContext _context;

        public handler(IAppDbContext context)
        {
            _context = context;

        }
        public async Task<CommandResponse<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _context.Products.Where(a => a.Id.ToString() == request.Id && !a.Deleted).AsTracking().FirstOrDefaultAsync();

                if (product == null)
                    return new CommandResponse<string> { Success = false, Message = "No Product Found" }; 

                product.Deleted = true;
                product.ModificationDate = DateTime.Now;
                product.ModifiedById = request.UserId;

                await _context.SaveChangesAsync(cancellationToken);

                return new CommandResponse<string> { Success =true,Message="Deleted"};
            }
            catch (Exception ex)
            {
                return new CommandResponse<string> { Success = false, Message = ex.Message }; ;
            }
        }
    }
}
