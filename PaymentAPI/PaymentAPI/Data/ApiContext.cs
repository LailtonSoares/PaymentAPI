namespace PaymentAPI.Data;

public class ApiContext : DbContext
{
  public DbSet<ItemRecord> Itens { get; set; }
  public DbSet<VendedorRecord> Vendedores { get; set; }
  public DbSet<VendaRecord> Vendas { get; set; }
  public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
}
