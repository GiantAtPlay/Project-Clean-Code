using System.Collections.Generic;

namespace Application.Domain.Data
{
    public interface IManager<T>
    {
        int Create(T model);
        void Update(T model);
        void Delete(int id);
    }
}