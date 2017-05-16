using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MazeLib;

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

        public event PropertyChangedEventHandler PropertyChanged;

        public string Communicate(TcpClient client, string messege)
        {
            throw new NotImplementedException();
        }

        public TcpClient Connect()
        {
            throw new NotImplementedException();
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
    }
}
