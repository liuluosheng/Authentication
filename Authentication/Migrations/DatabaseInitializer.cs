using Authentication.Permissions;
using Data;
using Data.Entitys;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Data
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }

    /// <summary>
    /// 创建种子数据
    /// </summary>
    public class DatabaseInitializer : IDatabaseInitializer
    {
        ApplicationDbContext _context;
        public DatabaseInitializer(ApplicationDbContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task SeedAsync()
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);
            if (!await _context.Users.AnyAsync())
            {
                Guid userId = Guid.NewGuid();
                Guid roleId = Guid.NewGuid();
                await _context.Users.AddAsync(new User { Id = userId, Email = "luosheng.liu@worthtec.com", Mobile = "13534138066", Name = "刘罗生", PassWord = "1234567" });
                await _context.Roles.AddAsync(new Role
                {
                    Id = roleId,
                    Name = "Admin",
                    CreatedDate = DateTime.Now,
                    Permissions = JsonConvert.SerializeObject(new Dictionary<Modules, Operations[]> {
                         { Modules.产品中心,new []{ Operations.Read } },
                         { Modules.关于我们, new[]{ Operations.Read,Operations.Create } }
                    }, new StringEnumConverter()),
                    Users = new List<UserRole>()
                    {
                       new UserRole{ RoleId=roleId, UserId=userId}
                    }
                });
                await _context.SaveChangesAsync();
            }
        }
    }
}
