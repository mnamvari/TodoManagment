using AutoMapper;
using TodoManagment.UI.Models;
using TodoManagment.UI.Services.Base;

namespace TodoManagment.UI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoVM, TodoDto>();
            CreateMap<TodoDto, TodoVM>().ReverseMap();
            CreateMap<DateTimeOffset, DateTime>().ConvertUsing(s => ConvertFromDateTimeOffset(s)); 
            CreateMap<TodoDto, TodoVM>()
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => ConvertFromDateTimeOffset(src.DueDate)));
            CreateMap<TodoForCreationDto, TodoForCreationVM>().ReverseMap()
                .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => ConvertFromDateTimeOffset(src.DueDate)));


        }

        static DateTime ConvertFromDateTimeOffset(DateTimeOffset dateTime)
        {
            if (dateTime.Offset.Equals(TimeSpan.Zero))
                return dateTime.UtcDateTime;
            else if (dateTime.Offset.Equals(TimeZoneInfo.Local.GetUtcOffset(dateTime.DateTime)))
                return DateTime.SpecifyKind(dateTime.DateTime, DateTimeKind.Local);
            else
                return dateTime.DateTime;
        }
    }
}
