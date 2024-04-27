

using BuildingBlocks.Behaviors;

namespace CatalogApi.Products.CreateProduct;
public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price):ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

//validation
public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
{
   public CreateProductCommandValidator() {
        RuleFor(p => p.Name).NotEmpty().WithMessage("Name is required")
            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
        RuleFor(p => p.Category).NotEmpty().WithMessage("Category is required");
        RuleFor(p => p.ImageFile).NotEmpty().WithMessage("ImageFile is required");
        RuleFor(p => p.Price).GreaterThan(0).WithMessage("Price must be greator than 0");
    }
}

internal class CreateProductCommandHandler(IDocumentSession session):ICommandHandler<CreateProductCommand,CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        //Create Product entity from command object
        //save to database
        //return CreateProcut result
        
        var product = new Product
        {
            Name = command.Name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };
        //save to database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        //return result
        return new CreateProductResult(product.Id);

    }
}
