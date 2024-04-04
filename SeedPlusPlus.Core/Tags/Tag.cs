namespace SeedPlusPlus.Core.Tags;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TagType Type { get; set; }
}

public enum TagType
{
    String,
    Boolean,
    SingleMonth,
    Months,
    Integer
}
