namespace PaymentAPI.Models.Request;

public class ItemRequest : Item, IRequest<ItemRecord>
{
  public ItemRecord ToRecord()
  {
    return new ItemRecord
    {
      Nome = this.Nome,
      PrecoUnitario = this.PrecoUnitario,
      Quantidade = this.Quantidade,
    };
  }
}