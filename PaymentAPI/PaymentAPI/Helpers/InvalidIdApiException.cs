namespace PaymentAPI.Helpers;

public class InvalidIdApiException : ApiException
{
  public InvalidIdApiException()
    : base(
        message: "Id invalido.",
        status: StatusCodes.Status404NotFound
    )
  { }
}