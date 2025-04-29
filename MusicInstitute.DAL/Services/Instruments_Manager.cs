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
    internal class Instruments_Manager : IInstruments_Manager
    {
        private readonly DB_Manager _dbManager;
        public Instruments_Manager(DB_Manager dbManager)
        {
            _dbManager = dbManager;
        }

        public async Task<List<Instrument>> GetInstruments()
        {

            List<Instrument> Instruments = await _dbManager.Instruments.ToListAsync();
            if (Instruments == null)
            {
                throw new InvalidOperationException("Instruments collection is not initialized.");
            }
            return Instruments;
        }

        public async Task AddInstrument(Instrument instrument)
        {
            await _dbManager.Instruments.AddAsync(instrument);
            await _dbManager.SaveChangesAsync();
        }

        public async Task DeleteInstrument(int instrumentID)
        {
            var existingInstrument = await _dbManager.Instruments.FirstOrDefaultAsync(i => i.InstrumentId == instrumentID);
            if (existingInstrument == null)
                throw new Exception("Instrument not found");

            _dbManager.Instruments.Remove(existingInstrument);
            await _dbManager.SaveChangesAsync();
        }
        public async Task AddTeacher(Teacher teacher, int instrumentID)
        {
            var existingInstrument = await _dbManager.Instruments.FirstOrDefaultAsync(i => i.InstrumentId == instrumentID);
            if (existingInstrument == null)
                throw new Exception("Instrument not found");
            existingInstrument.Teachers.Add(teacher);
            await _dbManager.SaveChangesAsync();
        }

    }
}
