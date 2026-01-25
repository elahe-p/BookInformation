using AutoMapper;
using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;

namespace BookInformation.Application.Mappings;

public class ConfigureMaping : Profile
{
    public ConfigureMaping()
    {
        #region Book
        // CreateMap<BookInfoDto, Book>().ReverseMap();

        CreateMap<Book, BookInfoDto>()
            .ForCtorParam("authorIds", opt => opt.MapFrom(src =>
                src.Authors.Select(a => a.AuthorId)))

            .ForCtorParam("authorNames", opt => opt.MapFrom(src =>
                string.Join(", ", src.Authors.Select(a =>
                    $"{a.Author.FirstName} {a.Author.LastName}"))));

        #endregion

        #region Author
        CreateMap<Author, AuthorDto>();

        #endregion

    }

}
