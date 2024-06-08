﻿using ALR.Data.Base;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ALR.Services.Common.Extentions
{
    public static class HttpContextExtention
    {
        public static string GetUserId(this HttpContext httpContext)
        {
            var res = string.Empty;
            if (httpContext != null && httpContext.User != null)
            {
                var a = httpContext.User.Claims.Where(c => c.Type.Equals(BaseConstants.USER_CLAIM_ID)).FirstOrDefault()?.Value;
                if (a != null)
                    res = a;
            }
            return res;
        }

        public static string GetUserRole(this HttpContext httpContext)
        {
            var res = string.Empty;
            if (httpContext != null && httpContext.User != null)
            {
                var a = httpContext.User.Claims.Where(c => c.Type.Equals(BaseConstants.USER_CLAIM_ROLE)).FirstOrDefault()?.Value;
                if (a != null)
                    res = a;
            }
            return res;
        }

        public static string GetAudienceJwt(this HttpContext httpContext)
        {
            var res = string.Empty;
            if (httpContext != null && httpContext.User != null)
            {
                var a = httpContext.User.Claims.Where(c => c.Type.Equals(JwtRegisteredClaimNames.Aud)).FirstOrDefault()?.Value;
                if (a != null)
                    res = a;
            }
            return res;
        }

    }
}
