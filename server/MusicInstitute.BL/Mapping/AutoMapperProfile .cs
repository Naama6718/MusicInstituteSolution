using AutoMapper;
using MusicInstitute.BL.Models;
using MusicInstitute.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstitute.BL.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<DAL.Models.Instrument, InstrumentDTO>().ReverseMap();
            CreateMap<DAL.Models.Student, StudentDTO>().ReverseMap();
            CreateMap<DAL.Models.Teacher, TeacherDTO>().ReverseMap();
            CreateMap<DAL.Models.AvailableLesson, AvailableLessonDTO>().ReverseMap();
            CreateMap<BookedLesson, BookedLessonDTO>()
       .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.StudentIdLessonsNavigation.FirstName))
       .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.StudentIdLessonsNavigation.LastName))
       .ForMember(dest => dest.TeacherFirstName, opt => opt.MapFrom(src => src.TeacherIdLessonsNavigation.FirstName))
       .ForMember(dest => dest.TeacherLastName, opt => opt.MapFrom(src => src.TeacherIdLessonsNavigation.LastName))
       .ForMember(dest => dest.LessonDate, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.LessonDate.ToDateTime(TimeOnly.MinValue))))
       .ForMember(dest => dest.LessonTime, opt => opt.MapFrom(src => TimeOnly.FromTimeSpan(src.LessonTime.ToTimeSpan())))
       .ReverseMap();
            CreateMap<DAL.Models.PassedLesson, PassedLessonDTO>().ReverseMap();
        }
    }
}
