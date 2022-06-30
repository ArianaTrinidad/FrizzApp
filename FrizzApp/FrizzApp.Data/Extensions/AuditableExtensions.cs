using FrizzApp.Data.Entities;
using System;

namespace FrizzApp.Data.Extensions
{
    public static class AuditableExtensions
    {
        public static void SetCreationAuditFields<T>(this T entity, string createBy)
           where T : AuditableEntity
        {
            entity.CreateAt = DateTime.UtcNow.AddHours(-3);
            entity.CreateBy = createBy;
        }


        public static void SetDelationAuditFields<T>(this T entity, string deleteBy)
           where T : AuditableEntity
        {
            entity.DeleteAt = DateTime.UtcNow.AddHours(-3);
            entity.DeleteBy = deleteBy;
        }
    }
}
