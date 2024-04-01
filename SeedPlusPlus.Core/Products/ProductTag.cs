using System.Text;

namespace SeedPlusPlus.Core.Products;

public class ProductTag
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Tag Tag { get; set; }
    public int TagId { get; set; }
    public byte[] Value { get; set; } = Array.Empty<byte>();

    public object GetValueAs()
    {
        return Tag.Type switch
        {
            TagType.String => Encoding.UTF8.GetString(Value),
            TagType.Boolean => BitConverter.ToBoolean(Value),
            TagType.Month => Value[0],
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}