
using Cff.Core.Interfaces;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cff.Core.Models
{
    public abstract class Entity<TKey> : IEntity<TKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public TKey Id { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Modified { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid? ModifiedBy { get; set; }
    }
}