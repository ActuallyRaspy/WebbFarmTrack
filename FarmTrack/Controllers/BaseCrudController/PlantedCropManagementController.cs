using FarmTrack.Models;
using FarmTrack.Models.Entities;

namespace FarmTrack.Controllers.BaseCrudController
{
    public class PlantedCropManagementController : BaseCrudController<PlantedCrop, FarmContext>
    {

        public PlantedCropManagementController(FarmContext context) : base(context) 
        {
        }
    }
}
