using MazeLib;
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
        TcpClient Connect();
        string Communicate(TcpClient client, string messege);
        void GetMaze(TcpClient client, string mazeInput);
        void GetSolveString(TcpClient client, string mazeName);
    }

}
