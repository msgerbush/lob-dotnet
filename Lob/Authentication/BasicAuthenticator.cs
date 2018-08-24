﻿using System;
using System.Globalization;
using System.Text;
using System.Collections.Generic;

namespace Lob.Internal
{
    class BasicAuthenticator : IAuthenticationHandler
    {
        public void Authenticate(IRequest request, Credentials credentials)
        {
            var header = string.Format(
                CultureInfo.InvariantCulture,
                "Basic {0}",
                Convert.ToBase64String(Encoding.UTF8.GetBytes(
                    string.Format(CultureInfo.InvariantCulture, "{0}:", credentials.ApiKey))));

            request.Headers["Authorization"] = header;
        }
    }
}