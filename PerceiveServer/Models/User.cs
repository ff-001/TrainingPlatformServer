using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PerceiveServer.Models
{
    public enum Gender
    {
        Male,
        Female
    }
    public class User
    {
        [Key]
        public long ID { get; set; }

        public string FullName { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public string PassWord { get; set; }

        public string Username { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        public DateTime CreateDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Login Date")]
        public DateTime? LoginDate { get; set; }

        public bool IsOnline { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }
    }
}