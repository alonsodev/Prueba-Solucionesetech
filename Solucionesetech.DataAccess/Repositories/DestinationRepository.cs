using Microsoft.EntityFrameworkCore;
using Solucionesetech.DataAccess.Models;
using Solucionesetech.Dtos.Destination.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using EFCore.BulkExtensions;
using Solucionesetech.Dtos.Destination.Request;
using Solucionesetech.Dtos.Common.Response;
using Solucionesetech.DataAccess.Common;

namespace Solucionesetech.DataAccess.Repositories
{
    public class DestinationRepository : Repository
    {
        internal DestinationRepository(DbContextOptions<TRAVELSContext> options)
           : base(options)
        {
        }

        public async Task<SearchPaginatedResponseDto<SearchDestinationResponseDto>> GetAll(SearchPaginatedDestinationRequestDto filters)
        {
            using (var db = this.GenerateNewContext())
            {
                SearchPaginatedResponseDto<SearchDestinationResponseDto> resultado = new SearchPaginatedResponseDto<SearchDestinationResponseDto>();

                var searchBy = (filters.search != null) ? filters.search.value : null;

                string sortBy = "";
                string sortDir = "";

                if (filters.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = filters.columns[filters.order[0].column].data;
                    sortDir = filters.order[0].dir.ToLower();
                }

                IQueryable<Destination> queryFilters = db.Destinations;

                int count_records = await queryFilters.CountAsync();
                int count_records_filtered = count_records;
                count_records_filtered = await queryFilters.CountAsync();

                if (String.IsNullOrWhiteSpace(searchBy) == false)
                {
                    queryFilters = queryFilters.Where(s => s.Name.ToLower().Contains(filters.search.value));
                }

                var query = queryFilters.Select(a => new SearchDestinationResponseDto
                {
                    DestinationId = a.DestinationId,
                    Name = a.Name
                });

                if (String.IsNullOrEmpty(sortBy)) sortBy = "DestinationId";
                if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
                string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
                if (sortExpression.Trim() != "")
                    query = OrderByDinamic.OrderBy<SearchDestinationResponseDto>(query, sortExpression.Trim());

                if (filters.start == 0 && filters.length == 0)
                {
                    resultado.data = query.ToList();
                }
                else
                {
                    resultado.data = query.Skip(filters.start).Take(filters.length).ToList();
                }


                resultado.recordsTotal = count_records;

                resultado.recordsFiltered = count_records_filtered;
                resultado.draw = filters.draw;
                return resultado;
            }
        }

        public DetailDestinationResponseDto Get(int? DestinationId)
        {
            using (var db = this.GenerateNewContext())
            {
                return db.Destinations.Select(p => new DetailDestinationResponseDto
                {
                    DestinationId = p.DestinationId,
                    Name = p.Name,
                    //Order=p.or


                }).FirstOrDefault(x => x.DestinationId == DestinationId); ;
            }
        }
        public bool VerifyDelete(int DestinationId)
        {
            using (var db = this.GenerateNewContext())
            {
                var count = db.AvailableTravels.Where(x => x.DestinationId == DestinationId).Count();
                if (count > 0)
                    return false;

                return true;
            }
        }

        public async Task Delete(int DestinationId)
        {
            using (var db = this.GenerateNewContext())
            {
                await db.Destinations.Where(x => x.DestinationId == DestinationId).BatchDeleteAsync(); ;

            }
        }

        public async Task<int> Add(Destination Destination)
        {
            using (var db = this.GenerateNewContext())
            {
                await db.Destinations.AddAsync(Destination);
                await db.SaveChangesAsync();
                return Destination.DestinationId;
            }
        }

        public void Update(UpdateDestinationRequestDto Destination)
        {
            using (var db = this.GenerateNewContext())
            {
                db.Destinations.Where(x => x.DestinationId == Destination.DestinationId).BatchUpdate(x => new Destination()
                {
                    Name = Destination.Name,
                });
            }
        }
    }
}
