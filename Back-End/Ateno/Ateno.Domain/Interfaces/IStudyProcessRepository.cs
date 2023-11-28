using Ateno.Domain.Entities;
using System.Threading.Tasks;

namespace Ateno.Domain.Interfaces
{
    public interface IStudyProcessRepository
    {
        Task<bool> Create(StudyProcess studyProcess);
        StudyProcess GetByCard(int studyCardId, string userId);
        Task<bool> Update(StudyProcess studyProcess);
    }
}
