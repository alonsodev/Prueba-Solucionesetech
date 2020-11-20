using AutoMapper;

using Solucionesetech.Command.Destination.Application.Interfaces;



using Solucionesetech.Dtos.Destination.Response;
using Solucionesetech.Dtos.Common.Response;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solucionesetech.DataAccess.Repositories;
using Solucionesetech.DataAccess;
using Solucionesetech.Command.Common.DomainModel.Entities;
using DataAccesModel = Solucionesetech.DataAccess.Models;
using Solucionesetech.Dtos.Destination.Request;
using Solucionesetech.Dtos.Common;

namespace Solucionesetech.Command.Destination.Application.Services
{


    public class DestinationApplicationService : IDestinationApplicationService
    {
        private static DestinationRepository oDestinationRepository;
        private static UnitOfWork oUnitOfWork;


        public DestinationApplicationService(string connectionString)
        {

            oUnitOfWork = new UnitOfWork(connectionString);
            oDestinationRepository = oUnitOfWork.DestinationRepository;
        }

        public async Task Delete(int DestinationId)
        {
            if (!oDestinationRepository.VerifyDelete(DestinationId))
            {
                throw new SETECHApplicationException("Hay viajes disponibles relacionados al origen seleccinado.");
            }
            await oDestinationRepository.Delete(DestinationId);
        }

        public async Task<SearchPaginatedResponseDto<SearchDestinationResponseDto>> GetAll()
        {
            return await oDestinationRepository.GetAll(new SearchPaginatedDestinationRequestDto());
        }

        public async Task<SearchPaginatedResponseDto<SearchDestinationResponseDto>> SearchPaginated(SearchPaginatedDestinationRequestDto filters)
        {
            return await oDestinationRepository.GetAll(filters);
        }

        public DetailDestinationResponseDto GetById(int DestinationId)
        {
            return oDestinationRepository.Get(DestinationId);
        }
        public void Update(UpdateDestinationRequestDto requestDto)
        {
            oDestinationRepository.Update(requestDto);
        }

        public async Task Add(AddDestinationRequestDto requestDto)
        {

            var configuration = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<AddDestinationRequestDto, DataAccesModel.Destination>()
                        .ForMember(x => x.DestinationId, opt => opt.Ignore());
            });




            // only during development, validate your mappings; remove it before release
            //configuration.AssertConfigurationIsValid();               
            var mapper = configuration.CreateMapper();

            DataAccesModel.Destination registerDestination = mapper.Map<AddDestinationRequestDto, DataAccesModel.Destination>(requestDto);
            await oDestinationRepository.Add(registerDestination);



        }

    }
}