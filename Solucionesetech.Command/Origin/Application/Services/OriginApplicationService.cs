using AutoMapper;

using Solucionesetech.Command.Origin.Application.Interfaces;



using Solucionesetech.Dtos.Origin.Response;
using Solucionesetech.Dtos.Common.Response;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Solucionesetech.DataAccess.Repositories;
using Solucionesetech.DataAccess;
using Solucionesetech.Command.Common.DomainModel.Entities;
using DataAccesModel = Solucionesetech.DataAccess.Models;
using Solucionesetech.Dtos.Origin.Request;
using Solucionesetech.Dtos.Common;

namespace Solucionesetech.Command.Origin.Application.Services
{


    public class OriginApplicationService : IOriginApplicationService
    {
        private static OriginRepository oOriginRepository;
        private static UnitOfWork oUnitOfWork;


        public OriginApplicationService(string connectionString)
        {

            oUnitOfWork = new UnitOfWork(connectionString);
            oOriginRepository = oUnitOfWork.OriginRepository;
        }

        public async Task Delete(int OriginId)
        {
            if (!oOriginRepository.VerifyDelete(OriginId))
            {
                throw new SETECHApplicationException("Hay viajes disponibles relacionados al origen seleccinado.");
            }
            await oOriginRepository.Delete(OriginId);
        }

        public async Task<SearchPaginatedResponseDto<SearchOriginResponseDto>> GetAll()
        {
            return await oOriginRepository.GetAll(new SearchPaginatedOriginRequestDto());
        }

        public async Task<SearchPaginatedResponseDto<SearchOriginResponseDto>> SearchPaginated(SearchPaginatedOriginRequestDto filters)
        {
            return await oOriginRepository.GetAll(filters);
        }

        public DetailOriginResponseDto GetById(int OriginId)
        {
            return oOriginRepository.Get(OriginId);
        }
        public void Update(UpdateOriginRequestDto requestDto)
        {
            oOriginRepository.Update(requestDto);
        }

        public async Task Add(AddOriginRequestDto requestDto)
        {

            var configuration = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<AddOriginRequestDto, DataAccesModel.Origin>()
                        .ForMember(x => x.OriginId, opt => opt.Ignore());
            });




            // only during development, validate your mappings; remove it before release
            //configuration.AssertConfigurationIsValid();               
            var mapper = configuration.CreateMapper();

            DataAccesModel.Origin registerOrigin = mapper.Map<AddOriginRequestDto, DataAccesModel.Origin>(requestDto);
            await oOriginRepository.Add(registerOrigin);



        }

    }
}