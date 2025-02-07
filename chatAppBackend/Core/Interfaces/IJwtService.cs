using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IJwtService
    {
        string GenerateAccessToken(Users user);
        string GenerateRefreshToken(Users user);
    }
}
