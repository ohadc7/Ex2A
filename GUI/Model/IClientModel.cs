using MazeLib;
using Newtonsoft.Json.Linq;
using System.ComponentModel;
using System.Net.Sockets;

namespace GUI.Model
{
    public interface IClientModel : INotifyPropertyChanged
    {
        string MazeName
        {
            get;
            set
            ;
        }
        int Rows
        {
            get;
            set
           ;
        }
        
       int Cols
        {
            get;
            set
            ;
        }
      
        string MazeString
        {
            get;
            set
            ;
        }
        Position InitPosition
        {
            get;
            set
            ;
        }
        Position GoalPosition
        {
            get;
            set
           ;
        }
        Position CurrentPosition
        {
            get;
            set
           ;
        }
        string SolveString
        {
            get;
            set;
        }
        bool FinishGame
        {
            get;
            set;
        }
        bool SolveFinish
        {
            get;
            set;
        }
       
        void NotifyPropertyChanged(string propName);

    }

}
