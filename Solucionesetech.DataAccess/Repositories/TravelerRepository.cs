
using Solucionesetech.DataAccess.Models;
using Solucionesetech.Dtos.Traveler.Request;
using Solucionesetech.Dtos.Traveler.Response;
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
    public class TravelerRepository : Repository
    {
        internal TravelerRepository(DbContextOptions<TRAVELSContext> options)
           : base(options)
        {
        }

        public async Task<SearchPaginatedResponseDto<SearchTravelerResponseDto>> GetAll(SearchPaginatedTravelerRequestDto filters)
        {
            using (var db = this.GenerateNewContext())
            {
                SearchPaginatedResponseDto<SearchTravelerResponseDto> resultado = new SearchPaginatedResponseDto<SearchTravelerResponseDto>();

                var searchBy = (filters.search != null) ? filters.search.value : null;

                string sortBy = "";
                string sortDir = "";

                if (filters.order != null)
                {
                    // in this example we just default sort on the 1st column
                    sortBy = filters.columns[filters.order[0].column].data;
                    sortDir = filters.order[0].dir.ToLower();
                }

                IQueryable<Traveler> queryFilters = db.Travelers;

                int count_records = await queryFilters.CountAsync();
                int count_records_filtered = count_records;
                count_records_filtered = await queryFilters.CountAsync();

                if (String.IsNullOrWhiteSpace(searchBy) == false)
                {
                    queryFilters = queryFilters.Where(s => s.Name.ToLower().Contains(filters.search.value) ||
                    s.IdentificationDocument.ToLower().Contains(filters.search.value) ||
                    s.Phone.ToLower().Contains(filters.search.value) ||
                    s.Address.ToLower().Contains(filters.search.value));
                }

                var query = queryFilters.Select(a => new SearchTravelerResponseDto
                {
                    TravelerId = a.TravelerId,
                    IdentificationDocument = a.IdentificationDocument,
                    Phone = a.Phone,
                    Address = a.Address,
                    Name = a.Name,
                });

                if (String.IsNullOrEmpty(sortBy)) sortBy = "Name";
                if (String.IsNullOrEmpty(sortDir)) sortDir = "asc";
                string sortExpression = sortBy.Trim() + " " + sortDir.Trim();
                if (sortExpression.Trim() != "")
                    query = OrderByDinamic.OrderBy<SearchTravelerResponseDto>(query, sortExpression.Trim());


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

        public DetailTravelerResponseDto Get(int? TravelerId)
        {
            using (var db = this.GenerateNewContext())
            {
                return db.Travelers.Select(p => new DetailTravelerResponseDto
                {
                    TravelerId = p.TravelerId,
                    IdentificationDocument = p.IdentificationDocument,
                    Phone = p.Phone,
                    Address = p.Address,
                    Name = p.Name,


                }).FirstOrDefault(x => x.TravelerId == TravelerId); ;
            }
        }
        public bool VerifyDelete(int TravelerId)
        {
            using (var db = this.GenerateNewContext())
            {
                var count = db.Travels.Where(x => x.TravelerId == TravelerId).Count();
                if (count > 0)
                    return false;

                return true;
            }
        }

        public async Task Delete(int TravelerId)
        {
            using (var db = this.GenerateNewContext())
            {
                await db.Travelers.Where(x => x.TravelerId == TravelerId).BatchDeleteAsync(); ;

            }
        }

        public async Task<int> Add(Traveler Traveler)
        {
            using (var db = this.GenerateNewContext())
            {
                await db.Travelers.AddAsync(Traveler);
                await db.SaveChangesAsync();
                return Traveler.TravelerId;
            }
        }

        public void Update(UpdateTravelerRequestDto Traveler)
        {
            using (var db = this.GenerateNewContext())
            {
                db.Travelers.Where(x => x.TravelerId == Traveler.TravelerId).BatchUpdate(x => new Traveler()
                {
                    IdentificationDocument = Traveler.IdentificationDocument,
                    Phone = Traveler.Phone,
                    Address = Traveler.Address,
                    Name = Traveler.Name,
                });
            }
        }
    }
}
