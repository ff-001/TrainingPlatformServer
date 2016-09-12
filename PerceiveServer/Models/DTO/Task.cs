using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerceiveServer.Models.DTO
{
    public class Task
    {
        public long ID { get; set; }

        public int SourceID { get; set; }

        public int DestinationID { get; set; }

        public int SelectLevel { get; set; }

        public string Instruction { get; set; }

        public int SelectTransmitType { get; set; }

        public int FaultCount { get; set; }

        //public virtual ICollection<Assignment> Assignment { get; set; }
    }
}