using ALR.Domain.Entities.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
//using static ALR.Data.Base.EnumBase;    
namespace ALR.Domain.Entities
{
    [Table("UserInfo")]
    public class UserEntity
    {
        [Key]
        public Guid UserEntityID { get; set; }

        [Required]
        public string Account { get; set; }

        [Required]
        [JsonIgnore]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public int UserRole { get; set; }

        [Required]
        public int Active { get; set; }

        [Required]
        public Guid ProfileID { get; set; }
        [Required]
        public float AccountBalance { get; set; }



        public Guid? roomId { get; set; }

        [ForeignKey(nameof(roomId))]
        public virtual RoomEntity? Room {  get; set; }
        [Required]
        public virtual ProfileEntity? Profile { get; set; }

        public virtual ICollection<RequestEntity>? Request { get; set; }
        public virtual ICollection<MotelEntity> Motels { get; set; }

    }

    public enum UserRoleEnum
    {
        Admin = 0,
        Tenant = 2,
        Landlord = 1
    }
}
