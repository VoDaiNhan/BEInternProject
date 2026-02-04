using AutoMapper;
using BEInternProject.Application.DTOs;
using BEInternProject.Domain.Entities;

namespace BEInternProject.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<RegisterDto, User>();
    }
}
