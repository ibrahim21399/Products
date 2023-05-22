using Microsoft.AspNetCore.Http;
using products.Application.Infrastructure;
using products.Application.Interfaces;
using products.Common;
using products.Domain.Entities;

namespace products.Application.Products.Commands;

public class CreateProductCommand : IRequest<CommandResponse<string>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal price { get; set; }
    public IFormFile Image { get; set; }
    public string WebRootPath { get; set; }
    public string UserId { get; set; }
    public class handler : IRequestHandler<CreateProductCommand, CommandResponse<string>>
    {
        private readonly IAppDbContext _Context;
        private readonly IMediator _mediator;
        private readonly IFileUploader _fileUploader;

        public handler(IAppDbContext Context, IMediator mediator, IFileUploader fileUploader)
        {
            _Context = Context;
            _mediator = mediator;
            _fileUploader = fileUploader;
        }


        public async Task<CommandResponse<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {

                FileUploaderResult UploadedPicture = null;
                if (request.Image != null)
                {
                    UploadedPicture = await _fileUploader.Save(request.Image, new ImageContentTypeValidator(request.WebRootPath, FileLocation.Products));
                }

                if (!UploadedPicture.Status)
                {
                    return new CommandResponse<string> { Data = null, Success = false, Message = UploadedPicture.Error };
                }


                var product = new Product
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.price,
                    Image = UploadedPicture.FileName,
                };

                await _Context.Products.AddAsync(product);
                await _Context.SaveChangesAsync(cancellationToken);


               return new CommandResponse<string> { Data =null, Success = true, Message ="product Created Success" };

            }
            catch (Exception ex)
            {
                return new CommandResponse<string> { Data = null, Success = false, Message =ex.Message};
            }
        }
    }
}

