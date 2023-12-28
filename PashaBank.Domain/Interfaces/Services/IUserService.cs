using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PashaBank.Domain.Interfaces.Services
{
    public interface IUserService
    {
        bool IsRecommendationAllowed(Guid? recommendedById, int level);
        bool HasRecommendedMoreThanThree(Guid? recommendedById);
    }
}
