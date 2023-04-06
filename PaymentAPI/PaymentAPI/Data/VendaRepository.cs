namespace PaymentAPI.Data;

public class VendaRepository : ARepository, IRepository<VendaRecord>
{
  public VendaRepository(ApiContext context) : base(context) { }

  public async Task<VendaRecord> GetById(uint id)
  {
    var venda = await _context.Vendas.FindAsync(id);
    if (venda == null) return null;
    _context.Entry(venda).Reference(v => v.Vendedor).Load();
    _context.Entry(venda).Collection(v => v.Itens).Load();
    return venda;
  }

  public async Task Insert(VendaRecord venda)
  {
    var vendedor = await _context.Vendedores.FindAsync(venda.Vendedor.Id);
    if (vendedor != null) venda.Vendedor = vendedor;
    await _context.Vendas.AddAsync(venda);
    await Save();
  }

  public async Task Update(VendaRecord venda)
  {
    _context.Vendas.Update(venda);
    await Save();
  }
}