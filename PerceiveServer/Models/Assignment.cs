using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerceiveServer.Models
{
    public class Assignment
    {
        public long AssignmentID { get; set; }
        public long TrainingID { get; set; }
        public long TaskID { get; set; }

        public DateTime Date { get; set; }

        public virtual Training Training { get; set; }
        public virtual Task Task { get; set; }
    }
}