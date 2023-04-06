namespace PaymentAPI.Tests;

[TestFixture]
public class VendaServiceTest : BaseTest
{
  protected VendaService _service;

  [SetUp]
  public void _beforeEach()
  {
    _service = new VendaService(_repository);
  }

  [TestCase(true)]
  [TestCase(false)]
  public void Create_ShouldThrowItemQuantityApiException(bool hasItemWithQuantidadeZero)
  {
    var request = new VendaRequest
    {
      Vendedor = new VendedorRequest
      {
        Id = 2,
        Cpf = "22222222222",
        Nome = "Victoria",
        Email = "victoria@outro.com",
        Telefone = "922222222"
      },
      Itens = new List<ItemRequest>()
    };
    if (hasItemWithQuantidadeZero)
    {
      request.Itens.Append(
        new ItemRequest
        {
          Nome = "Galak",
          PrecoUnitario = 1.5M,
          Quantidade = 0
        }
      );
    }

    Assert.ThrowsAsync<ItemQuantityApiException>(
      async () => await _service.Create(request)
    );
  }

  [Test]
  public async Task Create_ShouldCreateNewVenda()
  {
    var request = new VendaRequest
    {
      Vendedor = new VendedorRequest
      {
        Id = 2,
        Cpf = "22222222222",
        Nome = "Victoria",
        Email = "victoria@outro.com",
        Telefone = "922222222"
      },
      Itens = new List<ItemRequest>
      {
        new ItemRequest
        {
          Nome = "Galak",
          PrecoUnitario = 1.5M,
          Quantidade = 2
        },
        new ItemRequest
        {
          Nome = "Oreo",
          PrecoUnitario = 1.5M,
          Quantidade = 2
        }
      }
    };

    var venda = await _service.Create(request);
    Assert.That(venda.Vendedor.Nome, Is.EqualTo("Victoria"));
    Assert.That(venda.Itens.Any((item) => item.Nome == "Oreo"));
    Assert.That(venda.Valor, Is.EqualTo(6.0M));
    Assert.That(venda.Status, Is.EqualTo(EStatus.AGUARDANDO_PAGAMENTO));
    Assert.That(venda.Data, Is.InstanceOf(typeof(DateTime)));
  }

  [Test]
  public void GetById_ShouldThrowInvalidIdApiException()
  {
    Assert.ThrowsAsync<InvalidIdApiException>(
      async () => await _service.GetById(100)
    );
  }

  [Test]
  public async Task GetById_ShouldFindTheVenda()
  {
    var venda = await _service.GetById(1);
    Assert.That(venda.Vendedor.Nome, Is.EqualTo("Gabriel"));
    Assert.That(venda.Itens.Any((item) => item.Nome.Contains("Item")));
    Assert.That(venda.Valor, Is.EqualTo(0));
    Assert.That(venda.Status, Is.EqualTo(EStatus.AGUARDANDO_PAGAMENTO));
    Assert.That(venda.Data, Is.InstanceOf(typeof(DateTime)));
  }

  [Test]
  public void UpdateStatus_ShouldThrowInvalidIdApiException()
  {
    var request = new StatusRequest { Status = EStatus.CANCELADA };
    Assert.ThrowsAsync<InvalidIdApiException>(
      async () => await _service.UpdateStatus(100, request)
    );
  }

  [Test]
  public void UpdateStatus_ShouldThrowInvalidOperationApiException()
  {
    var request = new StatusRequest { Status = EStatus.ENTREGUE };
    Assert.ThrowsAsync<InvalidOperationApiException>(
      async () => await _service.UpdateStatus(1, request)
    );
  }

  [Test]
  public async Task UpdateStatus_ShoudChangeTheStatus()
  {
    var request = new StatusRequest { Status = EStatus.PAGAMENTO_APROVADO };
    var venda = await _service.UpdateStatus(1, request);
    Assert.That(venda.Id, Is.EqualTo(1));
    Assert.That(venda.Status, Is.EqualTo(EStatus.PAGAMENTO_APROVADO));
  }
}