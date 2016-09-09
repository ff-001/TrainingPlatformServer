using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PerceiveServer.Models
{
        public enum Level
        {
            Easy,
            Normal,
            Hard
        }

        public enum TransmitType
        {
            Stair,
            Elevator,
            Escalator
        }

        public class Task
        {
            [Key]
            public long ID { get; set; }

            public string SourceID { get; set; }

            public string DestinationID { get; set; }

            [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please select a task level.")]
            [System.ComponentModel.DataAnnotations.Display(Name = "Level")]
            public Level SelectLevel { get; set; }

            public string Instruction { get; set; }

            [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "Please select a transmit type.")]
            [System.ComponentModel.DataAnnotations.Display(Name = "Transmit Type")]
            public TransmitType? SelectTransmitType { get; set; }

            public int FaultCount { get; set; }

        }
    }