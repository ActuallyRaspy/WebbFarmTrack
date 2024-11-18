using FarmTrack.Models;
using FarmTrack.Models.Entities;

namespace FarmTrack.Controllers.BaseCrudController
{
    public class AlertManagementController : BaseCrudController<Alert, FarmContext>
    {

        public AlertManagementController(FarmContext context) : base(context)
        {
        }
    }
}
