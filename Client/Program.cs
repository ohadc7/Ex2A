// ***********************************************************************
// Assembly         : ClientForDebug
// Author           : Ohad and Ido
// Created          : 04-06-2017
//
// Last Modified By : IDO1
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="Program.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary> client of mazes game </summary>
// ***********************************************************************
using System;
using System.Threading;

namespace Client
{
    /// <summary>
    /// The program of the client 
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            Console.WriteLine("debug massage: I am the Client.");
            //Console.WriteLine("Waiting 0.5 second to the server");
            Thread.Sleep(500);

            var communication = new Communication();
            //manage all the communication with the server
            communication.Communicate();

            Thread.Sleep(1000);
        }
    }
}