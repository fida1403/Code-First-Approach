using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Connectivity_Pro.Models
{
    public class Users
    {
            public string Firstname { get; set; } = null!;
            public string? Lastname { get; set; }
            [Key]
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
            public DateTime? DOB { get; set; }
            public gender? Gender { get; set; }
            [NotMapped]
            public string GenderAsString
            {
                get
                {
                return Gender switch
                {
                    Models.gender.Male => "Male",
                    Models.gender.Female => "Female",
                    Models.gender.Others => "Others"

                };
                }
            }
    }
            public enum gender
            {
                Male = 1,
                Female = 2,
                Others = 3
            }
 }

