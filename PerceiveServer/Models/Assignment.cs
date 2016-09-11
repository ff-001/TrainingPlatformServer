using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PerceiveServer.Models
{
    public class Assignment
    {
        public long AssignmentID { get; set; }
        public long TrainingID { get; set; }
        public long TaskID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        public virtual Training Training { get; set; }
        public virtual Task Task { get; set; }
    }
}