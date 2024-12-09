using System.ComponentModel.DataAnnotations.Schema;

namespace dotNetProject.Models
{
    public class FormDataModal
    {
        [Column("user_id")]
        public int UserId {  get; set; }
        [Column("first_name")]
        public string? FirstName { get; set; }
        [Column("middle_name")]
        public string? MiddleName { get; set; }
        [Column("last_name")]
        public string? LastName { get; set;}
        [Column("email")]
        public string? Email { get; set; }
        [Column("phone")]
        public string? Phone { get; set; }
        [Column("date_of_birth")]
        public DateOnly? DateOfBirth { get; set; }
        [Column("age")]
        public int? Age { get; set; }
        [Column("nationality")]
        public string? Nationality {  get; set; }
        [Column("occupation")]
        public string? Occupation { get; set; }
        [Column("address1")]
        public string? Address1 { get; set;}
        [Column("address2")]
        public string? Address2 { get; set; }
        [Column("city")]
        public string? City { get; set; }
        [Column("state")]
        public string? State { get; set; }
        [Column("country")]
        public string? Country { get; set; }
        [Column("pincode")]
        public string? Pincode { get; set; }
        [Column("degree")]
        public string? Degree { get; set; }
        [Column("institution")]
        public string? Institution { get; set; }
        [Column("year_completed")]
        public int? YearCompleted { get; set; }

    }
}
