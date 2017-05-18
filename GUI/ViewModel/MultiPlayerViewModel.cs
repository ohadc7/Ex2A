using GUI.Model;
using MazeLib;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace GUI.ViewModel
{
    class MultiPlayerViewModel : ViewModel
    {
        //TcpClient client;

        private MultiClientModel model;

        public MultiPlayerViewModel()
        {
            this.model = new MultiClientModel();
        }

        public MultiClientModel Model
        {
            get { return model; }
        }

        public void ConnectAndCommunicate(string firstCommand)
        {
            model.ReceivingMessageEvent += UpdateViewThatTheServerSentMessageToUs;
            var t = new Task(() =>
            {
                this.model.MessageToSend = firstCommand;
                this.model.commandIsReadyToBeSent = true;
                this.model.Communicate();
            });
            //manage all the communication with the server
            t.Start();
        }

        private void UpdateViewThatTheServerSentMessageToUs(string message)
        {

        }


        private int mazeRowsDefinition;
        public int MazeRowsDefinition
        {
            get { return mazeRowsDefinition; }
            set { mazeRowsDefinition = value; }
        }
        private int mazeColsDefinition;
        public int MazeColsDefinition
        {
            get { return mazeColsDefinition; }
            set { mazeColsDefinition = value; }
        }

        public string mazeNameDefinition;

        public string MazeNameDefinition
        {
            get { return mazeNameDefinition; }
            set { mazeNameDefinition = value; ; }
        }

    }
}
