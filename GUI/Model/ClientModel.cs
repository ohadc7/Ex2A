// ***********************************************************************
// Assembly         : ClientForDebug
// Author           : Ohad and Ido
// Created          : 04-06-2017
//
// Last Modified By : IDO1
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="Communication.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using CommunicationSettings;

namespace GUI.Model
{
    /// <summary>
    /// Class Communication. managing communication with the server (
    /// </summary>
    class ClientModel : IClientModel
    {



        /// <summary>
        /// Communicate with the server.
        /// read massage from the user. connect to the server. send the massage to it, receive its answer and print it.
        /// do it iteratively.
        /// (in 'Multi-Player-Mode' save the connection with the server. in 'Single-Player-Mode' disconnect.)
        /// </summary>
        public void Communicate()
        {
            var command = "";
            //flag that indicates that the user entered a comment that haven't been sent yet.
            var commandIsReadyToBeSent = false;
            //in every iteration: connect to the server. send a massage to it, receive its answer and print it.
            //if the answer is Messages.PassToMultiPlayerMassage, then manage long communication with the server,
            //i.e. both send massages and get answers in parallel until the answer is Messages.PassToSinglePlayerMassage.
            while (true)
            {
                //read command from the user:
                if (!commandIsReadyToBeSent)
                {
                    //Console.Write("debug massage: Please enter a command: ");
                    command = Console.ReadLine();
                    commandIsReadyToBeSent = true;
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
                                    Console.WriteLine(updateFromServer);
                            }
                        });
                        readUpdates.Start();

                        while (!stop)
                        {
                            if (!commandIsReadyToBeSent)
                            {
                                //Console.Write("debug massage: You are in MultiPlayer mode. Please enter a command: ");
                                command = Console.ReadLine();
                                commandIsReadyToBeSent = true;
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
    }
}