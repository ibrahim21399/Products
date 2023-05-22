using Microsoft.EntityFrameworkCore;
using products.Application.Interfaces;
using products.Application.Products.Models;
using products.Common;

namespace products.Application.products.Queries;

public class GetAllProductsQuery : IRequest<CommandResponse<List<ProductsDto>>>

{

    public class Handler : IRequestHandler<GetAllProductsQuery, CommandResponse<List<ProductsDto>>>
    {
        private readonly IAppDbContext _context;
        public Handler(IAppDbContext context)
        {
            _context = context;
        }
        public async Task<CommandResponse<List<ProductsDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _context.Products.Where(a => !a.Deleted).Select(a => new ProductsDto
                {
                    Id = a.Id.ToString(),
                    Name = a.Name,
                    Description = a.Description,
                    ImagePath = FileLocation.Products + a.Image,
                    Price = a.Price,

                }).ToListAsync(cancellationToken);


                return new CommandResponse<List<ProductsDto>>
                {
                    Data = products,
                    Message = "List Of products",
                    Success = true,
                };
            }
            catch (Exception ex)
            {
               return new CommandResponse<List<ProductsDto>>
                {
                    Data = null,
                    Message = ex.Message,
                    Success = true,
                };
            }


        }

    }
}
