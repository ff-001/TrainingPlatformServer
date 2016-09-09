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

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Create Date")]
        public DateTime CreatDate { get; set; }

        [Range(0,2)]
        public int Type { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Pause Date")]
        public DateTime? PauseTime { get; set; }

        public string PausePosition { get; set; }

        public long? CurrentTaskId { get; set; }

        [Range(-1, 100)]
        public int FaultCount { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Finish Date")]
        public DateTime? FinishDate { get; set; }

        public bool IsFinished { get; set; }

        public long? UserId { get; set; }

        public User User { get; set; }

        public virtual ICollection<Assignment> Assignment { get; set; }
    }
}