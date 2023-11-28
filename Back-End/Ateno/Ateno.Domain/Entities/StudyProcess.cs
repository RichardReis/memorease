using Ateno.Domain.Validation;
using System;

namespace Ateno.Domain.Entities
{
    public class StudyProcess : Entity
    {
        public int StudyCardId { get; private set; }
        public int StudyDeckId { get; private set; }
        public string UserId { get; private set; }
        public int Repetitions { get; private set; }
        public bool Learning { get; set; }
        public float EFactor { get; private set; }
        public DateTime NextStudy { get; private set; }
        public virtual StudyCard StudyCard { get; set; }
        public virtual StudyDeck StudyDeck { get; set; }
        public virtual User User { get; set; }

        public StudyProcess(int id, int studyCardId, int studyDeckId, string userId, bool learning,
            int repetitions, float eFactor, DateTime nextStudy)
        {
            DomainExceptionValidation.When(id < 0, "O valor do Id é inválido.");
            DomainExceptionValidation.When(studyCardId <= 0, "O Id da Carta de Estudo é inválido.");
            DomainExceptionValidation.When(studyDeckId <= 0, "O Id do Baralho de Estudo é inválido.");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(userId), "O Id do Usuário é obrigatório.");
            DomainExceptionValidation.When(repetitions < 0, "A quantidade de Repetições é inválida.");
            DomainExceptionValidation.When((eFactor < 1 || eFactor > 5), "O Fator E está fora de faixa.");
            DomainExceptionValidation.When(nextStudy.Year < 2021, "A data do Próximo Estudo é inválida.");
            Id = id;
            StudyCardId = studyCardId;
            StudyDeckId = studyDeckId;
            UserId = userId;
            Learning = learning;
            Repetitions = repetitions;
            EFactor = eFactor;
            NextStudy = nextStudy;
        }

        public void ScheduleStudy(int days, float eFactor)
        {
            Learning = false;
            Repetitions++;
            EFactor = eFactor;
            NextStudy = DateTime.Now.AddDays(days).Date;
        }

        public void InLearning(float eFactor)
        {
            Learning = true;
            EFactor = eFactor;
            if (Repetitions == 0)
                Repetitions++;
            else
                EFactor = eFactor;
        }

        public void RestartStudy()
        {
            Repetitions = 0;
        }
    }
}