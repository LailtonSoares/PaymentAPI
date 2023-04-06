namespace PaymentAPI.Models.Record;

public interface IRecord<Response>
{
  Response ToResponse();
}