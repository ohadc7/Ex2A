// ***********************************************************************
// Assembly         : Ex1
// Author           : Cohen
// Created          : 04-18-2017
//
// Last Modified By : Cohen
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="View.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Ex1.Controller;
 
namespace Ex1.View
{
    /// <summary>
    /// Class View.
    /// </summary>
    /// <seealso cref="Ex1.View.IView" />
    internal class View : IView
    {
        /// <summary>
        /// The listener
        /// </summary>
        private TcpListener listener;
        /// <summary>
        /// The maze controller
        /// </summary>
        private readonly IController MazeController;

        /// <summary>
        /// The port
        /// </summary>
        private readonly int port;


        /// <summary>
        /// Initializes a new instance of the <see cref="View"/> class.
        /// </summary>
        /// <param name="port">The port.</param>
        /// <param name="controller">The controller.</param>
        public View(int port, IController controller)
        {
            this.port = port;
            MazeController = controller;
        }

        //public void Start(IController controller)
        /// <summary>
        /// Starts this instance.
        /// </summary>
        public void Start()
        { 
            IClientHandler ch = new ClientHandler(MazeController);
            //definition of communication channels:
            var ep = new IPEndPoint(IPAddress.Parse(ConfigurationManager.AppSettings["ip"]), port);
            listener = new TcpListener(ep);
            listener.Start();
            Console.WriteLine("I'm the Server");
            var acceptingClients = new Task(() =>
            {
                var counterOfClients = 0;
                //accept clients:
                while (true)
                {
                    try
                    {
                        var client = listener.AcceptTcpClient();
                        ch.HandleClient(client);
                    }
                    catch (SocketException)
                    {
                        break;
                    }
                    counterOfClients++;
                }
            });
            acceptingClients.Start();
        }

        /// <summary>
        /// Stops this instance.
        /// </summary>
        public void Stop()
        {
            listener.Stop();
        }
    }
}