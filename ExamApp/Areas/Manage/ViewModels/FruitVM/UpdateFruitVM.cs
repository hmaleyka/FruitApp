namespace ExamApp.Areas.Manage.ViewModels.FruitVM
{
    public class UpdateFruitVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public IFormFile Image { get; set; }
        public string? ImgUrl { get; set; }
    }
}
