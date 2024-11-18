namespace FarmTrack.Models.Entities
{
    public class Crop
    {
        public int CropId { get; set; }
        public string CropName { get; set; }
        public string CropDescription { get; set; }
        public string PlantingSeasonCold { get; set; } // Ändrad till string
        public string PlantingSeasonWarm { get; set; } // Ändrad till string
        public string HarvestSeasonCold { get; set; } // Ändrad till string
        public string HarvestSeasonWarm { get; set; } // Ändrad till string
        public int DaysToGrow { get; set; }
    }
}

