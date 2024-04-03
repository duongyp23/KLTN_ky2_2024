using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KLTN.Common.Entity;
using KLTN.Common.Entity.DTO;

namespace KLTN.BussinesLayer
{
    public interface IUserBL : IBaseBL<User>
    {
        Task<object> Authenticate(LoginForm user);
        Task<User> GetBankAccountAdmin();
        Task<Guid> GetWaitOrder(Guid userId);
        Task<object> Register(User user);
    }
}
