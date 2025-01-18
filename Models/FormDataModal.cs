using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace dotNetProject.Models
{
    public class FormDataModal
    {
        [Column("user_id")]
        public int UserId {  get; set; }

        [Column("first_name")]
        [BindProperty]
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(25, ErrorMessage = "First Name cannot exceed 25 characters")]
        public string? FirstName { get; set; }
        [Column("middle_name")]
        [BindProperty]
        [StringLength(30, ErrorMessage ="Middle name cannot exceed 30 characters")]
        public string? MiddleName { get; set; }
        [Column("last_name")]
        [BindProperty]
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(25, ErrorMessage = "Last Name cannot exceed 25 characters")]
        public string? LastName { get; set;}
        [Column("email")]
        [BindProperty]
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress(ErrorMessage ="Plase enter a valid email address")]
        public string? Email { get; set; }
        [Column("phone")]
        [BindProperty]
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        public string? Phone { get; set; }
        [Column("date_of_birth", TypeName ="date")]
        [BindProperty]
        [Required(ErrorMessage = "Date of Birth is required")]
        public DateTime? DateOfBirth { get; set; }
        
        [Column("age")]
        [BindProperty]
        [Range(18,int.MaxValue, ErrorMessage = "Age must be 18 or older")]
        public int? Age { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Nationality is required")]
        [StringLength(30, ErrorMessage = "Nationality cannot exceed 30 characters")]
        [Column("nationality")]
        public string? Nationality {  get; set; }
        [BindProperty]
        [Column("occupation")]
        public string? Occupation { get; set; }

        [Column("address1")]
        [BindProperty]
        [StringLength(50, ErrorMessage = "Street address cannot exceed 30 characters")]
        public string? Address1 { get; set;}

        [Column("address2")]
        [BindProperty]
        [StringLength(50, ErrorMessage = "Address Line 2 cannot exceed 30 characters")]
        public string? Address2 { get; set; }
        [Column("city")]
        [BindProperty]
        [StringLength(30, ErrorMessage = "City cannot exceed 30 characters")]
        public string? City { get; set; }
        [Column("state")]
        [BindProperty]
        [StringLength(30, ErrorMessage = "State cannot exceed 30 characters")]
        public string? State { get; set; }
        [Column("country")]
        [BindProperty]
        [StringLength(30, ErrorMessage = "Country cannot exceed 30 characters")]
        public string? Country { get; set; }
        [Column("pincode")]
        public string? Pincode { get; set; }
        [Column("degree")]
        [BindProperty]
        [StringLength(50, ErrorMessage = "Degree cannot exceed 50 characters")]
        public string? Degree { get; set; }
        [Column("institution")]
        [BindProperty]
        [StringLength(70, ErrorMessage = "Institution cannot exceed 70 characters")]
        public string? Institution { get; set; }
        [Column("year_completed")]
        public int? YearCompleted { get; set; }

    }
}
