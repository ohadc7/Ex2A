﻿// ***********************************************************************
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
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using CommunicationSettings;
using MazeLib;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GUI.Model
{
    /// <summary>
    /// Class Communication. managing communication with the server (
    /// </summary>
    public class SingleClientModel : AbstractClient
    {


        public SingleClientModel()
        {

        }

        /// <summary>
        /// Communicate with the server.
        /// read massage from the user. connect to the server. send the massage to it, receive its answer and print it.
        /// do it iteratively.
        /// (in 'Multi-Player-Mode' save the connection with the server. in 'Single-Player-Mode' disconnect.)
        /// </summary>
        public TcpClient Connect()
        {

            //connect to the server:
            var ep = new IPEndPoint(
                IPAddress.Parse(GUI.Properties.Settings.Default.ServerIP), Convert.ToInt32(GUI.Properties.Settings.Default.ServerPort));
            var client = new TcpClient();
            client.Connect(ep);
            return client;
        }
        public void GetMaze(TcpClient client, string mazeInput)
        {

            string serverResponedMaze = (this.Communicate(client, mazeInput));
            Maze maze = Maze.FromJSON(serverResponedMaze);
            var data = (JObject)JsonConvert.DeserializeObject(serverResponedMaze);
            MazeString = data["Maze"].Value<String>();
            MazeName = maze.Name;
            Cols = maze.Cols;
            Rows = maze.Rows;
            InitPosition = maze.InitialPos;
            GoalPosition = maze.GoalPos;
            CurrentPosition = maze.InitialPos;
            FinishGame = false;
            SolveFinish = false;
            client.Close();
        }

        public void GetSolveString(TcpClient client, string solveCommand)
        {
            string solve = (this.Communicate(client, solveCommand));
            var data = (JObject)JsonConvert.DeserializeObject(solve);
            SolveString = data["Solution"].Value<String>();
            client.Close();
        }
        public string Communicate(TcpClient client, string messege)
        {

            using (var stream = client.GetStream())
            using (var reader = new BinaryReader(stream))
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(messege);
                return reader.ReadString();

            }
        }


    }
}