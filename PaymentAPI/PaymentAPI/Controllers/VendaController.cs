namespace PaymentAPI.Controllers;

[Produces("application/json")]
[ApiController]
[Route("[controller]")]
public class VendaController : ControllerBase
{
  private readonly VendaService _service;
  public VendaController(VendaService service)
  {
    _service = service;
  }

  /// <summary>
  /// Cria uma nova Venda
  /// </summary>
  /// <response code="201">
  /// Se a nova Venda é válida, retorna a Venda criada
  /// </response>
  /// <response code="422">
  /// Se a quantidade de Itens é menor que 1, retorna uma mensagem de erro
  /// </response>
  [ProducesResponseType(typeof(VendaResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status422UnprocessableEntity)]
  [HttpPost]
  public async Task<IActionResult> Create(VendaRequest request)
  {
    var venda = await _service.Create(request);
    return CreatedAtAction(
      actionName: nameof(GetById),
      routeValues: new { id = venda.Id },
      value: venda
    );
  }

  /// <summary>
  /// Busca Venda por Id
  /// </summary>
  /// <param name="id">Id da Venda a ser buscada</param>
  /// <response code="200">
  /// Se o Id existe, retorna a Venda
  /// </response>
  /// <response code="404">
  /// Se o Id não existe, retorna uma mensagem de erro
  /// </response>
  [ProducesResponseType(typeof(VendaResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
  [HttpGet("{id}")]
  public async Task<IActionResult> GetById(uint id)
  {
    return Ok(value: await _service.GetById(id));
  }

  /// <summary>
  /// Atualiza Status da Venda do Id informado
  /// </summary>
  /// <param name="id">Id da Venda a ter o Status atualizado</param>
  /// <param name="request">Novo Status</param>
  /// <response code="200">
  /// Se o Id existe e o novo Status é válido, retorna a Venda com status atualizado
  /// </response>
  /// <response code="400">
  /// Se a atualização de Status é inválida, retorna uma mensagem de erro
  /// </response>
  /// <response code="404">
  /// Se o Id não existe, retorna uma mensagem de erro
  /// </response>
  [ProducesResponseType(typeof(VendaResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
  [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
  [HttpPatch("{id}")]
  public async Task<IActionResult> UpdateStatus(uint id, [FromBody] StatusRequest request)
  {
    return Ok(value: await _service.UpdateStatus(id, request));
  }
}
