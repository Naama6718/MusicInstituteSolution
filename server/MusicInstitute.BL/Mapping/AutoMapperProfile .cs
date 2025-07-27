// קובץ: MusicInstitute.BL/Mapping/AutoMapperProfile.cs

using AutoMapper;
using MusicInstitute.BL.Models;
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
            // מיפויים שלא משתנים
            // MusicInstitute.BL/Mapping/AutoMapperProfile.cs
            // החליפי את השורה של Instrument במיפוי הזה:
            CreateMap<DAL.Models.Instrument, InstrumentDTO>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.LessonName))
                .ReverseMap(); // ReverseMap עדיין שימושי למיפוי הפוך
                               // CreateMap<DAL.Models.Student, StudentDTO>().ReverseMap();
            CreateMap<DAL.Models.Student, StudentDTO>();
            CreateMap<StudentDTO, DAL.Models.Student>();
            CreateMap<DAL.Models.Teacher, TeacherDTO>().ReverseMap();
            CreateMap<DAL.Models.AvailableLesson, AvailableLessonDTO>().ReverseMap();
            CreateMap<TeacherUpdateDTO, DAL.Models.Teacher>();

            // === כאן התיקון ===
            // מיפוי מפורט עבור BookedLesson
            CreateMap<DAL.Models.BookedLesson, BookedLessonDTO>()
                .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.StudentIdLessonsNavigation.FirstName))
                .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.StudentIdLessonsNavigation.LastName))
                .ForMember(dest => dest.TeacherFirstName, opt => opt.MapFrom(src => src.TeacherIdLessonsNavigation.FirstName))
                .ForMember(dest => dest.TeacherLastName, opt => opt.MapFrom(src => src.TeacherIdLessonsNavigation.LastName));

            // מיפוי מפורט עבור PassedLesson
            CreateMap<DAL.Models.PassedLesson, PassedLessonDTO>()
                .ForMember(dest => dest.StudentFirstName, opt => opt.MapFrom(src => src.StudentIdLessonsNavigation.FirstName))
                .ForMember(dest => dest.StudentLastName, opt => opt.MapFrom(src => src.StudentIdLessonsNavigation.LastName))
                .ForMember(dest => dest.TeacherFirstName, opt => opt.MapFrom(src => src.TeacherIdLessonsNavigation.FirstName))
                .ForMember(dest => dest.TeacherLastName, opt => opt.MapFrom(src => src.TeacherIdLessonsNavigation.LastName));

            // אם אתם צריכים גם את המיפוי ההפוך (מ-DTO ל-DAL), תשאירו אותו
            CreateMap<BookedLessonDTO, DAL.Models.BookedLesson>();
            CreateMap<PassedLessonDTO, DAL.Models.PassedLesson>();
        }
    }
}