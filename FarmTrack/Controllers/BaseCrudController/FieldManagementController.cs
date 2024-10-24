using FarmTrack.Models;
using FarmTrack.Models.Entities;


namespace FarmTrack.Controllers.BaseCrudController
{
    public class FieldManagementController : BaseCrudController<Field, FarmContext>
    {

        public FieldManagementController(FarmContext context) : base(context)
        {
        }
    }
}
