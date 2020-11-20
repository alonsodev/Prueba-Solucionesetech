
using Solucionesetech.DataAccess.Models;
using Solucionesetech.Dtos.Travel.Request;
using Solucionesetech.Dtos.Travel.Response;
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
    public class TravelRepository : Repository
    {
        internal TravelRepository(DbContextOptions<TRAVELSContext> options)
           : base(options)
        {
        }

        public async Task<SearchPaginatedResponseDto<SearchTravelResponseDto>> GetAll(SearchPaginatedTravelRequestDto filters)
        {
            using (var db = this.GenerateNewContext())
            {
                SearchPaginatedResponseDto<SearchTravelResponseDto> resultado = new SearchPaginatedResponseDto<SearchTravelResponseDto>();

                var searchBy = (filters.search != null) ? filters.search.value : null;

                string sortBy = "";
                string sortDir = "";

                if (filters.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = filters.columns[filters.order[0].column].data;
                    sortDir = filters.order[0].dir.ToLower();
                }

                IQueryable<Travel> queryFilters = db.Travels;

                int count_records = await queryFilters.CountAsync();
                int count_records_filtered = count_records;
                count_records_filtered = await queryFilters.CountAsync();

                if (String.IsNullOrWhiteSpace(searchBy) == false)
                {
                    queryFilters = queryFilters.Where(s => s.AvailableTravel.Code.ToLower().Contains(filters.search.value) ||
                    s.Traveler.Name.ToLower().Contains(filters.search.value) ||
                    s.AvailableTravel.Destination.Name.ToLower().Contains(filters.search.value) ||
                    s.AvailableTravel.Origin.Name.ToLower().Contains(filters.search.value) ||
                    s.AvailableTravel.Price.ToString().ToLower().Contains(filters.search.value) ||
                    s.Traveler.IdentificationDocument.ToLower().Contains(filters.search.value));
                }

                var query = queryFilters.Select(a => new SearchTravelResponseDto
                {
                    TravelId = a.TravelId,
                    TravelerIdentificationDocument = a.Traveler.IdentificationDocument,
                    AvailableTravelCode = a.AvailableTravel.Code,
                    TravelerName = a.Traveler.Name,
                    DestinationName = a.AvailableTravel.Destination.Name,
                    OriginName = a.AvailableTravel.Origin.Name,
                    AvailableTravelPrice = a.AvailableTravel.Price
                });

                if (String.IsNullOrEmpty(sortBy)) sortBy = "TravelerName";
                if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
                string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
                if (sortExpression.Trim() != "")
                    query = OrderByDinamic.OrderBy<SearchTravelResponseDto>(query, sortExpression.Trim());
                resultado.data = query.Skip(filters.start).Take(filters.length).ToList();

                resultado.recordsTotal = count_records;

                resultado.recordsFiltered = count_records_filtered;
                resultado.draw = filters.draw;
                return resultado;
            }
        }

        public async Task<List<DetailTravelResponseDto>> GetAll()
        {
            using (var db = this.GenerateNewContext())
            {
                return await db.Travels.OrderBy(a => a.TravelId).Select(p => new DetailTravelResponseDto
                {
                    TravelId = p.TravelId,
                    AvailableTravelId  = p.AvailableTravelId,
                    TravelerId = p.TravelerId
                }).ToListAsync();
            }
        }

        public DetailTravelResponseDto Get(int? TravelId)
        {
            using (var db = this.GenerateNewContext())
            {
                return db.Travels.Select(p => new DetailTravelResponseDto
                {
                    TravelId = p.TravelId,
                    AvailableTravelId = p.AvailableTravelId,
                    TravelerId = p.TravelerId,


                }).FirstOrDefault(x => x.TravelId == TravelId); ;
            }
        }
        public bool VerifyDelete(int TravelId)
        {

                return true;
           
        }

        public async Task Delete(int TravelId)
        {
            using (var db = this.GenerateNewContext())
            {
                await db.Travels.Where(x => x.TravelId == TravelId).BatchDeleteAsync(); ;

            }
        }

        public async Task<int> Add(Travel Travel)
        {
            using (var db = this.GenerateNewContext())
            {
                await db.Travels.AddAsync(Travel);
                await db.SaveChangesAsync();
                return Travel.TravelId;
            }
        }

        public void Update(UpdateTravelRequestDto Travel)
        {
            using (var db = this.GenerateNewContext())
            {
                db.Travels.Where(x => x.TravelId == Travel.TravelId).BatchUpdate(x => new Travel()
                {
                    AvailableTravelId = Travel.AvailableTravelId,
                    TravelerId = Travel.TravelerId,
                });
            }
        }
    }
}
