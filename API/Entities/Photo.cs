using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public bool IsMain { get; set; }

        public string PublicId { get; set; }

        //making sure EF doesn't make AppUserId nullable

        // Including collection of Photo with an AppUser will generate a Loop:
        // AppUser has a collection of photos 
        // that has an AppUser that has a collection of photos...
        // Etc
        public AppUser AppUser { get; set; }

        public int AppUserId { get; set; }
    }
}