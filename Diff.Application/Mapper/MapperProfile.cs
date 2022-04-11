using AutoMapper;
using Diff.Application.Commands;
using Diff.Domain.Entities;

namespace Diff.Application.Mapper
{
    public class MapperProfile : Profile 
    {
        public MapperProfile() {
            CreateMap < InputModel, AddInputCommand > ().ReverseMap();
        }
    }
}