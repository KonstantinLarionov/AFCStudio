using System.ComponentModel.DataAnnotations;

namespace VDCompanyMVC.Models
{
    public class FeedbackRequest
    {
        [Required, StringLength(150)]
        public string Name { get; set; }

        [Required, EmailAddress, StringLength(254)]
        public string Email { get; set; }

        [Required, StringLength(200)]
        public string Subject { get; set; }

        [Required, StringLength(3000)]
        public string Message { get; set; }

        public bool ConsentAccepted { get; set; }
        public string ConsentVersion { get; set; }
    }
}
