using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebClient.Models
{
    public class Patient
    {
        public int Patient_ID { get; set; }

        [Required]
        public string Forname { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name should be in between 3 to 50", MinimumLength = 3)]
        public string Firstname { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name should be in between 2 to 50", MinimumLength = 2)]
        public string Surname { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = false)]
        public DateTime? DateofBirth { get; set; }

        [Required]
        public string Gender { get; set; }

        public string Home { get; set; }
        public string Work { get; set; }
        public string Mobile { get; set; }
    }
}