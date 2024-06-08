using ALR.Data.Database.Abstract;
using ALR.Domain.Entities;
using ALR.Services.Authentication.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.Authentication.Implement
{
    public class SaveUserToken : ISaveUserToken
    {
        public const string BASE26 = "abcdefghijklmnopqrstuvwxyx";
        public long[] C = new long[49999900];
        IRepository<UserTokenEntity> _usersTokenRepository;
        public SaveUserToken(IRepository<UserTokenEntity> usersTokenRepository)
        {
            _usersTokenRepository = usersTokenRepository;
        }
        public async Task SaveTokenAsync(UserTokenEntity token)
        {
             _usersTokenRepository.InsertAsync(token);
            await _usersTokenRepository.CommitChangeAsync();
        }
        public long Encode(string str)
        {

            long result = 0;
            char[] chars = str.ToCharArray();
            int length = chars.Length;
            try
            {
                if (str == "")
                {
                    Console.WriteLine("Empty string");
                    return -1;
                }
                else
                {
                    for (int i = 0; i < length; i++)
                    {
                        result += (BASE26.IndexOf(chars[i])) * (long)(Math.Pow(26, length - 1 - i));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
        public string Decode(long number)
        {
            string result = "";
            try
            {
                if (number == 0)
                {
                    result = "a";
                }

                while (number > 0)
                {
                    long value = number % 26;
                    result = (char)('a' + value) + result;
                    number = number / 26;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }
    }
}
