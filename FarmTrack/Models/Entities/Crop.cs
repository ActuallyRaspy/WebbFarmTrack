namespace FarmTrack.Models.Entities
{
    public class Crop
    {
        public int CropId { get; set; }
        public string CropName { get; set; }
        public string CropDescription { get; set; }
        public DateTime PlantingSeasonCold { get; set; }
        public DateTime PlantingSeasonWarm { get; set;}
        public DateTime HarvestSeasonCold { get; set; }
        public DateTime HarvestSeasonWarm { get; set; }
        public int DaysToGrow { get; set; }

    }
}
