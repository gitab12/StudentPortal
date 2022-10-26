using AutoMapper;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Models.Profiles
{
    public class WalksProfile : Profile
    {
         public WalksProfile()
        {
            CreateMap<Models.Domain.Walks, Models.DTO.WalksDTO>().ReverseMap();
        }
    }
}
