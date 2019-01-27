using AutoMapper;
using EbookShop.Models;
using EbookShop.Services.Dtos;
namespace EbookShop.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
         
            CreateMap<RegistrationDto,AppUser>()
                .ForMember(au=> au.UserName,map => map.MapFrom(vm=>vm.Email));

            CreateMap<AppUser, DashboardDto>();


            CreateMap<AuthorDto, Author>();
            CreateMap<Author, AuthorDto>();

            CreateMap<GenreDto, Genre>();
            CreateMap<Genre, GenreDto>();
            //Enable mapping from EbookGenre to GenreDto
            var ebookGenreToGenreDto = CreateMap<EbookGenre, GenreDto>()
               .ForMember(dest => dest.Name, opts => opts.MapFrom(
                   src => src.Genre.Name));
            //Enable mapping from EbookAuthor to AuthorDto
            var ebookAuthorToAuthorDto = CreateMap<EbookAuthor, AuthorDto>()
                 .ForMember(dest => dest.FirstName, opts => opts.MapFrom(
                      src => src.Author.FirstName))
                 .ForMember(dest => dest.LastName, opts => opts.MapFrom(
                     src => src.Author.LastName));
            // Map diffrent collection names
            CreateMap<EbookDto, Ebook>();
            CreateMap<Ebook, EbookDto>()
                .ForMember(dest => dest.Genres, opts => opts.MapFrom(
                    src => src.EbookGenres))
                .ForMember(dest => dest.Authors, opts => opts.MapFrom(
                    src => src.EbookAuthors));
        }
    }
}
