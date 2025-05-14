using Microsoft.EntityFrameworkCore;
using MusicInstitute.DAL.Api;
using MusicInstitute.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstitute.DAL.Services
{
   public class Instrument_Manager_DAL : IInstrument_Manager_DAL
    {
        private readonly DB_Manager _dbManager;
        public Instrument_Manager_DAL(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }
        public async Task<List<Instrument>> GetAllInstruments()
        {
            List<Instrument> instruments = await _dbManager.Instruments.ToListAsync();
            if (instruments == null)
            {
                throw new InvalidOperationException("Instruments collection is not initialized.");
            }
            return instruments;
        }
        public async Task AddInstrument(Instrument instrument)
        {
            await _dbManager.Instruments.AddAsync(instrument);
            await _dbManager.SaveChangesAsync();
        }
        public async Task DeleteInstrument(int instrumentId)
        {
            var existingInstrument = await _dbManager.Instruments.FirstOrDefaultAsync(i => i.InstrumentId == instrumentId);
            if (existingInstrument == null)
                throw new Exception("Instrument not found");

            _dbManager.Instruments.Remove(existingInstrument);
            await _dbManager.SaveChangesAsync();
        }
        public async Task AddTeacherInstrument(int instrumentId, Teacher teacher)
        {
            var instrument = await _dbManager.Instruments.FirstOrDefaultAsync(i => i.InstrumentId == instrumentId);
            if (instrument == null)
                throw new Exception("Instrument not found");
            instrument.Teachers.Add(teacher);
            await _dbManager.SaveChangesAsync();
        }

    }
}
