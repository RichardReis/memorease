using Ateno.Application.DTOs;
using Ateno.Domain.Entities;
using AutoMapper;

namespace Ateno.Application.Mappings
{
    public class DomainAndDTOMapping : Profile
    {
        public DomainAndDTOMapping()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Room, RoomDTO>().ReverseMap();
            CreateMap<StudyDeck, StudyDeckDTO>().ReverseMap();
            CreateMap<StudyCard, StudyCardDTO>().ReverseMap();
            CreateMap<Room, RoomDTO>().ForMember(x => x.Users, opt => opt.Ignore());
            CreateMap<RoomDTO, Room>().ForSourceMember(x => x.Users, opt => opt.DoNotValidate());
            CreateMap<StudyDeck, StudyDeckDTO>().ForMember(x => x.studyCardDTOs, opt => opt.Ignore());
            CreateMap<StudyDeckDTO, StudyDeck>().ForSourceMember(x => x.studyCardDTOs, opt => opt.DoNotValidate());
            CreateMap<StudyCard, StudyCardDTO>().ForMember(x => x.StudyDeckName, opt => opt.Ignore());
            CreateMap<StudyCardDTO, StudyCard>().ForSourceMember(x => x.StudyDeckName, opt => opt.DoNotValidate());
        }
    }
}
