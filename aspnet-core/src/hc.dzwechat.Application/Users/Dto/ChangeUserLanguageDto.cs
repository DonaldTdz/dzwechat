using System.ComponentModel.DataAnnotations;

namespace hc.dzwechat.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}