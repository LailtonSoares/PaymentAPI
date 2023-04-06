namespace PaymentAPI.Tests;

[TestFixture]
public class VendaControlleTest : VendaServiceTest
{
  private VendaController _controller;

  [SetUp]
  public void __beforeEach()
  {
    _controller = new VendaController(_service);
  }

  [Test]
  public async Task Create_ShouldReturn201Created()
  {
    var request = new VendaRequest
    {
      Vendedor = new VendedorRequest
      {
        Id = 3,
        Cpf = "33333333333",
        Nome = "Kana",
        Email = "kana@jp.com",
        Telefone = "933333333"
      },
      Itens = new List<ItemRequest>
      {
        new ItemRequest
        {
          Nome = "Oreo",
          PrecoUnitario = 1.5M,
          Quantidade = 1
        }
      }
    };
    var response = (await _controller.Create(request)) as ObjectResult;
    Assert.That(response.StatusCode, Is.EqualTo(201));
  }

  [Test]
  public void Create_ShouldReturn422UnprocessableEntity()
  {
    var request = new VendaRequest
    {
      Vendedor = new VendedorRequest
      {
        Id = 3,
        Cpf = "33333333333",
        Nome = "Kana",
        Email = "kana@jp.com",
        Telefone = "933333333"
      },
      Itens = new List<ItemRequest>()
    };
    var exception = Assert.ThrowsAsync<ItemQuantityApiException>(
      async () => await _controller.Create(request)
    );
    Assert.That(exception.Status, Is.EqualTo(422));
  }

  [Test]
  public async Task GetById_ShouldReturn200OK()
  {
    var response = (await _controller.GetById(1)) as ObjectResult;
    Assert.That(response.StatusCode, Is.EqualTo(200));
  }

  [Test]
  public void GetById_ShouldReturn404NotFound()
  {
    var exception = Assert.ThrowsAsync<InvalidIdApiException>(
      async () => await _controller.GetById(100)
    );
    Assert.That(exception.Status, Is.EqualTo(404));
  }

  [Test]
  public async Task UpdateStatus_ShouldReturn200OK()
  {
    var request = new StatusRequest { Status = EStatus.CANCELADA };
    var response = (await _controller.UpdateStatus(1, request)) as ObjectResult;
    Assert.That(response.StatusCode, Is.EqualTo(200));
  }

  [Test]
  public void UpdateStatus_ShouldReturn404NotFound()
  {
    var request = new StatusRequest { Status = EStatus.CANCELADA };
    var exception = Assert.ThrowsAsync<InvalidIdApiException>(
      async () => await _controller.UpdateStatus(100, request)
    );
    Assert.That(exception.Status, Is.EqualTo(404));
  }

  [Test]
  public void UpdateStatus_ShouldReturn400BadRequest()
  {
    var request = new StatusRequest { Status = EStatus.ENTREGUE };
    var exception = Assert.ThrowsAsync<InvalidOperationApiException>(
      async () => await _controller.UpdateStatus(1, request)
    );
    Assert.That(exception.Status, Is.EqualTo(400));
  }
}