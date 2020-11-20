using AutoMapper;

using Solucionesetech.Command.Travel.Application.Interfaces;



using Solucionesetech.Dtos.Travel.Response;
using Solucionesetech.Dtos.Common.Response;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solucionesetech.DataAccess.Repositories;
using Solucionesetech.DataAccess;
using Solucionesetech.Command.Common.DomainModel.Entities;
using DataAccesModel = Solucionesetech.DataAccess.Models;
using Solucionesetech.Dtos.Travel.Request;
using Solucionesetech.Dtos.Common;

namespace Solucionesetech.Command.Travel.Application.Services
{


    public class TravelApplicationService : ITravelApplicationService
    {
        private static TravelRepository oTravelRepository;
        private static UnitOfWork oUnitOfWork;


        public TravelApplicationService(string connectionString)
        {

            oUnitOfWork = new UnitOfWork(connectionString);
            oTravelRepository = oUnitOfWork.TravelRepository;
        }

        public async Task Delete(int TravelId)
        {
            if (!oTravelRepository.VerifyDelete(TravelId))
            {
                throw new SETECHApplicationException("Hay viajes relacionados al viajero seleccionado.");
            }
            await oTravelRepository.Delete(TravelId);
        }

        public async Task<SearchPaginatedResponseDto<SearchTravelResponseDto>> GetAll()
        {
            return await oTravelRepository.GetAll(new SearchPaginatedTravelRequestDto());
        }

        public async Task<SearchPaginatedResponseDto<SearchTravelResponseDto>> SearchPaginated(SearchPaginatedTravelRequestDto filters)
        {
            return await oTravelRepository.GetAll(filters);
        }

        public DetailTravelResponseDto GetById(int TravelId)
        {
            return oTravelRepository.Get(TravelId);
        }
        public void Update(UpdateTravelRequestDto requestDto)
        {
            oTravelRepository.Update(requestDto);
        }

        public async Task Add(AddTravelRequestDto requestDto)
        {

            var configuration = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<AddTravelRequestDto, DataAccesModel.Travel>()
                        .ForMember(x => x.TravelId, opt => opt.Ignore());
            });




            // only during development, validate your mappings; remove it before release
            //configuration.AssertConfigurationIsValid();               
            var mapper = configuration.CreateMapper();

            DataAccesModel.Travel registerTravel = mapper.Map<AddTravelRequestDto, DataAccesModel.Travel>(requestDto);
            await oTravelRepository.Add(registerTravel);



        }

    }
}