using MusicInstitute.BL.Mapping;

namespace MusicInstitute.BL.Services
{
    public interface IInstrument_Manager_BL
    {
        Task AddInstrumentAsync(InstrumentDTO instrumentDTO);
        Task AddTeacherToInstrumentAsync(int instrumentId, TeacherDTO teacherDTO);
        Task DeleteInstrumentAsync(int instrumentId);
        Task<List<InstrumentDTO>> GetAllInstruments();
    }
}