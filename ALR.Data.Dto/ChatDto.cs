using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto
{
    public class CreateBoxChatDto
    {
        public string? Name { get; set; }
        public List<Guid> UserIds { get; set; }
    }

    public class CreateMessageDto
    {
        public Guid BoxChatId { get; set; }
        public string Message { get; set; }
        public Guid SenderId { get; set; }
    }
}
