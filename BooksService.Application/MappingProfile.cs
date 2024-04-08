using AutoMapper;
using BooksService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksService.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Item, BooksResponse>()
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.volumeInfo.language))
                .ForMember(dest => dest.title, opt => opt.MapFrom(src => src.volumeInfo.title))
                .ForMember(dest => dest.publisher, opt => opt.MapFrom(src => src.volumeInfo.publisher))
                .ForMember(dest => dest.publishedDate, opt => opt.MapFrom(src => src.volumeInfo.publishedDate))
                .ForMember(dest => dest.description, opt => opt.MapFrom(src => src.volumeInfo.description))
                .ForMember(dest => dest.categories, opt => opt.MapFrom(src => src.volumeInfo.categories));
            // Add other mappings if any
        }
    }
}
