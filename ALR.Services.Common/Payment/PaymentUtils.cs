﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.Common.Payment
{
    public class PaymentUtils
    {
        public static String HmacSHA512(string key, String inputData)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }

        //public static string GetIpAddress()
        //{
        //    string ipAddress;
        //    try
        //    {
        //        ipAddress = HttpContext.Request.

        //        if (string.IsNullOrEmpty(ipAddress) || (ipAddress.ToLower() == "unknown"))
        //            ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    catch (Exception ex)
        //    {
        //        ipAddress = "Invalid IP:" + ex.Message;
        //    }

        //    return ipAddress;
        //}
    }
}
