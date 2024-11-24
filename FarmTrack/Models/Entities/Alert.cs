using Microsoft.EntityFrameworkCore;

namespace FarmTrack.Models.Entities
{
    public class Alert
    {
        public int AlertId { get; set; }
        public string AlertName { get; set; }
        public string AlertDescription { get; set; }
        public DateTime AlertDate { get; set; }
        public int Triggered {  get; set; } // 0 if not triggered yet (date not passed), 1 if triggered.
        public int Dismissed {  get; set; } // 0 if not triggered and/or not dismissed in the dashboard, 1 if dismissed.
       
        public int? PlantedCropId { get; set; }
        public PlantedCrop PlantedCrop { get; set; }
    }
}
