using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto
{
    public class BookingScheduleDto
    {
        public DateTime createdDate { get; set; }
        public DateTime bookingDate { get; set; }
        public int bookingStatus { get; set; }
    }
}
