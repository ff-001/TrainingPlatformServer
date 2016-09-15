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
                CreateTraining(user);
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

        public void Gettraining(string username, int trainingType)
        {

            User user = db.Users.First(u => u.Username == username);
            Training training = user.Trainings.First(t => t.Type == trainingType);

            Clients.Client(user.ConnectionID).getTraining(training.ID, training.PausePosition, training.CurrentTaskId);
        }

        public void Finish(long trainingID)
        {
            Training t = db.Trainings.Find(trainingID);
            t.IsFinished = true;
            t.FinishDate = DateTime.Now;
            db.SaveChanges();
        }

        public void Fault(long trainingID, long currentTask)
        {
            Training training = db.Trainings.Find(trainingID);
            Models.Task task = db.Tasks.Find(currentTask);
            training.FaultCount++;
            task.FaultCount++;
            db.SaveChanges();
        }

        private void CreateTraining(User user)
        {
            
            if (user.Trainings.Count == 0)
            {
                Training training1 = new Training()
                {
                    CreatDate = DateTime.Now,
                    Type = 1,
                    User = user,
                    UserId = user.ID,
                    FaultCount = 0,
                    PausePosition = "",
                    CurrentTaskId = 0
                };
                Training training2 = new Training()
                {
                    CreatDate = DateTime.Now,
                    Type = 2,
                    User = user,
                    UserId = user.ID,
                    FaultCount = 0,
                    PausePosition = "",
                    CurrentTaskId = 0
                };
                user.Trainings.Add(training1);
                AddTasks(training1);
                user.Trainings.Add(training2);
                db.SaveChanges();
            }
        }

        private void AddTasks(Training training)
        {
            List<Models.Task> tasks = db.Tasks.ToList();
            foreach (var t in tasks)
            {
                Assignment assignment = new Assignment()
                {
                    TaskID = t.ID,
                    TrainingID = training.ID,
                    Task = t,
                    Training = training,
                    Date = DateTime.Now
                };
                db.Assignments.Add(assignment);
            }
            db.SaveChanges();
        }
    }
}