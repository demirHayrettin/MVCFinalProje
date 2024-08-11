using System.ComponentModel.DataAnnotations;

namespace MVCFinalProje.UI.Areas.Admin.Models.AuthorVMs
{
    public class AdminAuthorCreateVM
    {
        [Required(ErrorMessage ="Bu alan boş bıakılamaz!!!!")]
        [MaxLength(30, ErrorMessage ="30 Karakterden fazla giremezsiniz")]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
