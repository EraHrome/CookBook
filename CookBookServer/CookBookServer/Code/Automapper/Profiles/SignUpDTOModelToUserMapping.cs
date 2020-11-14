using AutoMapper;
using CookBookServer.Enums;
using DTOModels;
using Mongo.Models;
using System;

namespace CookBookServer.Code.Automapper.Profiles
{
    public class SignUpDTOModelToUserMapping : Profile
    {
        public SignUpDTOModelToUserMapping()
        { 
            CreateMap<SignUpDTOModel, User>()
                .ForMember(w => w.Id, w => w.MapFrom(n => Guid.NewGuid().ToString()))
                .ForMember(w => w.IsConfirmed, w => w.MapFrom(n => true))
                .ForMember(w => w.LastName, w => w.MapFrom(n => n.LastName.Trim()))
                .ForMember(w => w.Login, w => w.MapFrom(n => n.Login.Trim()))
                .ForMember(w => w.Password, w => w.MapFrom(n => n.Password))
                .ForMember(w => w.Rating, w => w.MapFrom(n => 0))
                .ForMember(w => w.Role, w => w.MapFrom(n => UserRoleEnum.User));
        }
    }
}
