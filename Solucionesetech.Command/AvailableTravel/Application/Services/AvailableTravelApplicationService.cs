using AutoMapper;

using Solucionesetech.Command.AvailableTravel.Application.Interfaces;



using Solucionesetech.Dtos.AvailableTravel.Response;
using Solucionesetech.Dtos.Common.Response;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solucionesetech.DataAccess.Repositories;
using Solucionesetech.DataAccess;
using Solucionesetech.Command.Common.DomainModel.Entities;
using DataAccesModel = Solucionesetech.DataAccess.Models;
using Solucionesetech.Dtos.AvailableTravel.Request;
using Solucionesetech.Dtos.Common;

namespace Solucionesetech.Command.AvailableTravel.Application.Services
{


    public class AvailableTravelApplicationService : IAvailableTravelApplicationService
    {
        private static AvailableTravelRepository oAvailableTravelRepository;
        private static UnitOfWork oUnitOfWork;


        public AvailableTravelApplicationService(string connectionString)
        {

            oUnitOfWork = new UnitOfWork(connectionString);
            oAvailableTravelRepository = oUnitOfWork.AvailableTravelRepository;
        }

        public async Task Delete(int AvailableTravelId)
        {
            if (!oAvailableTravelRepository.VerifyDelete(AvailableTravelId))
            {
                throw new SETECHApplicationException("Hay viajes relacionados al viaje disponible seleccinado.");
            }
            await oAvailableTravelRepository.Delete(AvailableTravelId);
        }

        public async Task<SearchPaginatedResponseDto<SearchAvailableTravelResponseDto>> GetAll()
        {
            return await oAvailableTravelRepository.GetAll(new SearchPaginatedAvailableTravelRequestDto());
        }

        public async Task<SearchPaginatedResponseDto<SearchAvailableTravelResponseDto>> SearchPaginated(SearchPaginatedAvailableTravelRequestDto filters)
        {
            return await oAvailableTravelRepository.GetAll(filters);
        }

        public DetailAvailableTravelResponseDto GetById(int AvailableTravelId)
        {
            return oAvailableTravelRepository.Get(AvailableTravelId);
        }
        public void Update(UpdateAvailableTravelRequestDto requestDto)
        {
            oAvailableTravelRepository.Update(requestDto);
        }

        public async Task Add(AddAvailableTravelRequestDto requestDto)
        {

            var configuration = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<AddAvailableTravelRequestDto, DataAccesModel.AvailableTravel>()
                        .ForMember(x => x.AvailableTravelId, opt => opt.Ignore());
            });




            // only during development, validate your mappings; remove it before release
            //configuration.AssertConfigurationIsValid();               
            var mapper = configuration.CreateMapper();

            DataAccesModel.AvailableTravel registerAvailableTravel = mapper.Map<AddAvailableTravelRequestDto, DataAccesModel.AvailableTravel>(requestDto);
            await oAvailableTravelRepository.Add(registerAvailableTravel);



        }

    }
}