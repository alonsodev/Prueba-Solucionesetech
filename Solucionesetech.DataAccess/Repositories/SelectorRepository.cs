//using Solucionesetech.DataAccess.Selectors.Repositories;
using Solucionesetech.DataAccess.Common;
using Solucionesetech.DataAccess.Models;

using Solucionesetech.Dtos.Selectors.Response;
using Solucionesetech.Dtos.Common.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Solucionesetech.Dtos.Users.Request;

namespace Solucionesetech.DataAccess.Repositories
{
    public class SelectorRepository : Repository
    {
        internal SelectorRepository(DbContextOptions<TRAVELSContext> options)
           : base(options)
        {
        }
        public async Task<List<ItemSelectorResponseDto>> GetDestinations()
        {
            using (var db = this.GenerateNewContext())
            {
                return await db.Destinations.Select(p => new ItemSelectorResponseDto
                {
                    Id = p.DestinationId,
                    Name = p.Name
                }).ToListAsync();
            }
        }

        public async Task<List<ItemSelectorResponseDto>> GetOrigins()
        {
            using (var db = this.GenerateNewContext())
            {
                return await  db.Origins.Select(p=> new ItemSelectorResponseDto
                {
                    Id = p.OriginId,
                    Name = p.Name
                }).ToListAsync(); 
            }

           
        }
    }
}
