using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using CommunicationSettings;
using MazeLib;
using GUI.Model;

namespace GUI.ViewModel
{
    class SinglePlayerViewModel : ViewModel
    {
        private IClientModel model;

        private TcpClient client;

        public SinglePlayerViewModel()
        {

            this.model = new ClientModel();
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };

            client  = this.model.Connect();
            //manage all the communication with the server
           string x =  this.model.send(client, "generate maze 5 5");
            // string x = this.model.recive(client);
            string y = x;
        }

        public int Rows2
        {
            get { return 10; }
            set
            {
                model.Rows = value;
            }
        }
    }
}
