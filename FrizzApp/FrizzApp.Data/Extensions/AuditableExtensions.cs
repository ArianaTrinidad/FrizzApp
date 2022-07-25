using FrizzApp.Data.Entities;
using System;

namespace FrizzApp.Data.Extensions
{
    public static class AuditableExtensions
    {
        public static void SetCreateAuditFields<T>(this T entity, string createBy)
           where T : AuditableEntity
        {
            entity.CreateAt = DateTime.UtcNow.AddHours(-3);
            entity.CreateBy = createBy;
        }


        public static void SetDeleteAuditFields<T>(this T entity, string deleteBy)
           where T : AuditableEntity
        {
            entity.DeleteAt = DateTime.UtcNow.AddHours(-3);
            entity.DeleteBy = deleteBy;
        }

        public static void SetActualizedAuditFields<T>(this T entity, string updateBy)
   where T : AuditableEntity
        {
            entity.ActualizedAt = DateTime.UtcNow.AddHours(-3);
            entity.ActualizedBy = updateBy;
        }

        public static void SetUpdateAuditFields<T>(this T entity, string updateBy)
           where T : AuditableEntity
        {
            entity.UpdateAt = DateTime.UtcNow.AddHours(-3);
            entity.UpdateBy = updateBy;
        }
    }
}
