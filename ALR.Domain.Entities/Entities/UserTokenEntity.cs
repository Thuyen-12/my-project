using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALR.Domain.Entities
{
    [Table("UserToken")]
    public class UserTokenEntity
    {
        [Key]
        [MaxLength(1000)]
        public string AccessToken { get; set; }
        public Guid UserID { get; set; }
        public DateTime ExpireDateAccessToken { get; set; }
        [MaxLength(1000)]
        public string RefreshToken { get; set; }
        public DateTime ExpireDateRefreshToken { get; set; }

        public DateTime CreationTime { get; set; }
        public Boolean IsActive { get; set; }
        [ForeignKey(nameof(UserID))]
        public virtual UserEntity? userEntity { get; set; }
    }
}
