using AutoMapper;
using System;
using TEST.Dtos;
using TEST.Models;

namespace TEST.Mapper
{
    public class MapperMgr
    {
        private static MapperMgr _instance;
        public IMapper Mapper { get; set; }

        public MapperMgr()
        {
            MapperConfiguration mapperConfig = CreateMapperConfig();
            Mapper = mapperConfig.CreateMapper();
        }

        private MapperConfiguration CreateMapperConfig()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Customer, CustomerDto>();
                cfg.CreateMap<CustomerDto, Customer>();
                cfg.CreateMap<Movie, MovieDto>();
                cfg.CreateMap<MovieDto, Movie>();
            });
        }

        public static MapperMgr Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MapperMgr();

                return _instance;
            }
        }
    }
}