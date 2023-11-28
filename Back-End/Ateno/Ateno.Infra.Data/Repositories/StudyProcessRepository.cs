using Ateno.Domain.Entities;
using Ateno.Domain.Interfaces;
using Ateno.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ateno.Infra.Data.Repositories
{
    public class StudyProcessRepository : IStudyProcessRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StudyProcessRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Create(StudyProcess studyProcess)
        {
            try
            {
                _applicationDbContext.StudyProcess.Add(studyProcess);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public StudyProcess GetByCard(int studyCardId, string userId)
        {
            try
            {
                return _applicationDbContext.StudyProcess.Where(x => x.StudyCardId == studyCardId && x.UserId == userId && (x.StudyDeck.UserId == userId || x.StudyDeck.Room.RoomUsers.Where(y => y.UserId == userId).Count() > 0)).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Update(StudyProcess studyProcess)
        {
            try
            {
                _applicationDbContext.StudyProcess.Update(studyProcess);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
