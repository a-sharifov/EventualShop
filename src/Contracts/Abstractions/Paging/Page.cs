namespace Contracts.Abstractions.Paging;

public record Page
{
    public int Number { get; init; } = 1;
    public int Size { get; init; } = 10;
    public bool HasPrevious { get; init; }  
    public bool HasNext { get; init; }

    public static implicit operator Abstractions.Protobuf.Page(Page page)
        => new()
        {
            Current = page.Number,
            Size = page.Size,
            HasPrevious = page.HasPrevious,
            HasNext = page.HasNext
        };

    public static implicit operator Page(Abstractions.Protobuf.Page page)
        => new()
        {
            Number = page.Current,
            Size = page.Size,
            HasPrevious = page.HasPrevious,
            HasNext = page.HasNext
        };
}