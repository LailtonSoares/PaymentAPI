namespace PaymentAPI.Models.Request;

public interface IRequest<Record>
{
  Record ToRecord();
}