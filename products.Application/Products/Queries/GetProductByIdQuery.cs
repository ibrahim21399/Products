
using Microsoft.EntityFrameworkCore;
using products.Application.Interfaces;
using products.Application.Products.Models;
using products.Common;

namespace products.Application.products.Queries;

public class GetProductByIdQuery : IRequest<CommandResponse<ProductsDto>>
{
    public string Id { get; set; }
    public class handler : IRequestHandler<GetProductByIdQuery, CommandResponse<ProductsDto>>
    {
        private readonly IAppDbContext _Context;


        public handler(IAppDbContext context)
        {
            _Context = context;

        }
        public async Task<CommandResponse<ProductsDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var Result = await _Context.Products.Where(a => a.Id.ToString() == request.Id && !a.Deleted).Select(a => new ProductsDto
                {
                    Id = a.Id.ToString(),
                    Name = a.Name,
                    Description = a.Description,
                    ImagePath = FileLocation.Products + a.Image,
                    Price = a.Price,

                }).FirstOrDefaultAsync();


                return new CommandResponse<ProductsDto> { Data = Result ,Success =true, Message ="Product by Id:" };

            }
            catch (Exception ex)
            {
                return new CommandResponse<ProductsDto> { Data = null, Success = false, Message = ex.Message };
            }
        }
    }
}
