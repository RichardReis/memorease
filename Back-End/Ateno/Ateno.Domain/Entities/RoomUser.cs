using Ateno.Domain.Validation;
using System;

namespace Ateno.Domain.Entities
{
    public class RoomUser : Entity
    {
        public int RoomId { get; private set; }
        public string UserId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public virtual Room Room { get; set; }
        public virtual User User { get; set; }

        public RoomUser(int id, int roomId, string userId, DateTime createdAt)
        {
            DomainExceptionValidation.When(id < 0, "O valor do Id é inválido.");
            DomainExceptionValidation.When(roomId < 1, "A Sala de Estudos é obrigatória.");
            DomainExceptionValidation.When(string.IsNullOrWhiteSpace(userId), "O Usuário é obrigatório.");
            Id = id;
            RoomId = roomId;
            UserId = userId;
            CreatedAt = createdAt;
        }
    }
}