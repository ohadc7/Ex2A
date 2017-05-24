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
    //public delegate void ChangedEventHandler(object sender, EventArgs e);

    public delegate void ServerSentMessage(string message);
    public delegate void SomethingHappend();

    public class MultiClientModel : AbstractClient
    {
        public string MessageToSend { private get; set; }
        public bool commandIsReadyToBeSent { private get; set; }

        public event ServerSentMessage ReceivingMessageEvent;
        public event SomethingHappend GameBecameClosedEvent;
        public event SomethingHappend NoCommunicationWithServerEvent;

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

                try
                {

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
                                try
                                {
                                    while (!stop)
                                    {
                                        var updateFromServer = reader.ReadString();
                                        if (updateFromServer == Messages.PassToSinglePlayerMassage)
                                        {
                                            GameBecameClosedEvent?.Invoke();
                                            stop = true;
                                        }
                                        else
                                        {
                                            //Console.WriteLine(updateFromServer);
                                            //this.Message = updateFromServer;
                                            ReceivingMessageEvent?.Invoke(updateFromServer);
                                        }
                                    }
                                }
                                catch (Exception e)
                                {
                                    NoCommunicationWithServerEvent?.Invoke();
                                }
                            });
                            readUpdates.Start();

                            while (!stop)
                            {
                                if (!commandIsReadyToBeSent)
                                {
                                    while (!commandIsReadyToBeSent)
                                    {
                                        Thread.Sleep(20);
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
                catch (Exception e)
                {
                    NoCommunicationWithServerEvent?.Invoke();
                }
            }
        }
        private Position opponentPosition;
        public Position OpponentPosition
        {
            get { return opponentPosition; }
            set
            {
                opponentPosition = value;
                NotifyPropertyChanged("OpponentPosition");
            }
        }
      
    }
}
