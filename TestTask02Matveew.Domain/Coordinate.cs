using System.ComponentModel.DataAnnotations;

namespace TestTask02Matveew.Domain
{
    public class Coordinate
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int CoordinateX { set; get; }

        [Required]
        public int CoordinateY { set; get; }

        [Required]
        public string Client { set; get; }
    }
}
