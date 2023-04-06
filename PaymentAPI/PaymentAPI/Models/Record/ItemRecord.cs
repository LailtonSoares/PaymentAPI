namespace PaymentAPI.Models.Record;

public class ItemRecord : Item, IRecord<ItemResponse>
{
  public uint Id { get; set; }

  public ItemResponse ToResponse()
  {
    return new ItemResponse
    {
      Nome = this.Nome,
      PrecoUnitario = this.PrecoUnitario,
      Quantidade = this.Quantidade,
    };
  }

  // Relacionamentos
  public uint VendaId { get; set; }
  public VendaRecord Venda { get; set; }
}