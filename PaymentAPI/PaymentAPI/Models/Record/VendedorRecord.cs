namespace PaymentAPI.Models.Record;

public class VendedorRecord : Vendedor, IRecord<VendedorResponse>
{
  public VendedorResponse ToResponse()
  {
    return new VendedorResponse
    {
      Id = this.Id,
      Cpf = this.Cpf,
      Nome = this.Nome,
      Email = this.Email,
      Telefone = this.Telefone
    };
  }

  // Relacionamentos
  public ICollection<VendaRecord> Vendas { get; set; }
}