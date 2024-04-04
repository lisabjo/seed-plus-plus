using SeedPlusPlus.Core.Products.Entities;

namespace SeedPlusPlus.Core.Products.Features;

public class AddImage : IUseCase<AddImageInput, Result<bool>>
{
    public async Task<Result<bool>> Handle(AddImageInput input)
    {
        return new NotImplementedException();
    }
}

public record AddImageInput(ImageType Type, byte[] Content, string AltText);
