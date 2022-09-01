using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace BabyStore.Models
{

    public class ApplicationUser : IdentityUser
    {
        // setting error messages if the user inputs do not meet the required standard
        [Required]
        [Display(Name = "First Name")]
        //the lengh of the First name cannot be longer than 50 characters 
        [StringLength(50)]
        // setting error messages if the user inputs do not meet the required standard
        public string FirstName { get; set; }
        [Required]
        //the lengh of the Last name cannot be longer than 50 characters
        [Display(Name = "Last Name")]
        [StringLength(50)]
        public string LastName { get; set; }
        // setting error messages if the user inputs do not meet the required standard
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of birth")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DateOfBirth { get; set; }

        public Address Address { get; set; }
        public async Task<ClaimsIdentity> 
            GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            
            var userIdentity = await manager
                .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
           
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection",
                  throwIfV1Schema: false)
        {
        }
        

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>
                (new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}