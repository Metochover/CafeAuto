using AutoMapper;
using NewCafeAuto.DTO.AddressDTO;
using NewCafeAuto.DTO.CafeDTO;
using NewCafeAuto.DTO.CardDTO;
using NewCafeAuto.DTO.CategoryDTO;
using NewCafeAuto.DTO.CompanyDTO;
using NewCafeAuto.DTO.MenuDTO;
using NewCafeAuto.DTO.ProductDTO;
using NewCafeAuto.DTO.ProfileDTO;
using NewCafeAuto.DTO.UserDTO;
using NewCafeAuto.Models;

namespace CafeAutoNew.Mapper_Profile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Users, UserDTO>();
            CreateMap<Addresses, AddressDTO>();
            CreateMap<Profiles, ProfileDTO>();
            CreateMap<Addresses, UserAddressDTO>();
            CreateMap<Profiles, UserProfileDTO>();
            CreateMap<Cards, CardDTO>();
            CreateMap<Company, CompanyDTO>();
            CreateMap<Cafe, CafeDTO>();
            CreateMap<Cafe, AddCafeDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Menu, MenuDTO>();
            CreateMap<Product, ProductDTO>();
        }

    }
}
