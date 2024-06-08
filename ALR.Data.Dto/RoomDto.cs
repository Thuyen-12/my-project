using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto
{
    public class RoomDto
    {
        public string roomNumber { get; set; }
        public string roomDescription { get; set; }
        public float roomPrice { get; set; }
        public int availableSlot { get; set; }
        public int roomStatus { get; set; }
    }
}
