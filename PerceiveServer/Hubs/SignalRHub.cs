using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using PerceiveServer.DAL;
using PerceiveServer.Models;

namespace PerceiveServer.Hubs
{
    public class SignalRHub : Hub
    {
        private PerceiveContext db = new PerceiveContext();
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }

        public void Taskrequest(long trainingID)
        {
            List<Models.DTO.Task> taskList = new List<Models.DTO.Task>();
            Training training = db.Trainings.First(t => t.ID == trainingID);
            foreach (var assignment in training.Assignment)
            {
                taskList.Add(Helper.DTOHelper.convertToDTO(assignment.Task));
            }
            //taskList.Add(new Models.DTO.Task { ID = 1, SourceID = "E", DestinationID = "D", Instruction = "h", FaultCount = 1, SelectLevel = (int)Level.Easy, SelectTransmitType = (int)TransmitType.Elevator});
            Clients.All.getTask(taskList);
        }
    }
}