using MusicInstitute.DAL.Models;

namespace MusicInstitute.DAL.Api
{
    public interface IInstrument_Manager_DAL
    {
        Task AddInstrument(Instrument instrument);
        Task AddTeacherInstrument(int instrumentId, Teacher teacher);
        Task DeleteInstrument(int instrumentId);
        Task<List<Instrument>> GetAllInstruments();
    }
}