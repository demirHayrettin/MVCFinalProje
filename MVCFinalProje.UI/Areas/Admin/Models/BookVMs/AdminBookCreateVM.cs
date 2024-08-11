using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVCFinalProje.UI.Areas.Admin.Models.BookVMs
{
    public class AdminBookCreateVM
    {
        public string Name { get; set; }
        public DateTime PublishDate { get; set; } = DateTime.Now;
        public Guid AuthorId { get; set; }
        public SelectList Publishers { get; set; }
        public SelectList Authors { get; set; }

        public Guid PublisherId { get; set; }
    }
}
