﻿using Mongoose.IDO;
using Mongoose.IDO.Protocol;
using System;

namespace EmployeePictureUpload
{
    internal class Session
    {
        private bool logonSucceeded;
        public Client client;

        public Session()
        {
            logonSucceeded = false;
        }

        public void Login(string requestServiceUrl, string username, string password, string config)
        {
            client = new Client(requestServiceUrl, IDOProtocol.Http);
            OpenSessionResponseData response = default;
            response = this.client.OpenSession(username, password, config);
            logonSucceeded = response.LogonSucceeded;
            if (!logonSucceeded)
            {
                throw new Exception("Login Unsuccessful");
            }
        }
    }
}
