using FarmTrack.Models.Entities;
namespace FarmTrack.Models
{
    public class TrackerViewModel
    {
        public IEnumerable<Crop> cropList { get; set; }
        public IEnumerable<Field> fieldList { get; set; }
        public IEnumerable<PlantedCrop> trackedCrops { get; set; }
    }
}
