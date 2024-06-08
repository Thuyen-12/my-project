using ALR.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.Authentication.Abstract
{
    public interface ISaveUserToken
    {
        string Decode(long number);
        long Encode(string str);
        Task SaveTokenAsync(UserTokenEntity token);
    }
}

