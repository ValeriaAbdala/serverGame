using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace OcaWCF
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    internal class ChatService : IChatService

    {
        Dictionary<IChatClient, string> _users= new Dictionary<IChatClient, string>(); //***
        void IChatService.Join(string nickname)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            _users[connection] = nickname;
        }

        void IChatService.SendMessage(string message)
        {
            var connection = OperationContext.Current.GetCallbackChannel<IChatClient>();
            String user;
            if (!_users.TryGetValue(connection, out user))
                return;
            foreach (var other in _users.Keys)
            { 
                if(other == connection)
                    continue;
                other.RecieveMessage(user, message);
            }
        }
    }
}
