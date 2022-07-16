using System;
using System.Collections.Generic;
using System.Text;

namespace UnitOfTestProject.Services
{
    public interface IIdentityService 
    {
        bool IsValid(string IdentityNum);
    }
}
