using System.ComponentModel.DataAnnotations;
using CVBanken.Data.Models.Site;

namespace CVBanken.Data.Models.Requests
{
    public class MessageRequest
    {
        [Required]
        [MinLength(5, ErrorMessage = "Message too short")]
        [MaxLength(125, ErrorMessage = "Message too long")]
        public string text { get; set; }

        public SiteMessage ToMessage()
        {
            return new SiteMessage
            {
                Text = text
            };
        }
    }
}