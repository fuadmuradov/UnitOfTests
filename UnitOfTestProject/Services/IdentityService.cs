using System;
using System.Collections.Generic;
using System.Text;

namespace UnitOfTestProject.Services
{
    public class IdentityService : IIdentityService
    {
        public bool IsValid(string IdentityNum)
        {
            return true;
        }
    }
}
