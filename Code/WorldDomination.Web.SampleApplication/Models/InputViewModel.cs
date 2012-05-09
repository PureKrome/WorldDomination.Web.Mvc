using System.ComponentModel.DataAnnotations;

namespace WorldDomination.Web.SampleApplication.Models
{
    public class InputViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}