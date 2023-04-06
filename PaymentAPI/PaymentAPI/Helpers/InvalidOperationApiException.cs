namespace PaymentAPI.Helpers;

public class InvalidOperationApiException : ApiException
{
  public InvalidOperationApiException()
    : base(
        message: "Operacao invalida.",
        status: StatusCodes.Status400BadRequest
    )
  { }
}