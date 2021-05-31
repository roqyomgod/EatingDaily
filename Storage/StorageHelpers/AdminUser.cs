using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EatingDaily.Storage.Entity;
using EatingDaily.Storage;

namespace EatingDaily.Storage.StorageEntity
{
    public static class AdminUser
    {
        public static void Initialize(WorkContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(
                    new User
                    {
                        Email = "Admin",
                        Password = "Admin"
                    }
                );
                context.Profiles.Add(
                    new Profile
                    {
                        Name = "Admin",
                        LastName = "Super",
                        Phone = "77777777777"
                    });
                context.SourceData.Add(
                    new Source
                    {
                        Description = "Не вкусная, но полезная еда.",
                        Url = "http://vkusvill.ru"
                    });
                context.SaveChanges();
            }
        }
    }
}
