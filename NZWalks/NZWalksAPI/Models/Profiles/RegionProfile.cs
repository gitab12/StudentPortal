using AutoMapper;
namespace NZWalksAPI.Models.Profiles

{
    public class RegionProfile : Profile
    {

        public RegionProfile()
        {
            // CreateMap<Models.Region, Models.DTO.RegionDTO>().ForMember(dest => dest.Id, options =>
            //options.MapFrom(src => src.Id)).ReverseMap();
           CreateMap<Models.Region, Models.DTO.RegionDTO>().ReverseMap();
        }
    }
}
