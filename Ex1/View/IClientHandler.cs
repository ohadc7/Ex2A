// ***********************************************************************
// Assembly         : Ex1
// Author           : Cohen
// Created          : 04-20-2017
//
// Last Modified By : Cohen
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="IClientHandler.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Net.Sockets;
 
namespace Ex1.Controller
{ 
    /// <summary>
    /// Interface IClientHandler
    /// </summary>
    public interface IClientHandler
    {
        /// <summary>
        /// Handles the client.
        /// </summary>
        /// <param name="client">The client.</param>
        void HandleClient(TcpClient client);
    }
}