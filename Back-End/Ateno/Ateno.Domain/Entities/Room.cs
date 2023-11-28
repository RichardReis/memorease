using Ateno.Domain.Validation;
using System.Collections.Generic;

namespace Ateno.Domain.Entities
{
    public class Room : Entity
    {
        public string Name { get; private set; }
        public string Code { get; private set; }
        public string AdminId { get; private set; }
        public bool IsPublic { get; private set; }
        public virtual User Admin { get; set; }
        public virtual ICollection<RoomUser> RoomUsers { get; set; }
        public virtual ICollection<StudyDeck> StudyDecks { get; set; }

        public Room(int id, string name, string code, string adminId, bool isPublic)
        {
            DomainExceptionValidation.When(id < 0, "O valor do Id é inválido.");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "O Nome é obrigatório.");
            DomainExceptionValidation.When((string.IsNullOrWhiteSpace(adminId)), "O Id do Administrador é obrigatório.");
            Id = id;
            Name = name;
            Code = code;
            AdminId = adminId;
            IsPublic = isPublic;
        }

        public void GenerateCode()
        {
            if (Id > 0)
            {
                Code = string.Concat("#SL", Id.ToString().PadLeft(6, '0'));
            }
        }

        public void Update(string name, bool isPublic)
        {
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(name), "O Nome é obrigatório.");
            Name = name;
            IsPublic = isPublic;
        }
    }
}