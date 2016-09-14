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
            Clients.All.getTask(taskList);
        }

        public void Updatepause(string username, int trainingtype, string position, long taskID)
        {
            User user = db.Users.First(u => u.Username == username);
            foreach (var training in user.Trainings)
            {
                if (training.Type == trainingtype)
                {
                    training.PausePosition = position;
                    training.PauseTime = DateTime.UtcNow;
                    training.CurrentTaskId = taskID;
                }
            }
            db.SaveChanges();
        }

        public void Userlogin(string username, string password)
        {
            User user = db.Users.First(u => u.Username == username);
            if ((user != null) && (password == user.PassWord) && (user.ConnectionID == null))
            {
                // update database
                user.ConnectionID = Context.ConnectionId;
                user.LoginDate = DateTime.UtcNow;
                user.IsOnline = true;
                db.SaveChanges();

                Clients.Client(user.ConnectionID).login("Success", user.Username);
            }
            else
            {
                Clients.Client(Context.ConnectionId).login("Fail", "");
            }
        }

        public void Userlogout(string username)
        {
            User user = db.Users.First(u => u.Username == username);
                // update database
            user.ConnectionID = null;
            user.IsOnline = false;
            db.SaveChanges();

            Clients.Client(Context.ConnectionId).login("Logout", user.Username);
        }
    }
}