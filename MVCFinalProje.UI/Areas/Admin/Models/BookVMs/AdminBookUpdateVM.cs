using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MVCFinalProje.UI.Areas.Admin.Models.BookVMs
{
    public class AdminBookUpdateVM
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime PublishDate { get; set; }   
       
        public Guid PublisherId { get; set; }
        public SelectList? Publishers { get; set; } 
        public Guid AuthorId { get; set; }
        public SelectList? Authors { get; set; } 
    }
}
