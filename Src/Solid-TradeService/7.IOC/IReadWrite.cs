
using Functional.Maybe;

public interface IReadWrite<T>
{
    Maybe<T> Load(string id);
    void Save(Maybe<T> entity);
}