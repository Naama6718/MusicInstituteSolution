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
            CreateMap<Instrument, InstrumentDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Teacher, TeacherDTO>().ReverseMap();
            CreateMap<AvailableLesson, AvailableLessonDTO>().ReverseMap();
            CreateMap<BookedLesson, BookedLessonDTO>().ReverseMap();
            CreateMap<PassedLesson, PassedLessonDTO>().ReverseMap();
        }
    }
}
