using AutoMapper;
using BookInformation.Application.DTOs;
using BookInformation.Domain.Entities;

namespace BookInformation.Application.Mappings;

public class ConfigureMapping : Profile
{
    public ConfigureMapping()
    {
        #region Book
        CreateMap<Book, BookInfoDto>()
            .ForCtorParam("AuthorIds", opt =>
                opt.MapFrom(src => src.Authors.Select(a => a.AuthorId)))
            .ForCtorParam("AuthorNames", opt =>
                opt.MapFrom(src => string.Join(", ", src.Authors.Where(a => a.Author != null).Select(a => $"{a.Author.FirstName} {a.Author.LastName}"))));

        #endregion

        #region Author
        CreateMap<Author, AuthorDto>();

        #endregion

        #region AuditLog
        CreateMap<AuditLog, AuditLogDto>();
            
        #endregion

    }

}
