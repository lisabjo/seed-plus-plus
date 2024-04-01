namespace SeedPlusPlus.Core.Products;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TagType Type { get; set; }
    // configure many to many with tag 
}

public enum TagType
{
    String = 1,
    Boolean = 2,
    Month = 3
}
// TODO: Add month flags