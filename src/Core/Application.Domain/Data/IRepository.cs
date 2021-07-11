using System.Collections.Generic;

namespace Application.Domain.Data
{
    public interface IRepository<T>
    {
        T FindById(int id);
        ICollection<T> FindAll();
    }
}