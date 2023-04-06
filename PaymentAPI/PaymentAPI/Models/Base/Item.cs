namespace PaymentAPI.Models.Base;

public abstract class Item
{
  /// <example>Oreo</example>
  [Required]
  public string Nome { get; set; }
  /// <example>2.5</example>
  [Required]
  public decimal PrecoUnitario { get; set; }
  /// <example>3</example>
  [Required]
  public uint Quantidade { get; set; }
}