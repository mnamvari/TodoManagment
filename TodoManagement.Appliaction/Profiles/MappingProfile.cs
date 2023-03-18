using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TodoManagement.Appliaction.DTOs;
using TodoManagement.Domains;

namespace TodoManagement.Appliaction.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Todo, TodoDto>().ReverseMap();
            CreateMap<TodoForCreationDto, Todo>().ReverseMap();
        }
    }
}
