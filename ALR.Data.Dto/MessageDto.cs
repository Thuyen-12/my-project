using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto
{
    public class MessageViewDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public Guid BoxChatId { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
