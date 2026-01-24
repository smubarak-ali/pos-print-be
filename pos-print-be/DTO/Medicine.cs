namespace pos_print_be.DTO;

public record Medicine(
    int Id,
    string Name,
    decimal Price
)
{
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}, {nameof(Name)}: {Name}, {nameof(Price)}: {Price}";
    }
}