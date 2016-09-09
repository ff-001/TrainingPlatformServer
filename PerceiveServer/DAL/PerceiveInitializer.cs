using PerceiveServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerceiveServer.DAL
{
    public class PerceiveInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<PerceiveContext>
    {
        protected override void Seed(PerceiveContext context)
        {
            base.Seed(context);
            var users = new List<User>
            {
                new User { FullName = "Siri Wang", Age = 24, CreateDate = DateTime.UtcNow }
            };

            users.ForEach(s => context.Users.Add(s));
            context.SaveChanges();
        }
    }
}