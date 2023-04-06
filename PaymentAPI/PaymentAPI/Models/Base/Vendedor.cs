namespace PaymentAPI.Models.Base;

public abstract class Vendedor
{
  /// <example>1</example>
  [Required]
  public uint Id { get; set; }

  /// <example>11111111111</example>
  [Required]
  public string Cpf { get; set; }

  /// <example>Gabriel</example>
  [Required]
  public string Nome { get; set; }

  /// <example>gabriel@email.com</example>
  [Required]
  public string Email { get; set; }

  /// <example>999999999</example>
  [Required]
  public string Telefone { get; set; }
}