namespace FarmTrack.Models.Entities
{
    public class Field
    {
        public int FieldId { get; set; } //pk
        public string FieldName { get; set; }
        public string Description { get; set; }
        public float Hectare { get; set; }
        public ICollection<PlantedCrop> PlantedCrops { get; set; } 
        public User User { get; set; }
        public int UserId { get; set; } //fk
    }
}
