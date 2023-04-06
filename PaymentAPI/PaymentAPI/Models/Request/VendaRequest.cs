namespace PaymentAPI.Models.Request;

public class VendaRequest : IRequest<VendaRecord>
{
  [Required]
  public VendedorRequest Vendedor { get; set; }
  [Required]
  public IEnumerable<ItemRequest> Itens { get; set; }

  public VendaRecord ToRecord()
  {
    return new VendaRecord
    {
      Valor = _calculateValor(),
      Data = DateTime.Now,
      Status = EStatus.AGUARDANDO_PAGAMENTO,
      Vendedor = this.Vendedor.ToRecord(),
      Itens = _transformItensRequestToRecord()
    };
  }

  private List<ItemRecord> _transformItensRequestToRecord()
  {
    return Itens.Select(
      (item) => item.ToRecord()
    ).ToList<ItemRecord>();
  }

  private decimal _calculateValor()
  {
    return Itens.Aggregate(
      seed: 0.0M,
      (sum, item) => sum + item.PrecoUnitario * item.Quantidade
    );
  }
}