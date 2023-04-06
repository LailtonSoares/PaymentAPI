namespace PaymentAPI.Models.Base;

public abstract class Venda
{
  /// <example>7.5</example>
  public decimal Valor { get; set; }

  /// <example>2022-09-17T11:05:44.4267674-03:00</example>
  public DateTime Data { get; set; }

  public EStatus Status { get; set; }
}