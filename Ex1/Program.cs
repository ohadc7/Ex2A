// ***********************************************************************
// Assembly         : Ex1
// Author           : Cohen
// Created          : 04-04-2017
//
// Last Modified By : Cohen
// Last Modified On : 04-21-2017
// ***********************************************************************
// <copyright file="Program.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using Ex1.Controller;
using Ex1.Model;
using Ex1.View;
using System.Configuration;

 
namespace Ex1
{
    /// <summary>
    /// Class Program.
    /// </summary>
    internal class Program
    { 
        //The main:
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            //create the Controller
            IController controller = new Controller.Controller();
            //create the model
            IModel model = new Model.Model();
            //create the view - gets controller as parameter
            IView view = new View.View(Convert.ToInt32(ConfigurationManager.AppSettings["port"]), controller);
            //give the controller instance of the model
            controller.SetModel(model);
            //start the server in the view
            view.Start();
            Console.ReadLine();
            view.Stop();
        }
    }
}