using AutoMapper;

using Solucionesetech.Command.Traveler.Application.Interfaces;



using Solucionesetech.Dtos.Traveler.Response;
using Solucionesetech.Dtos.Common.Response;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solucionesetech.DataAccess.Repositories;
using Solucionesetech.DataAccess;
using Solucionesetech.Command.Common.DomainModel.Entities;
using DataAccesModel = Solucionesetech.DataAccess.Models;
using Solucionesetech.Dtos.Traveler.Request;
using Solucionesetech.Dtos.Common;

namespace Solucionesetech.Command.Traveler.Application.Services
{


    public class TravelerApplicationService : ITravelerApplicationService
    {
        private static TravelerRepository oTravelerRepository;
        private static UnitOfWork oUnitOfWork;


        public TravelerApplicationService(string connectionString)
        {

            oUnitOfWork = new UnitOfWork(connectionString);
            oTravelerRepository = oUnitOfWork.TravelerRepository;
        }

        public async Task Delete(int TravelerId)
        {
            if (!oTravelerRepository.VerifyDelete(TravelerId))
            {
                throw new SETECHApplicationException("Hay viajes relacionados al viajero seleccionado.");
            }
            await oTravelerRepository.Delete(TravelerId);
        }

        public async Task<SearchPaginatedResponseDto<SearchTravelerResponseDto>> GetAll()
        {
            return await oTravelerRepository.GetAll(new SearchPaginatedTravelerRequestDto());
        }

        public async Task<SearchPaginatedResponseDto<SearchTravelerResponseDto>> SearchPaginated(SearchPaginatedTravelerRequestDto filters)
        {
            return await oTravelerRepository.GetAll(filters);
        }

        public DetailTravelerResponseDto GetById(int TravelerId)
        {
            return oTravelerRepository.Get(TravelerId);
        }
        public void Update(UpdateTravelerRequestDto requestDto)
        {
            oTravelerRepository.Update(requestDto);
        }

        public async Task Add(AddTravelerRequestDto requestDto)
        {

            var configuration = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<AddTravelerRequestDto, DataAccesModel.Traveler>()
                        .ForMember(x => x.TravelerId, opt => opt.Ignore());
            });




            // only during development, validate your mappings; remove it before release
            //configuration.AssertConfigurationIsValid();               
            var mapper = configuration.CreateMapper();

            DataAccesModel.Traveler registerTraveler = mapper.Map<AddTravelerRequestDto, DataAccesModel.Traveler>(requestDto);
            await oTravelerRepository.Add(registerTraveler);



        }

    }
}