using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PerceiveServer.Models
{
    public class Training
    {
        [Key]
        public long ID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        public DateTime CreatDate { get; set; }

        public int Type { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Pause Date")]
        public DateTime? PauseTime { get; set; }

        public string PausePosition { get; set; }

        public int FaultCount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Finish Date")]
        public DateTime? FinishDate { get; set; }

        public bool IsFinished { get; set; }

        public long? UserId { get; set; }

        public User User { get; set; }

        public virtual ICollection<Assignment> Assignment { get; set; }
    }
}