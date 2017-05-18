using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net;

namespace GUI.Model
{
    class MultiClientModel : IClientModel
    {
        public int Cols
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Position GoalPosition
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        public Position CurrentPosition
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int HeightOfBlock
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Position InitPosition
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Maze
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string MazeString
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        public string MazeName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Rows
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int WidthOfBlock
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int widthOfBlock
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string SolveString
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Communicate(TcpClient client, string messege)
        {
            throw new NotImplementedException();
        }

        public TcpClient Connect()
        {
            var ep = new IPEndPoint(
            IPAddress.Parse(GUI.Properties.Settings.Default.ServerIP), Convert.ToInt32(GUI.Properties.Settings.Default.ServerPort));
            var client = new TcpClient();
            client.Connect(ep);
            return client;

        }

        public void GetMaze(TcpClient client, string messege)
        {
            throw new NotImplementedException();
        }

        public string recive(TcpClient client)
        {
            throw new NotImplementedException();
        }

        public string send(TcpClient client, string messege)
        {
            throw new NotImplementedException();
        }

        public void GetSolveString(TcpClient client, string mazeName)
        {
            throw new NotImplementedException();
        }
    }
}
