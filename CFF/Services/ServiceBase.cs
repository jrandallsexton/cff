
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using CFF.Data;

namespace CFF.Services
{
    class ServiceBase
    {
        //private readonly CffContext _context;

        //protected CffContext Context
        //{
        //    get { return _context; }
        //}

        //protected int SaveAndAudit(string username)
        //{
        //    foreach (var entry in Context.ChangeTracker.Entries().Where(e => e.Entity is Entity))
        //    {
        //        var entity = (Entity)entry.Entity;
        //        switch (entry.State)
        //        {
        //            case EntityState.Added:
        //                entity.CreatedBy = username;
        //                entity.CreatedDate = (entity.CreatedDate.Equals(DateTime.MinValue) ? DateTime.UtcNow : entity.CreatedDate.ToUniversalTime());
        //                break;
        //            case EntityState.Modified:
        //                entity.UpdatedBy = username;
        //                entity.UpdatedDate = DateTime.UtcNow;
        //                entry.Property("CreatedBy").IsModified = false;
        //                entry.Property("CreatedDate").IsModified = false;
        //                break;
        //        }
        //    }
        //    return Context.SaveChanges();

        //}
    }
}
