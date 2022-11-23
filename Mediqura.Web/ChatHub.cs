using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using SignalRChat.Common;
using Mediqura.Web.MedCommon;
using Mediqura.CommonData.LoginData;

namespace SignalRChat
{
    public class ChatHub : Hub
    {
        #region Data Members
        public class AlertMsg
        {
            public string UserName { get; set; }
            public string Message { get; set; }

        }
        static List<UserDetail> ConnectedUsers = new List<UserDetail>();
        static List<MessageDetail> CurrentMessage = new List<MessageDetail>();
        static List<AlertMsg> CurrentAlert = new List<AlertMsg>();
        #endregion
        #region Methods

        public void Connect(string userName, int isNotify, string role)
        {

            var id = Context.ConnectionId;


            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                ConnectedUsers.Add(new UserDetail { ConnectionId = id, UserName = userName });

                // send to caller
				
				if (isNotify == 1)
                {
                    Groups.Add(id, "ALERT");
                    Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage, CurrentAlert);
                }
			   
                else
                {
                    List<AlertMsg> empty = new List<AlertMsg>();
                    Groups.Add(id, role);
                    Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage, empty);
                }
                // send to all except caller client
                Clients.AllExcept(id).onNewUserConnected(id, userName);

            }

        }

		public void phrconnect(string userName, int isNotify, int phrrequestenable, int phrapproveenable, string role)
		{

			var id = Context.ConnectionId;


			if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
			{
				ConnectedUsers.Add(new UserDetail { ConnectionId = id, UserName = userName });

				// send to caller
				if (phrrequestenable == 1)
				{
					Groups.Add(id, "PHRReq");
					Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage, CurrentAlert);
				}
				if (phrapproveenable == 1)
				{
					Groups.Add(id, "PHRApv");
					Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage, CurrentAlert);
				}

				else
				{
					List<AlertMsg> empty = new List<AlertMsg>();
					Groups.Add(id, role);
					Clients.Caller.onConnected(id, userName, ConnectedUsers, CurrentMessage, empty);
				}
				// send to all except caller client
				Clients.AllExcept(id).onNewUserConnected(id, userName);

			}

		}

        public void SendMessageToAll(string userName, string message, string group, string no, string ID, string Bill)
        {
            // store last 100 messages in cache

            if (group != "ALL")
            {
                if (group == "UPDATE")
                {
                    Clients.All.UpdateMessage(userName, message);
                }
                else
                {
                    string[] Exceptional = new string[0];
                    Clients.Group(group, Exceptional).receiveMessage(userName, message, group, no, ID, Bill);

                }
            }
            else
            {
                AddMessageinCache(userName, message);
                // Broad cast message
                Clients.All.messageReceived(userName, message);
            }
        }
		public void sendMessageToPHR(string userName, string message, string group, string no, string ID, string Bill)
		{
			// store last 100 messages in cache

			
					string[] Exceptional = new string[0];
					Clients.Group(group, Exceptional).receiveMessageToPHR(userName, message, group, no, ID, Bill);

			
		}
        public void SendPrivateMessage(string toUserId, string message)
        {

            string fromUserId = Context.ConnectionId;

            var toUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == toUserId);
            var fromUser = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == fromUserId);

            if (toUser != null && fromUser != null)
            {
                // send to 
                Clients.Client(toUserId).sendPrivateMessage(fromUserId, fromUser.UserName, message);

                // send to caller user
                Clients.Caller.sendPrivateMessage(toUserId, fromUser.UserName, message);
            }

        }

        //public override System.Threading.Tasks.Task OnDisconnected()
        //{
        //    var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
        //    if (item != null)
        //    {
        //        ConnectedUsers.Remove(item);

        //        var id = Context.ConnectionId;
        //        Clients.All.onUserDisconnected(id, item.UserName);

        //    }

        //    return base.OnDisconnected();
        //}


        #endregion
        #region private Messages

        private void AddMessageinCache(string userName, string message)
        {
            CurrentMessage.Add(new MessageDetail { UserName = userName, Message = message });

            if (CurrentMessage.Count > 100)
                CurrentMessage.RemoveAt(0);
        }

        #endregion
    }

}