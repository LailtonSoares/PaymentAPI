namespace PaymentAPI.Services;

public class VendaService
{
  private readonly VendaRepository _repository;
  private readonly Dictionary<EStatus, List<EStatus>> _allowedStatusUpdates = new Dictionary<EStatus, List<EStatus>>
  {
    { EStatus.AGUARDANDO_PAGAMENTO,
      new List<EStatus> { EStatus.PAGAMENTO_APROVADO, EStatus.CANCELADA }},
    { EStatus.PAGAMENTO_APROVADO,
      new List<EStatus> { EStatus.ENVIADO_PARA_TRANSPORTADORA, EStatus.CANCELADA }},
    { EStatus.ENVIADO_PARA_TRANSPORTADORA,
      new List<EStatus> { EStatus.ENTREGUE }}
  };

  public VendaService(VendaRepository repository)
  {
    _repository = repository;
  }

  public async Task<VendaResponse> Create(VendaRequest request)
  {
    if (request.Itens.Count() < 1
      || request.Itens.Any((item) => item.Quantidade < 1))
      throw new ItemQuantityApiException();
    var venda = request.ToRecord();
    await _repository.Insert(venda);
    return venda.ToResponse();
  }

  public async Task<VendaResponse> GetById(uint id)
  {
    var venda = await _getRecordById(id);
    return venda.ToResponse();
  }

  public async Task<VendaResponse> UpdateStatus(uint id, [FromBody] StatusRequest request)
  {
    var venda = await _getRecordById(id);
    EStatus newStatus = request.Status;
    if (!_isAllowedToUpdateStatus(venda.Status, newStatus))
      throw new InvalidOperationApiException();
    venda.Status = newStatus;
    await _repository.Update(venda);
    return venda.ToResponse();
  }

  private async Task<VendaRecord> _getRecordById(uint id)
  {
    var venda = await _repository.GetById(id);
    if (venda == null) throw new InvalidIdApiException();
    return venda;
  }

  private bool _isAllowedToUpdateStatus(EStatus previous, EStatus newS)
  {
    return _allowedStatusUpdates.ContainsKey(previous)
      && _allowedStatusUpdates[previous].Contains(newS);
  }
}