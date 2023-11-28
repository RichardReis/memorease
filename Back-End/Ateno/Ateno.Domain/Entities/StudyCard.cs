using Ateno.Domain.Validation;
using System.Collections.Generic;

namespace Ateno.Domain.Entities
{
    public class StudyCard : Entity
    {
        public int StudyDeckId { get; private set; }
        public string Front { get; private set; }
        public string Back { get; private set; }
        public virtual StudyDeck StudyDeck { get; set; }
        public virtual ICollection<StudyProcess> StudyProcesses { get; set; }

        public StudyCard(int id, int studyDeckId, string front, string back)
        {
            DomainExceptionValidation.When(id < 0, "O valor do Id é inválido.");
            DomainExceptionValidation.When(studyDeckId <= 0, "O Id do Baralho de Estudo é inválido.");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(front), "A Frente do Cartão de Estudo é obrigatória.");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(back), "O Verso do Cartão de Estudo é obrigatório.");
            Id = id;
            StudyDeckId = studyDeckId;
            Front = front;
            Back = back;
        }
    }
}
