using AutoMapper;
using MusicInstitute.DAL.Services;
using MusicInstitute.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MusicInstitute.DAL.Api;
using MusicInstitute.BL.Services;
using MusicInstitute.BL.Models;

namespace MusicInstitute.BL.Api
{
    public class Instrument_Manager_BL : IInstrument_Manager_BL
    {
        private readonly IInstrument_Manager_DAL _instrumentManagerDAL;
        private readonly IMapper _mapper;

        public Instrument_Manager_BL(IInstrument_Manager_DAL instrumentManagerDAL, IMapper mapper)
        {
            _instrumentManagerDAL = instrumentManagerDAL;
            _mapper = mapper;
        }

        // פונקציה לקבלת כל הכלים כמערך DTO
        public async Task<List<InstrumentDTO>> GetAllInstruments()
        {
            try
            {
                var instruments = await _instrumentManagerDAL.GetAllInstruments();
                if (instruments == null || !instruments.Any())
                {
                    throw new InvalidOperationException("No instruments found.");
                }
                return _mapper.Map<List<InstrumentDTO>>(instruments);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // פונקציה להוספת כלי נגינה
        public async Task AddInstrumentAsync(InstrumentDTO instrumentDTO)
        {
            if (string.IsNullOrEmpty(instrumentDTO.LessonName))
            {
                throw new ArgumentException("Instrument name cannot be null or empty.");
            }

            try
            {
                // מיפוי מ-DTO ל-Entity
                var instrument = _mapper.Map<Instrument>(instrumentDTO);

                // לבדוק אם הכלי כבר קיים
                var allInstruments = await _instrumentManagerDAL.GetAllInstruments();
                if (allInstruments.Any(i => i.LessonName.Equals(instrument.LessonName, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new InvalidOperationException("An instrument with this name already exists.");
                }

                await _instrumentManagerDAL.AddInstrument(instrument);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // פונקציה למחיקת כלי נגינה
        public async Task DeleteInstrumentAsync(int instrumentId)
        {
            try
            {
                var instrumentToDelete = await _instrumentManagerDAL.GetAllInstruments();
                var instrument = instrumentToDelete.FirstOrDefault(i => i.InstrumentId == instrumentId);

                if (instrument == null)
                {
                    throw new Exception("Instrument not found.");
                }

                // אם הכלי משויך למורים, לא ניתן למחוק אותו
                if (instrument.Teachers.Any())
                {
                    throw new InvalidOperationException("Cannot delete instrument as it is assigned to teachers.");
                }

                await _instrumentManagerDAL.DeleteInstrument(instrumentId);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // פונקציה להוספת מורה לכלי נגינה
        public async Task AddTeacherToInstrumentAsync(int instrumentId, TeacherDTO teacherDTO)
        {
            try
            {
                // המורה חייב להיות מאומת
                if (string.IsNullOrEmpty(teacherDTO.FirstName) || string.IsNullOrEmpty(teacherDTO.LastName))
                {
                    throw new ArgumentException("Teacher must have a valid first and last name.");
                }

                // מיפוי מ-DTO ל-Entity
                var teacher = _mapper.Map<Teacher>(teacherDTO);

                await _instrumentManagerDAL.AddTeacherInstrument(instrumentId, teacher);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }
        public async Task DeleteTeacherFromInstrumentAsync(int instrumentId, int teacherId)
        {
            ;

            var allInstruments = await _instrumentManagerDAL.GetAllInstruments();
            var instrument = allInstruments.FirstOrDefault(i => i.InstrumentId == instrumentId);
            if (instrument == null)
            {
                throw new InvalidOperationException();
            }

        }
    }
}
    
