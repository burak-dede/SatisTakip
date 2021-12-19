using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SatisTakip.Models;
using System.Linq;

namespace SatisTakip.DAL
{
    public class AccountInitializer : System.Data.Entity.CreateDatabaseIfNotExists<ApplicationDbContext>
    {

    protected override void Seed(ApplicationDbContext context){

                if (!context.Roles.Any(r => r.Name == "Administrator"))
    {
        var store = new RoleStore<IdentityRole>(context);
        var manager = new RoleManager<IdentityRole>(store);
        var role = new IdentityRole { Name = "Administrator" };

        manager.Create(role);
    }

    if (!context.Users.Any(u => u.UserName == "admin"))
    {
        var store = new UserStore<ApplicationUser>(context);
        var manager = new UserManager<ApplicationUser>(store);
        var user = new ApplicationUser { UserName = "admin", Email = "admin@companydomain.xyz" };

        manager.Create(user, "4qFew3%6");
        manager.AddToRole(user.Id, "Administrator");
    }

            base.Seed(context);
        }


    }
}