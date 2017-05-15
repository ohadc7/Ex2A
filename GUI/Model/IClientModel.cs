using MazeLib;
using System.ComponentModel;
using System.Net.Sockets;

namespace GUI.Model
{
    interface IClientModel : INotifyPropertyChanged
    {
        int widthOfBlock { get; set; }
         int WidthOfBlock
        {
            get;
            set;
            
        }
        int HeightOfBlock
        { get;
            set;
           
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
      
        string Maze
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
        string send(TcpClient client, string messege);
        string recive(TcpClient client);
    }

}
