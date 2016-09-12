using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PerceiveServer.Helper
{
    public static class DTOHelper
    {
        static Dictionary<string, int> LandMarkNameDic = new Dictionary<string, int>()
        {
            { "Entrance544", 1 },
            { "Hallway", 2 },
            { "RightMetalGate", 3 },
            { "RightFareMachine", 4 },
            { "EndFareGate", 5 },
            { "EnterOutBound", 6 },
            { "RightWaitingLine", 7 },
            { "RightElevator", 8 },
            { "Entrance562", 9 },
            { "EnterInBound", 10 },
            { "LeftWaitingLine", 11 },
            { "LeftElevator", 12 },
            { "Entrance575_1", 13 },
            { "Entrance575_2", 14 },
            { "LeftMetalGate", 15 },
            { "LeftFareMachine", 16 },
            { "Entrance582_1", 17 },
            { "Entrance582_2", 18 },
            { "Entrance582_Lobby", 19 },
        };
        


        public static Models.DTO.Task convertToDTO(Models.Task task)
        {
            Models.DTO.Task dto = new Models.DTO.Task()
            {
                ID = task.ID,
                SourceID = LandMarkNameDic[task.SourceID],
                DestinationID = LandMarkNameDic[task.DestinationID],
                SelectLevel = (int)task.SelectLevel,
                Instruction = task.Instruction,
                SelectTransmitType = (int)task.SelectTransmitType,
                FaultCount = task.FaultCount
            };
            return dto;
        }
    }
}