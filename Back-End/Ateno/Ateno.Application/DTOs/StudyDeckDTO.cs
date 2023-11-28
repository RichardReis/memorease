using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ateno.Application.DTOs
{
    public class StudyDeckDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome do Baralho é obrigatório")]
        [MaxLength(32)]
        [DisplayName("Nome do Baralho")]
        public string Name { get; set; }
        public string UserId { get; set; }
        public int StudyRoomId { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<StudyCardDTO> studyCardDTOs { get; set; }
    }

    public class StudyDeckListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InReview { get; set; }
        public int InLearning { get; set; }
        public int? StudyRoomId { get; set; }
        public string CreatedAt { get; set; }
    }
}