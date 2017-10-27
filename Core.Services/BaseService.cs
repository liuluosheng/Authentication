using Core.IRepository;
using Data.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public class BaseService<T> where T : EntityBase
    {
        public readonly IBaseRepository<T> Repository;

        public BaseService(IBaseRepository<T> repository)
        {
            Repository = repository;
        }
    }
}
