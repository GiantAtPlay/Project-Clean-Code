using System;

namespace Application.Data
{
    // [DATA]: All database entity models should inherit from this for consistency
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
    }
}