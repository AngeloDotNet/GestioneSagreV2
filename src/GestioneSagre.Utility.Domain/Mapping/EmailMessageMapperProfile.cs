using AutoMapper;
using GestioneSagre.Utility.Domain.Models.ViewModels;
using GestioneSagre.Utility.Infrastructure.Entities;

namespace GestioneSagre.Utility.Domain.Mapping;

public class EmailMessageMapperProfile : Profile
{
    public EmailMessageMapperProfile()
    {
        CreateMap<EmailMessage, EmailMessageViewModel>();
    }
}