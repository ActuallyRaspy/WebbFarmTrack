namespace FarmTrack.Models.Entities
{
    public class PlantedCrop
    {
        public int PlantedCropId { get; set; }
        public DateTime PlantDate { get; set; }
        public int Climate { get; set; } // 0 = cold, 1 = warm. This will change the displayed planting/harvesting period
        public int Harvested { get; set; } // 0 if not yet harvested, 1 if harvested.
        public int FieldId { get; set; }
        public Field Field { get; set; }

        public int CropId { get; set; }
        public Crop Crop { get; set; }

        public ICollection<Alert> Alerts { get; set; }

        public int Amount { get; set; }
        public DateTime? HarvestDate { get; set; }


    }
}
