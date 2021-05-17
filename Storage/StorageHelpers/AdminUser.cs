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
                        Description = "Самые милые котки",
                        Url = "https://www.google.com/search?q=%D0%A1%D0%B0%D0%BC%D1%8B%D0%B5+%D0%BC%D0%B8%D0%BB%D1%8B%D0%B5+%D0%BA%D0%BE%D1%82%D0%B8%D0%BA%D0%B8&sxsrf=ALeKk020JKWFXrR_HkDtLQas8OYUV5OW1Q:1618931757128&source=lnms&tbm=isch&sa=X&ved=2ahUKEwjmgrLijo3wAhXxk4sKHbGRATkQ_AUoAXoECAEQAw&biw=1745&bih=852"
                    });
                context.SaveChanges();
            }
        }
    }
}
