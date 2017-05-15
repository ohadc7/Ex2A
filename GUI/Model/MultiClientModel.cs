using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Model
{
    class MultiClientModel : IClientModel
    {
        public TcpClient Connect()
        {
            throw new NotImplementedException();
        }

        public string recive(TcpClient client)
        {
            throw new NotImplementedException();
        }

        public string send(TcpClient client, string messege)
        {
            throw new NotImplementedException();
        }
    }
}
