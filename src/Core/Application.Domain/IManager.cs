using System.Collections.Generic;

namespace Application.Domain
{
    public interface IManager<T>
    {
        void Create(T model);
        void Delete(T model);
        T Get(int id);
        ICollection<T> Get();
    }
}