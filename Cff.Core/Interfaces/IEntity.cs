
using System;

namespace Cff.Core.Interfaces
{
    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
        DateTime Created { get; set; }
        DateTime? Modified { get; set; }
        Guid CreatedBy { get; set; }
        Guid? ModifiedBy { get; set; }
    }
}