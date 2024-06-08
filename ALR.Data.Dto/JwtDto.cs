using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Data.Dto
{
    public class JwtDto
    {
        public string status { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Account { get; set; }
        public Guid UserID { get; set; }
    }
}
