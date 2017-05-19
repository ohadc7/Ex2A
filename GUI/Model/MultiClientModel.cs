using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net;
using System.IO;
using CommunicationSettings;
using System.Threading;

namespace GUI.Model
{
    public delegate void ChangedEventHandler(object sender, EventArgs e);

    public delegate void ServerSentMessage(string message);

    public class MultiClientModel : INotifyPropertyChanged
    {
        public string MessageToSend { private get; set; }
        public bool commandIsReadyToBeSent { private get; set; }

        public event ServerSentMessage ReceivingMessageEvent;
        public event PropertyChangedEventHandler PropertyChanged;

        public MultiClientModel()
        {
            commandIsReadyToBeSent = false;
        }

        public void Communicate()
        {
            var command = MessageToSend;
            //flag that indicates that the user entered a comment that haven't been sent yet.
            //var commandIsReadyToBeSent = false;
            //in every iteration: connect to the server. send a massage to it, receive its answer and print it.
            //if the answer is Messages.PassToMultiPlayerMassage, then manage long communication with the server,
            //i.e. both send massages and get answers in parallel until the answer is Messages.PassToSinglePlayerMassage.
            while (true)
            {
                //read command from the user:
                if (!commandIsReadyToBeSent)
                {
                    //Console.Write("debug massage: Please enter a command: ");
                    //command = "";//Console.ReadLine();
                    while(!commandIsReadyToBeSent)
                    {
                        Thread.Sleep(50);
                    }
                    //commandIsReadyToBeSent = true;
                    command = MessageToSend;
                }

                //connect to the server:
                var ep = new IPEndPoint(
                    IPAddress.Parse(GUI.Properties.Settings.Default.ServerIP), Convert.ToInt32(GUI.Properties.Settings.Default.ServerPort));
                var client = new TcpClient();
                client.Connect(ep);
                //Console.WriteLine("debug massage: You are connected");

                using (var stream = client.GetStream())
                using (var reader = new BinaryReader(stream))
                using (var writer = new BinaryWriter(stream))
                {
                    // Send the command to server:
                    if (commandIsReadyToBeSent)
                    {
                        writer.Write(command);
                        commandIsReadyToBeSent = false;
                    }

                    // Get answer from server and print it:
                    var answerFromServer = reader.ReadString();
                    if (!(answerFromServer == Messages.PassToMultiPlayerMassage))
                    Console.WriteLine("{0}", answerFromServer);
                    //Manage long communication with the server:
                    else
                    {
                        //flag to stop the long communication
                        var stop = false;
                        var readUpdates = new Task(() =>
                        {
                            while (!stop)
                            {
                                var updateFromServer = reader.ReadString();
                                if (updateFromServer == Messages.PassToSinglePlayerMassage)
                                    stop = true;
                                else
                                {
                                    //Console.WriteLine(updateFromServer);
                                    //this.Message = updateFromServer;
                                    ReceivingMessageEvent?.Invoke(updateFromServer);
                                }
                            }
                        });
                        readUpdates.Start();

                        while (!stop)
                        {
                             if (!commandIsReadyToBeSent)
                            {
                                while (!commandIsReadyToBeSent)
                                {
                                    Thread.Sleep(50);
                                }
                                command = MessageToSend;
                                /*
                                //Console.Write("debug massage: You are in MultiPlayer mode. Please enter a command: ");
                                command = Console.ReadLine();
                                commandIsReadyToBeSent = true;
                                */
                            }

                            if (!stop)
                                if (commandIsReadyToBeSent)
                                {
                                    writer.Write(command);
                                    commandIsReadyToBeSent = false;
                                }
                        }
                        stop = true;
                    }
                }
                //Disconnect
                client.Close();
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        
        private string mazeName;
        public string MazeName
        {
            get { return mazeName; }
            set
            {
                mazeName = value;
                NotifyPropertyChanged("MazeName");
            }
        }
        private Position currentPosition;
        public Position CurrentPosition
        {
            get { return currentPosition; }
            set
            {
                currentPosition = value;
                NotifyPropertyChanged("CurrentPosition");
            }
        }
        private int rows;
        public int Rows
        {
            get { return rows; }
            set
            {
                rows = value;
                NotifyPropertyChanged("Rows");
            }
        }
        private int cols;
        public int Cols
        {
            get { return cols; }
            set
            {
                cols = value;
                NotifyPropertyChanged("Cols");
            }
        }
        private string maze;
        public string MazeString
        {
            get { return maze; }
            set
            {
                maze = value;
                NotifyPropertyChanged("MazeString");
            }
        }
        private Position initPosition;
        public Position InitPosition
        {
            get { return initPosition; }
            set
            {
                initPosition = value;
                NotifyPropertyChanged("InitPosition");
            }
        }
        private Position goalPosition;
        public Position GoalPosition
        {
            get { return goalPosition; }
            set
            {
                goalPosition = value;
                NotifyPropertyChanged("GoalPosition");
            }
        }
        private string solveString;
        public string SolveString
        {
            get { return solveString; }
            set
            {
                solveString = value;
                NotifyPropertyChanged("SolveString");
            }
        }
        











        /*
public int WidthOfBlock
{
    get
    {
        throw new NotImplementedException();
    }

    set
    {
        throw new NotImplementedException();
    }
}
public int widthOfBlock
{
    get
    {
        throw new NotImplementedException();
    }

    set
    {
        throw new NotImplementedException();
    }
}
public string SolveString
{
    get
    {
        throw new NotImplementedException();
    }

    set
    {
        throw new NotImplementedException();
    }
}


public string Communicate(TcpClient client, string messege)
{
throw new NotImplementedException();
}
*/
        /*
        public TcpClient Connect()
        {
            var ep = new IPEndPoint(
            IPAddress.Parse(GUI.Properties.Settings.Default.ServerIP), Convert.ToInt32(GUI.Properties.Settings.Default.ServerPort));
            var client = new TcpClient();
            client.Connect(ep);
            return client;

        }
        */
        /*
        public void GetMaze(TcpClient client, string messege)
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
        public void GetSolveString(TcpClient client, string mazeName)
        {
            throw new NotImplementedException();
        }
        */
    }
}
