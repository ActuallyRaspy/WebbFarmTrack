using FarmTrack.Models;
using FarmTrack.Models.Entities;


namespace FarmTrack.Controllers.BaseCrudController
{
    public class CropManagementController : BaseCrudController<Crop, FarmContext>
    {

        public CropManagementController(FarmContext context) : base(context)
        { 
        }
    }
}
