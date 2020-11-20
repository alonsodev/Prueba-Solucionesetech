
using Solucionesetech.DataAccess.Models;
using Solucionesetech.Dtos.Origin.Request;
using Solucionesetech.Dtos.Origin.Response;
using Microsoft.EntityFrameworkCore;
using Solucionesetech.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EFCore.BulkExtensions;
using Solucionesetech.Dtos.Common.Response;
using Solucionesetech.DataAccess.Common;

namespace Solucionesetech.DataAccess.Repositories
{
    public class OriginRepository : Repository
    {
        internal OriginRepository(DbContextOptions<TRAVELSContext> options)
           : base(options)
        {
        }

        public async Task<SearchPaginatedResponseDto<SearchOriginResponseDto>> GetAll(SearchPaginatedOriginRequestDto filters)
        {
            using (var db = this.GenerateNewContext())
            {
                SearchPaginatedResponseDto<SearchOriginResponseDto> resultado = new SearchPaginatedResponseDto<SearchOriginResponseDto>();

                var searchBy = (filters.search != null) ? filters.search.value : null;

                string sortBy = "";
                string sortDir = "";

                if (filters.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = filters.columns[filters.order[0].column].data;
                    sortDir = filters.order[0].dir.ToLower();
                }

                IQueryable<Origin> queryFilters = db.Origins;

                int count_records = await queryFilters.CountAsync();
                int count_records_filtered = count_records;
                count_records_filtered = await queryFilters.CountAsync();

                if (String.IsNullOrWhiteSpace(searchBy) == false)
                {
                    queryFilters = queryFilters.Where(s => s.Name.ToLower().Contains(filters.search.value));
                }

                var query = queryFilters.Select(a => new SearchOriginResponseDto
                {
                    OriginId = a.OriginId,
                    Name = a.Name
                });

                if (String.IsNullOrEmpty(sortBy)) sortBy = "OriginId";
                if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
                string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
                if (sortExpression.Trim() != "")
                    query = OrderByDinamic.OrderBy<SearchOriginResponseDto>(query, sortExpression.Trim());

                if (filters.start == 0 && filters.length == 0)
                {
                    resultado.data = query.ToList(); 
                }
                else {
                    resultado.data = query.Skip(filters.start).Take(filters.length).ToList();
                }
                

                resultado.recordsTotal = count_records;

                resultado.recordsFiltered = count_records_filtered;
                resultado.draw = filters.draw;
                return resultado;
            }
        }

        public DetailOriginResponseDto Get(int? OriginId)
        {
            using (var db = this.GenerateNewContext())
            {
                return db.Origins.Select(p => new DetailOriginResponseDto
                {
                    OriginId = p.OriginId,
                    Name = p.Name,
                    //Order=p.or


                }).FirstOrDefault(x => x.OriginId == OriginId); ;
            }
        }
        public bool VerifyDelete(int OriginId)
        {
            using (var db = this.GenerateNewContext())
            {
                var count = db.AvailableTravels.Where(x => x.OriginId == OriginId).Count();
                if (count > 0)
                    return false;

                return true;
            }
        }

        public async Task Delete(int OriginId)
        {
            using (var db = this.GenerateNewContext())
            {
               await  db.Origins.Where(x => x.OriginId == OriginId).BatchDeleteAsync(); ;

            }
        }

        public async Task<int> Add(Origin Origin)
        {
            using (var db = this.GenerateNewContext())
            {
                await db.Origins.AddAsync(Origin);
                await db.SaveChangesAsync();
                return Origin.OriginId;
            }
        }

        public void Update(UpdateOriginRequestDto Origin)
        {
            using (var db = this.GenerateNewContext())
            {
                db.Origins.Where(x => x.OriginId == Origin.OriginId).BatchUpdate(x => new Origin()
                {
                    Name = Origin.Name,
                });
            }
        }
    }
}
