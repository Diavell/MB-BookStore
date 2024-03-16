using System;
using System.Collections.Generic;
using System.Text;

namespace MB.Shared.Services
{
    public interface ISharedIdentityService
    {
        string GetUserId { get; }
    }
}
