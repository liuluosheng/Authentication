using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Enums
{
    [Flags]
    public enum Operation
    {

        查询 = 1 << 1,
        更新 = 1 << 2,
        创建 = 1 << 3,
        删除 = 1 << 4,
        全部 = 1 << 10
    }
}
