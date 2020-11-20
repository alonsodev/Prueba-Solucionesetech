
using Solucionesetech.DataAccess.Models;
using Solucionesetech.Dtos.AvailableTravel.Request;
using Solucionesetech.Dtos.AvailableTravel.Response;
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
    public class AvailableTravelRepository : Repository
    {
        internal AvailableTravelRepository(DbContextOptions<TRAVELSContext> options)
           : base(options)
        {
        }

        public async Task<SearchPaginatedResponseDto<SearchAvailableTravelResponseDto>> GetAll(SearchPaginatedAvailableTravelRequestDto filters)
        {
            using (var db = this.GenerateNewContext())
            {
                SearchPaginatedResponseDto<SearchAvailableTravelResponseDto> resultado = new SearchPaginatedResponseDto<SearchAvailableTravelResponseDto>();

                var searchBy = (filters.search != null) ? filters.search.value : null;

                string sortBy = "";
                string sortDir = "";

                if (filters.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = filters.columns[filters.order[0].column].data;
                    sortDir = filters.order[0].dir.ToLower();
                }

                IQueryable<AvailableTravel> queryFilters = db.AvailableTravels;

                int count_records = await queryFilters.CountAsync();
                int count_records_filtered = count_records;
                count_records_filtered = await queryFilters.CountAsync();

                if (String.IsNullOrWhiteSpace(searchBy) == false)
                {
                    queryFilters = queryFilters.Where(s => s.Code.ToLower().Contains(filters.search.value) ||
                    s.Capacity.ToString().ToLower().Contains(filters.search.value) ||
                    s.Price.ToString().ToLower().Contains(filters.search.value) ||
                    s.Destination.Name.ToLower().Contains(filters.search.value) ||
                    s.Origin.Name.ToLower().Contains(filters.search.value));
                }

                var query = queryFilters.Select(a => new SearchAvailableTravelResponseDto
                {
                    AvailableTravelId = a.AvailableTravelId,
                    Code = a.Code,
                    DestinationId = a.DestinationId,
                    OriginId = a.OriginId,
                    Capacity = a.Capacity,
                    Price = a.Price,
                    DestinationName = a.Destination.Name,
                    OriginName = a.Origin.Name
                });

                if (String.IsNullOrEmpty(sortBy)) sortBy = "Code";
                if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
                string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
                if (sortExpression.Trim() != "")
                    query = OrderByDinamic.OrderBy<SearchAvailableTravelResponseDto>(query, sortExpression.Trim());


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

        public DetailAvailableTravelResponseDto Get(int? AvailableTravelId)
        {
            using (var db = this.GenerateNewContext())
            {
                return db.AvailableTravels.Select(p => new DetailAvailableTravelResponseDto
                {
                    AvailableTravelId = p.AvailableTravelId,
                    Code = p.Code,
                    DestinationId = p.DestinationId,
                    OriginId = p.OriginId,
                    Capacity = p.Capacity,
                    Price = p.Price,


                }).FirstOrDefault(x => x.AvailableTravelId == AvailableTravelId); ;
            }
        }
        public bool VerifyDelete(int AvailableTravelId)
        {
            using (var db = this.GenerateNewContext())
            {
                var count = db.Travels.Where(x => x.AvailableTravelId == AvailableTravelId).Count();
                if (count > 0)
                    return false;

                return true;
            }
        }

        public async Task Delete(int AvailableTravelId)
        {
            using (var db = this.GenerateNewContext())
            {
               await  db.AvailableTravels.Where(x => x.AvailableTravelId == AvailableTravelId).BatchDeleteAsync(); ;

            }
        }

        public async Task<int> Add(AvailableTravel AvailableTravel)
        {
            using (var db = this.GenerateNewContext())
            {
                await db.AvailableTravels.AddAsync(AvailableTravel);
                await db.SaveChangesAsync();
                return AvailableTravel.AvailableTravelId;
            }
        }

        public void Update(UpdateAvailableTravelRequestDto AvailableTravel)
        {
            using (var db = this.GenerateNewContext())
            {
                db.AvailableTravels.Where(x => x.AvailableTravelId == AvailableTravel.AvailableTravelId).BatchUpdate(x => new AvailableTravel()
                {
                    Code = AvailableTravel.Code,
                    DestinationId = AvailableTravel.DestinationId,
                    OriginId = AvailableTravel.OriginId,
                    Capacity = AvailableTravel.Capacity,
                    Price = AvailableTravel.Price,
                });
            }
        }
    }
}
