namespace PaymentAPI.Models.Request;

public class VendedorRequest : Vendedor, IRequest<VendedorRecord>
{
  public VendedorRecord ToRecord()
  {
    return new VendedorRecord
    {
      Id = this.Id,
      Cpf = this.Cpf,
      Nome = this.Nome,
      Email = this.Email,
      Telefone = this.Telefone
    };
  }
}