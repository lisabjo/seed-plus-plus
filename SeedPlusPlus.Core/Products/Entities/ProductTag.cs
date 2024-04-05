using System.Text;
using System.Text.Json;
using SeedPlusPlus.Core.Tags;

namespace SeedPlusPlus.Core.Products.Entities;

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
            TagType.SingleMonth => Value[0],
            TagType.Months => (MonthFlags)BitConverter.ToUInt16(Value),
            TagType.Integer => BitConverter.ToInt32(Value),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

public enum Month: byte
{
    January = 1,
    February = 2,
    March = 3,
    April = 4,
    May = 5,
    June = 6,
    July = 7,
    August = 8,
    September = 9,
    October = 10,
    November = 11,
    December = 12
}

[Flags]
public enum MonthFlags : ushort  // short to get 16 bits
{
    January = 1 << 0,
    February = 1 << 1,
    March = 1 << 2,
    April = 1 << 3,
    May = 1 << 4,
    June = 1 << 5,
    July = 1 << 6,
    August = 1 << 7,
    September = 1 << 8,
    October = 1 << 9,
    November = 1 << 10,
    December = 1 << 11
}

public static class ProductTagConversions
{
    
    /// <summary>
    /// Parses a Json string into a byte[] depending on the TagType.
    /// </summary>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static Result<byte[]> Parse(string value, TagType type)
    {
        return type switch
        {
            TagType.String => ParseTrivial<string>(value).Map(Encoding.UTF8.GetBytes),
            TagType.Boolean => ParseTrivial<bool>(value).Map(BitConverter.GetBytes),
            TagType.Integer => ParseTrivial<int>(value).Map(BitConverter.GetBytes),
            TagType.SingleMonth => ParseSingleMonth(value).Map(x => new[] {x}),
            TagType.Months => ParseMonths(value).Map(flags => BitConverter.GetBytes((ushort)flags)),
            _ => new ArgumentOutOfRangeException(nameof(type), type, "Can not parse value of this type.")
        };
    }

    private static Result<byte> ParseSingleMonth(string json)
    {
        var value = ParseTrivial<string>(json);
        return value.Match<Result<byte>>(
            s => Enum.TryParse<Month>(s, true, out var month)
                ? (byte)month
                : new ArgumentException("Could not parse month."),
            e => e);
    }
    
    private static Result<MonthFlags> ParseMonths(string json)
    {
        return ParseTrivial<string[]>(json)
            .Map(flags => string.Join(",", flags))
            .Map(commaSeparatedFlags => Enum.Parse<MonthFlags>(commaSeparatedFlags, true));
        
        /*return ParseTrivial<string[]>(json)
            .Map(s => s
                .Select(x => Enum.Parse<MonthFlags>(x, true))
                .Aggregate((x, y) => x | y)
            );*/
    }
    
    private static Result<T> ParseTrivial<T>(string json)
    {
        return Result<T>.Create(() => JsonSerializer.Deserialize<T>(json, JsonSerializerOptions.Default)!);
    }
}
