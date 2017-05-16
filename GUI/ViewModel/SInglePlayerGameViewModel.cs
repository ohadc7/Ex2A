using System;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using CommunicationSettings;
using MazeLib;
using GUI.Model;

namespace GUI.ViewModel
{
    class SinglePlayerGameViewModel : ViewModel
    {
        private IClientModel model;

        public SinglePlayerGameViewModel()
        {

            this.model = new ClientModel();
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public int VM_Rows
        {
            get { return model.Rows; }
            set
            {
                model.Rows = value;
                NotifyPropertyChanged("Rows");

            }
        }
        
        public int VM_Cols
        {
            get { return model.Cols; }
            set
            {
                model.Cols = value;
                NotifyPropertyChanged("Cols");
            }
        }
        public string VM_Maze
        {
            get { return model.MazeString; }
            set
            {
                model.MazeString = value;
                NotifyPropertyChanged("maze");
            }
        }
        public Position VM_InitPosition
        {
            get { return model.InitPosition; }
            set
            {
                model.InitPosition = value;
                NotifyPropertyChanged("InitPosition");
            }
        }
        public Position VM_GoalPosition
        {
            get { return model.GoalPosition; }
            set
            {
                model.GoalPosition = value;
                NotifyPropertyChanged("GoalPosition");
            }
        }
    }
}
