using FarmTrack.Models;
using FarmTrack.Models.Entities;

namespace FarmTrack.Controllers.BaseCrudController
{
    public class UserManagementController : BaseCrudController<User, FarmContext>
    {
        
        public UserManagementController(FarmContext context) : base(context) 
        {
        }
    }
}
