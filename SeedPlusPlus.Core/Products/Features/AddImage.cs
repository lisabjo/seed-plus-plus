namespace SeedPlusPlus.Core.Products.Features;

public class AddImage : IUseCase<AddImageInput, object>
{
    public Task<object> Handle(AddImageInput input)
    {
        throw new NotImplementedException();
    }
}

public record AddImageInput(ImageType Type, byte[] Content, string AltText);