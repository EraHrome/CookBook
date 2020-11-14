﻿using AutoMapper;
using CookBookServer.Enums;
using CookBookServer.Models.DTO.Auth;
using CookBookServer.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBookServer.Code.Automapper.Profiles
{
    public class SignUpDTOModelToUserMapping : Profile
    {
        public SignUpDTOModelToUserMapping()
        { 
            CreateMap<SignUpDTOModel, User>()
                .ForMember(w => w.Id, w => w.MapFrom(n => Guid.NewGuid().ToString()))
                .ForMember(w => w.IsConfirmed, w => w.MapFrom(n => false))
                .ForMember(w => w.LastName, w => w.MapFrom(n => n.LastName))
                .ForMember(w => w.Login, w => w.MapFrom(n => n.Login))
                .ForMember(w => w.Password, w => w.MapFrom(n => n.Password))
                .ForMember(w => w.Rating, w => w.MapFrom(n => 0))
                .ForMember(w => w.Role, w => w.MapFrom(n => UserRoleEnum.User));
        }
    }
}