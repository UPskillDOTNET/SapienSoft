using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicParkAPI.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<ActionResult<IEnumerable<T>>> GetAllAsync();
    }
}
