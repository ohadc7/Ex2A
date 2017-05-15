// ***********************************************************************
// Assembly         : Ex1
// Author           : Cohen
// Created          : 04-20-2017
//
// Last Modified By : Cohen
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="ClientHandler.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Ex1.Controller
{
    /// <summary>
    /// Class ClientHandler.
    /// </summary>
    /// <seealso cref="Ex1.Controller.IClientHandler" />
    internal class ClientHandler : IClientHandler
    {
        /// <summary>
        /// The controller
        /// </summary>
        private readonly IController Controller;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientHandler"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        public ClientHandler(IController controller)
        {
            Controller = controller;
        } 
         
        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        public void HandleClient(TcpClient client)
        {
            var t = new Task(() =>
            {
                var stream = client.GetStream();
                var reader = new BinaryReader(stream);
                var writer = new BinaryWriter(stream);
                {
                    var commandLine = reader.ReadString();
                    Controller.ExecuteCommand(commandLine, client);
                }
            });
            t.Start();
        }
    }
}