using MazeLib;
using System.ComponentModel;
using System.Net.Sockets;

namespace GUI.Model
{
    public interface IClientModel : INotifyPropertyChanged
    {

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
        TcpClient Connect();
        string Communicate(TcpClient client, string messege);
        void GetMaze(TcpClient client, string mazeInput);
    }

}
