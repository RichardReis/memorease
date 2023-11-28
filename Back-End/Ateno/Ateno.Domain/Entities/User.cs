using Ateno.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ateno.Domain.Entities
{
    public class User
    {
        public string Id { get; private set; }
        public string FirstName { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public DateTime? DisabledAt { get; private set; }
        public virtual ICollection<StudyDeck> StudyDecks { get; set; }
        public virtual ICollection<StudyProcess> StudyProcesses { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual ICollection<RoomUser> RoomUsers { get; set; }

        public User(string id, string firstName, string name, string email)
        {
            DomainExceptionValidation.When((string.IsNullOrWhiteSpace(name) || name.Length < 4), "O Nome é obrigatório.");
            DomainExceptionValidation.When((string.IsNullOrWhiteSpace(email) || email.Length < 10), "Email inválido ou não inserido.");
            Id = id;
            if (string.IsNullOrEmpty(firstName))
                formatName(name);
            else
            {
                Name = name;
                FirstName = firstName;
            }
            Email = email;
        }

        public void Update(string name, string email)
        {
            DomainExceptionValidation.When((string.IsNullOrWhiteSpace(name) || name.Length < 4), "O Nome é obrigatório.");
            DomainExceptionValidation.When((string.IsNullOrWhiteSpace(email) || email.Length < 10), "Email inválido ou não inserido.");
            formatName(name);
            Email = email;
        }

        public void Disable()
        {
            DisabledAt = DateTime.UtcNow;
        }

        private void formatName(string name)
        {
            string[] nameSplit = name.Split(' ');
            Name = "";
            foreach (string item in nameSplit)
            {
                if (item.Length > 3)
                    Name = string.Concat(Name, char.ToUpper(item[0]), item.Substring(1).ToLower(), ' ');
                else
                    Name = string.Concat(Name, item.ToLower(), ' ');
            }
            Name = Name.Remove(Name.Length - 1);
            FirstName = Name.Split(' ').First();
        }
    }
}