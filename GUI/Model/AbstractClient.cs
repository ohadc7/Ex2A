using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using Newtonsoft.Json.Linq;

namespace GUI.Model
{
    public abstract class AbstractClient : IClientModel
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private string mazeName;
        public string MazeName
        {
            get { return mazeName; }
            set
            {
                mazeName = value;
                NotifyPropertyChanged("MazeName");
            }
        }
        private Position currentPosition;
        public Position CurrentPosition
        {
            get { return currentPosition; }
            set
            {
                currentPosition = value;
                NotifyPropertyChanged("CurrentPosition");
            }
        }
        private int rows;
        public int Rows
        {
            get { return rows; }
            set
            {
                rows = value;
                NotifyPropertyChanged("Rows");
            }
        }
        private int cols;
        public int Cols
        {
            get { return cols; }
            set
            {
                cols = value;
                NotifyPropertyChanged("Cols");
            }
        }
        private string maze;
        public string MazeString
        {
            get { return maze; }
            set
            {
                maze = value;
                NotifyPropertyChanged("MazeString");
            }
        }
        private Position initPosition;
        public Position InitPosition
        {
            get { return initPosition; }
            set
            {
                initPosition = value;
                NotifyPropertyChanged("InitPosition");
            }
        }
        private Position goalPosition;
        public Position GoalPosition
        {
            get { return goalPosition; }
            set
            {
                goalPosition = value;
                NotifyPropertyChanged("GoalPosition");
            }
        }
        private string solveString;
        public string SolveString
        {
            get { return solveString; }
            set
            {
                solveString = value;
                NotifyPropertyChanged("SolveString");
            }
        }

        private bool finishGame;
        public bool FinishGame
        {
            get { return finishGame; }
            set
            {
                finishGame = value;
                NotifyPropertyChanged("FinishGame");
            }
        }

        private bool solveFinish;
        public bool SolveFinish
        {
            get { return solveFinish; }
            set
            {
                solveFinish = value;
                NotifyPropertyChanged("SolveFinish");
            }
        }
    }
}
