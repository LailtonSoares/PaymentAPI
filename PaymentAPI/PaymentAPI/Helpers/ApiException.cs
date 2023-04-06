namespace PaymentAPI.Helpers;

public class ApiException : Exception
{
  public int Status { get; set; }
  public ApiException(string message, int status) : base(message)
  {
    Status = status;
  }
}