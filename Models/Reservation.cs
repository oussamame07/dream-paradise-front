#pragma warning disable CS8618
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamParadise.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        // =========== CheckIn Date validation ===============
        [Required(ErrorMessage = "Please enter the CheckIn date")]
        [FutureDate]
        public DateTime CheckIn { get; set; }
        
        // =========== CheckOut Date validation ===============
        [Required(ErrorMessage = "Please enter the CheckOut date")]
        [FutureDate]
        [DateRange(nameof(CheckIn), ErrorMessage = "CheckOut date must be after CheckIn date")]
        public DateTime CheckOut { get; set; }

        // =========== Adults count validation ===============
        [Required]
        [Range(1, 5)]
        public int AdultsCount { get; set; }

        // =========== Child count validation ===============
        [Required]
        public int ChildCount { get; set; }

        // =========== Room type and price ===============
        [Required]
        public string RoomType { get; set; }

        [NotMapped]
        public int RoomPrice { get; set; }

        // =========== Total Price validation ===============
        [Required]
        public int TotalPrice { get; set; }

        // ======= Created & Updated validation ============
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // =========== Navigation ===============
        public User? UserWhoReserved { get; set; }

    } 
    // =========== Future date attribute creation ===============
    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            DateTime submittedDate = (DateTime)value;
            if (submittedDate < DateTime.Now)
            {
                return new ValidationResult("Please enter a date in the future!");
            }
            return ValidationResult.Success;
        }
    }

    // =========== Checkout date range attribute creation ===============
    public class DateRangeAttribute : ValidationAttribute
    {
        private readonly string _startDatePropertyName;

        public DateRangeAttribute(string startDatePropertyName)
        {
            _startDatePropertyName = startDatePropertyName;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var endDate = (DateTime?)value;
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDatePropertyName);

            if (startDateProperty == null)
            {
                return new ValidationResult($"Unknown property: {_startDatePropertyName}");
            }

            var startDate = (DateTime?)startDateProperty.GetValue(validationContext.ObjectInstance);

            if (endDate.HasValue && startDate.HasValue && endDate <= startDate)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
