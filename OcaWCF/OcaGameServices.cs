using OcaDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Collections.Concurrent;

namespace OcaWCF
{
      //  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]

    public partial class OcaGameServices : IAuthentication
        {
            public bool login(string userName, string password)
            {
                bool sucess = false;

                OcaBussinessLogic.Authentication authentication = new OcaBussinessLogic.Authentication();
                User userAcount = authentication.login(userName, password);
                if (userAcount.Nickname != null)
                {
                    sucess = true;
                }
                return sucess;
            }

            public Boolean SignUp(string nickname, string password)
            {
                OcaBussinessLogic.UsersAdministration users = new OcaBussinessLogic.UsersAdministration();
                Boolean sucess = false;
                sucess = users.SignUpUser(nickname, password);
                return sucess;
               // Console.WriteLine("*** Sign up User Starts ***");
            }
        }


       public partial class OcaGameServices : IChatService
    {
      
        int nextId = 1;
        ConcurrentDictionary<String, OperationContext> users2 = new ConcurrentDictionary<String, OperationContext>();


        public void Join(string nickname) //declaras un nuevo elemento del diccionario
        {
            /* var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
             _users[connection] = nickname; */
            this.users2.TryAdd(nickname,OperationContext.Current);
                     
            
           
        }

        public void SendMessage(string message, string identifier)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            foreach (var item in users2)
            {
                string answer = DateTime.Now.ToShortTimeString();

                //  var user = users.FirstOrDefault(i => i.IdUser == identifier);
                /*   if (user != null)
                   {
                       answer += ": " + user.Nickname + "";
                   }
                   answer += message; */
                //  item.operationContext.GetCallbackChannel<IChatClient>().RecieveMessage(answer);
                var user=users2.FirstOrDefault(i => i.Key== identifier);

                answer += ": " + user.Key + " "+message;
                item.Value.GetCallbackChannel<IChatClient>().RecieveMessage(answer);
            } 

        }

   
    }
   
 }


