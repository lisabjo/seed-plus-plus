using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using SeedPlusPlus.Api.Categories;
using SeedPlusPlus.Core;
using SeedPlusPlus.Core.Products.Entities;
using SeedPlusPlus.Core.Products.Features;
using SeedPlusPlus.Core.Tags;

namespace SeedPlusPlus.Api.Products;

public static class Mapper
{
    public static Result<AddImageInput> ToAddImageInput(this AddImageRequest request)
    {
        return Enum.TryParse<ImageType>(request.ImageType, true, out var type)
            ? new AddImageInput
            (
                Type: type,
                Content: Convert.FromBase64String(request.Content),
                AltText: request.AltText
            )
            : new Exception("Invalid ImageType");  // Todo: Create an Exception
    }

    public static Result<CreateProductInput> ToCreateProductInput(this CreateProductRequest request)
    {
        var images = request.Images ?? Array.Empty<ProductImageRequest>();
        var tags = request.Tags ?? Array.Empty<ProductTagRequest>();
        
        return new CreateProductInput
        (
            Name: request.Name,
            Price: request.Price,
            TypeId: request.TypeId,
            CategoryId: request.CategoryId,
            Images: images
                .Select(i => new ProductImageInput(i.ImageId))
                .ToArray(),
            Tags: Result<ProductTagInput>
                .FilterOutErrors(tags.Select(MapTagRequest))
                .ToArray()
        );

        Result<ProductTagInput> MapTagRequest(ProductTagRequest tag)
        {
            return Result<TagType>
                .Create(() => Enum.Parse<TagType>(tag.TagType, true))
                .Map(tagType => ProductTagConversions.Parse(tag.Value, tagType))
                .Map(v => new ProductTagInput(
                    TagId: tag.TagId,
                    Value: v));
        }
    }
    
    public static ProductResponse ToProductResponse(this CreateProductOutput output)
    {
        return new ProductResponse(
            Id: output.Id,
            Name: output.Name,
            Price: output.Price,
            TypeId: output.TypeId,
            CategoryId: output.CategoryId,
            NumberInStock: output.NumberInStock,
            Images: output.Images.Select(ToProductImageResponse).ToArray(),
            Tags: output.Tags.Select(ToProductTagResponse).ToArray());
    }

    public static ProductsResponse ToProductsResponse(this GetProductsOutput output)
    {
        return new ProductsResponse(
            Id: output.Id,
            Name: output.Name,
            Price: output.Price,
            TypeId: output.TypeId,
            CategoryId: output.CategoryId,
            NumberInStock: output.NumberInStock
        );
    }
    
    public static ProductResponse ToProductResponse(this GetProductOutput output)
    {
        return new ProductResponse(
            Id: output.Id,
            Name: output.Name,
            Price: output.Price,
            TypeId: output.TypeId,
            CategoryId: output.CategoryId,
            NumberInStock: output.NumberInStock,
            Images: output.Images.Select(ToProductImageResponse).ToArray(),
            Tags: output.Tags.Select(ToProductTagResponse).ToArray()
        );
    }

    public static UpdateProductInput ToUpdateProductInput(this UpdateProductRequest request, int id)
    {
        return new UpdateProductInput(
            Id: id,
            Name: request.Name,
            Price: request.Price,
            TypeId: request.TypeId,
            CategoryId: request.CategoryId
        );
    }

    public static ProductResponse ToProductResponse(this UpdateProductOutput output)
    {
        return new ProductResponse(
            Id: output.Id,
            Name: output.Name,
            Price: output.Price,
            TypeId: output.TypeId,
            CategoryId: output.CategoryId,
            NumberInStock: output.NumberInStock,
            Images: Array.Empty<ProductImageResponse>(),
            Tags: Array.Empty<ProductTagResponse>());
    }

    public static ProductImageResponse ToProductImageResponse(this ProductImageOutput output)
    {
        return new ProductImageResponse(
            ImageId: output.ImageId,
            AltText: output.AltText,
            Index: output.Index,
            ImageType: output.ImageType
        );
    }

    public static ProductTagResponse ToProductTagResponse(this ProductTagOutput output)
    {
        return new ProductTagResponse(
            TagId: output.ProductTag.TagId,
            TagName: output.ProductTag.Tag.Name,
            TagType: output.ProductTag.Tag.Type.ToString().ToLower(),
            Value: MapInter(output.ProductTag)
        );
            
        object MapInter(ProductTag pt)
        {
            return pt.Tag.Type switch
            {
                TagType.String => Encoding.UTF8.GetString(pt.Value),
                TagType.Boolean => BitConverter.ToBoolean(pt.Value),
                TagType.Integer => BitConverter.ToInt32(pt.Value),
                TagType.SingleMonth => ((Month)BitConverter.ToUInt16(pt.Value)).ToString().ToLower(),
                TagType.Months => ((MonthFlags)pt.Value[0]).ToString().ToLower().Split(","),
                _ => throw new UnreachableException()
            };
        }
    }

    public static Result<CreateCategoryInput> ToCreateCategoryInput(this CreateCategoryRequest request)
    {
        return new CreateCategoryInput(
            Name: request.Name,
            ParentId: request.ParentId
        );
    }

    public static CategoryResponse ToCategoryResponse(this CategoryOutput output)
    {
        return new CategoryResponse(
            Id: output.Id,
            Name: output.Name,
            Left: output.Left,
            Right: output.Right,
            ParentId: output.ParentId
        );
    }
}
