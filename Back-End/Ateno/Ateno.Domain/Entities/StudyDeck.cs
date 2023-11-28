using Ateno.Domain.Validation;
using System;
using System.Collections.Generic;

namespace Ateno.Domain.Entities
{
    public class StudyDeck : Entity
    {
        public string Name { get; private set; }
        public string UserId { get; private set; }
        public int? RoomId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public virtual User User { get; set; }
        public virtual Room Room { get; set; }
        public virtual ICollection<StudyCard> StudyCards { get; set; }
        public virtual ICollection<StudyProcess> StudyProcesses { get; set; }

        public StudyDeck(int id, string name, string userId, int? roomId, DateTime createdAt)
        {
            DomainExceptionValidation.When(id < 0, "O valor do Id é inválido.");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "O Nome é obrigatório.");
            if (!(string.IsNullOrWhiteSpace(userId)) && roomId == null)
                UserId = userId;
            else if ((string.IsNullOrWhiteSpace(userId)) && roomId != null && roomId > 0)
                RoomId = roomId;
            else
                DomainExceptionValidation.When(true, "O Baralho de Estudo precisa de um vínculo.");
            Id = id;
            Name = name;
            CreatedAt = createdAt;
        }

        public void UpdateName(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "O Nome é obrigatório.");
            Name = name;
        }
    }
}
