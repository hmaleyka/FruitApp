using ExamApp.Models.Base;

namespace ExamApp.Models
{
    public class Fruit : BaseEntity
    {
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string ImgUrl { get; set; }
    }
}
