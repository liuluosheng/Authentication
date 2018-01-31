using Core.IServices;
using Core.Service;
using Data.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        public RoleService(DbContext context) : base(context)
        {
        }
    }
}
