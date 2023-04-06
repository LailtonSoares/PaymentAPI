namespace PaymentAPI.Tests;

[TestFixture]
public class VendaRepositoryTest : BaseTest
{
  [TestCase((uint)1)]
  [TestCase((uint)2)]
  [TestCase((uint)3)]
  public async Task GetById_ShouldReturnVendaWithVendedorAndItens(uint id)
  {
    var venda = await _repository.GetById(id);

    Assert.That(venda, Is.Not.Null);
    Assert.That(venda.Vendedor.Id, Is.EqualTo(1));
    Assert.That(venda.Itens, Is.Not.Empty);
  }

  [TestCase((uint)0)]
  [TestCase((uint)4)]
  public async Task GetById_ShouldReturnNull(uint id)
  {
    var venda = await _repository.GetById(id);

    Assert.That(venda, Is.Null);
  }

  [Test]
  public async Task Insert_ShouldAddNewVendaWithExistingVendedor()
  {
    var venda = new VendaRecord
    {
      Id = 13,
      Vendedor = new VendedorRecord
      {
        Id = 1,
        Cpf = "22222222222",
        Nome = "Victoria",
        Email = "victoria@outro.com",
        Telefone = "922222222"
      },
      Itens = new List<ItemRecord>
      {
        new ItemRecord
        {
          Nome = "Galak",
          PrecoUnitario = 1.5M,
          Quantidade = 1
        },
        new ItemRecord
        {
          Nome = "Diamante Negro",
          PrecoUnitario = 2.25M,
          Quantidade = 2
        }
      }
    };
    await _repository.Insert(venda);
    var insertedVenda = await _context.Vendas.FindAsync((uint)13);

    Assert.That(insertedVenda.Id, Is.EqualTo(13));
    Assert.That(insertedVenda.Vendedor.Id, Is.EqualTo(1));
    Assert.That(insertedVenda.Vendedor.Nome, Is.EqualTo("Gabriel"));
    Assert.That(insertedVenda.Itens.Any((item) => item.Nome == "Galak"));
  }

  [Test]
  public async Task Insert_ShouldAddNewVendaWithNewVendedor()
  {
    var venda = new VendaRecord
    {
      Id = 15,
      Vendedor = new VendedorRecord
      {
        Id = 2,
        Cpf = "22222222222",
        Nome = "Victoria",
        Email = "victoria@outro.com",
        Telefone = "922222222"
      },
      Itens = new List<ItemRecord>
      {
        new ItemRecord
        {
          Nome = "Galak",
          PrecoUnitario = 1.5M,
          Quantidade = 3
        },
        new ItemRecord
        {
          Nome = "Laka",
          PrecoUnitario = 1.25M,
          Quantidade = 2
        }
      }
    };
    await _repository.Insert(venda);
    var insertedVenda = await _context.Vendas.FindAsync((uint)15);

    Assert.That(insertedVenda.Id, Is.EqualTo(15));
    Assert.That(insertedVenda.Vendedor.Id, Is.EqualTo(2));
    Assert.That(insertedVenda.Vendedor.Nome, Is.EqualTo("Victoria"));
    Assert.That(insertedVenda.Itens.Any((item) => item.Nome == "Galak"));
  }

  [Test]
  public async Task Update_ShouldChangeValue()
  {
    var venda = await _repository.GetById(1);
    venda.Status = EStatus.PAGAMENTO_APROVADO;
    await _repository.Update(venda);
    var vendaAtualizada = await _repository.GetById(1);

    Assert.That(vendaAtualizada.Status, Is.EqualTo(EStatus.PAGAMENTO_APROVADO));
  }
}