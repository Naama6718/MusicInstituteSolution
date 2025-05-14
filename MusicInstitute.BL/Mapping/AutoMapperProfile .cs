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
            CreateMap<DAL.Models.Instrument, InstrumentDTO>().ReverseMap();
            CreateMap<DAL.Models.Student, StudentDTO>().ReverseMap();
            CreateMap<DAL.Models.Teacher, TeacherDTO>().ReverseMap();
            CreateMap<DAL.Models.AvailableLesson, AvailableLessonDTO>().ReverseMap();
            CreateMap<DAL.Models.BookedLesson, BookedLessonDTO>().ReverseMap();
            CreateMap<DAL.Models.PassedLesson, PassedLessonDTO>().ReverseMap();
        }
    }
}
