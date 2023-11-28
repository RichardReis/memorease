using System.Collections.Generic;

namespace Ateno.Application.DTOs
{
    public class RoomDeckInfoDTO
    {
        public RoomDeckInfoDTO()
        {
            RoomCardInfo = new List<RoomCardInfoDTO>();
        }

        public string Performance { get; set; }
        public int StudyUsers { get; set; }
        public List<RoomCardInfoDTO> RoomCardInfo { get; set; }
    }

    public class RoomCardInfoDTO
    {
        public string Front { get; set; }
        public string Performance { get; set; }
        public int StudyUsers { get; set; }
    }
}