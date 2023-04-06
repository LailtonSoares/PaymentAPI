namespace PaymentAPI.Helpers;

public class ItemQuantityApiException : ApiException
{
  public ItemQuantityApiException()
    : base(
        message: "A inclusão de uma venda deve possuir pelo menos 1 item.",
        status: StatusCodes.Status422UnprocessableEntity
    )
  { }
}