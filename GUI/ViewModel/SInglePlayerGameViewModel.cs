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
        public IClientModel model;

        public SinglePlayerGameViewModel(IClientModel model)
        {

            this.model = model;
            model.PropertyChanged +=
            delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }

        public String VM_MazeName
        {
            get { return model.MazeName; }
            set
            {
                model.MazeName = value;

            }
        }

        public Position VM_CurrentPosition
        {
            get { return model.CurrentPosition; }
            set
            {
                model.CurrentPosition = value;

            }
        }

        public int VM_Rows
        {
            get { return model.Rows; }
            set
            {
                model.Rows = value;
            }
        }
        
        public int VM_Cols
        {
            get { return model.Cols; }
            set
            {
                model.Cols = value;
            }
        }
        public string VM_MazeString
        {
            get { return model.MazeString; }
            set
            {
                model.MazeString = value;
            }
        }
        public Position VM_InitPosition
        {
            get { return model.InitPosition; }
            set
            {
                model.InitPosition = value;
            }
        }
        public Position VM_GoalPosition
        {
            get { return model.GoalPosition; }
            set
            {
                model.GoalPosition = value;
            }
        }
        public string VM_SolveString
        {
            get { return model.SolveString; }
            set
            {
                model.SolveString = value;
            }
        }
    }
}
