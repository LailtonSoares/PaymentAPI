namespace PaymentAPI.Data;

public interface IRepository<T>
{
  Task<T> GetById(uint id);
  Task Insert(T value);
  Task Update(T value);
}