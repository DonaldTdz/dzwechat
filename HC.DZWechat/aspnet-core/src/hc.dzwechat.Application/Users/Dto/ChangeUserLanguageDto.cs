using System.ComponentModel.DataAnnotations;

namespace HC.DZWechat.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
