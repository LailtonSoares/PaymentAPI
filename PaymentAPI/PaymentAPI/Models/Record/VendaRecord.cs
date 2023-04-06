namespace PaymentAPI.Models.Record;

public class VendaRecord : Venda, IRecord<VendaResponse>
{
  public uint Id { get; set; }

  public VendaResponse ToResponse()
  {
    return new VendaResponse
    {
      Id = this.Id,
      Valor = this.Valor,
      Data = this.Data,
      Status = this.Status,
      Vendedor = this.Vendedor.ToResponse(),
      Itens = _transformItensRecordToResponse()
    };
  }

  private List<ItemResponse> _transformItensRecordToResponse()
  {
    return Itens.Select(
      (item) => item.ToResponse()
    ).ToList<ItemResponse>();
  }

  // Relacionamentos
  public uint VendedorId { get; set; }
  public VendedorRecord Vendedor { get; set; }
  public ICollection<ItemRecord> Itens { get; set; }
}