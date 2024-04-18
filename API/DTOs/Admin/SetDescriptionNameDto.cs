using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Admin
{
    public class SetDescriptionNameDto
    {
        [Required]
        public string DescriptionName { get; set; }
    }
}