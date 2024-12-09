using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Items
{
    [Key]
    public int ItemID { get; set; }
    public required string Name { get; set; }

    public int? Value { get; set; }

    public bool Stackable { get; set; }

    public int CategoryId { get; set; }
}

public class ItemCategories
{
    [Key]
    public int CategoryID { get; set; }
    public required string Name { get; set; }
}


public class ConsumableItems
{
    [Key]
    public int ConsumableItemID { get; set; }

    [ForeignKey(nameof(Items))]
    public int ItemID { get; set; }
    public int HealthValue { get; set; }
    public int EnergyValue { get; set; }
    public int RadiationValue { get; set; }
}