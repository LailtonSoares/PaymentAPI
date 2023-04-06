namespace PaymentAPI.Models.Response;

public class VendaResponse : Venda
{
  public uint Id { get; set; }
  public VendedorResponse Vendedor { get; set; }
  public IEnumerable<ItemResponse> Itens { get; set; }
}