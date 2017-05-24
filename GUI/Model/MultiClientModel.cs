// ***********************************************************************
// Assembly         : GUI
// Author           : ohad
// Created          : 05-15-2017
//
// Last Modified By : ohad
// Last Modified On : 05-24-2017
// ***********************************************************************
// <copyright file="MultiClientModel.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using MazeLib;
using System.Net;
using System.IO;
using CommunicationSettings;
using System.Threading;

namespace GUI.Model
{
    /// <summary>
    /// Delegate ServerSentMessage
    /// </summary>
    /// <param name="message">The message.</param>
    public delegate void ServerSentMessage(string message);
    /// <summary>
    /// Delegate SomethingHappend
    /// </summary>
    public delegate void SomethingHappend();

    /// <summary>
    /// Class MultiClientModel.
    /// </summary>
    /// <seealso cref="GUI.Model.AbstractClient" />
    public class MultiClientModel : AbstractClient
    {
        /// <summary>
        /// Gets or sets the message to send.
        /// </summary>
        /// <value>The message to send.</value>
        public string MessageToSend { private get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [command is ready to be sent].
        /// </summary>
        /// <value><c>true</c> if [command is ready to be sent]; otherwise, <c>false</c>.</value>
        public bool commandIsReadyToBeSent { private get; set; }

        public event ServerSentMessage ReceivingMessageEvent;
        public event SomethingHappend GameBecameClosedEvent;
        public event SomethingHappend CommunicationProblemEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiClientModel"/> class.
        /// </summary>
        public MultiClientModel()
        {
            commandIsReadyToBeSent = false;
        }

        /// <summary>
        /// Communicates this instance.
        /// </summary>
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
                    while (!commandIsReadyToBeSent)
                    {
                        Thread.Sleep(50);
                    }
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
                                            ReceivingMessageEvent?.Invoke(updateFromServer);
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                    CommunicationProblemEvent?.Invoke();
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
                catch (Exception)
                {
                    CommunicationProblemEvent?.Invoke();
                }
            }
        }

        /// <summary>
        /// The opponent position
        /// </summary>
        private Position opponentPosition;
        /// <summary>
        /// Gets or sets the opponent position.
        /// </summary>
        /// <value>The opponent position.</value>
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
