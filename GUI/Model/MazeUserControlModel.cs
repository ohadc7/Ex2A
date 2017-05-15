using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adapter;
using MazeGeneratorLib;
using MazeLib;
using GUI.ViewModel;

namespace GUI.Model
{
    class MazeUserControlModel
    {
        public int WidthOfBlock
        {
            get;
            set;
        }
        public int HeightOfBlock
        {
            get;
            set;
        }
        public int Rows
        {
            get;
            set;
        }
        public int Cols
        {
            get;
            set;
        }
        public string Maze
        {
            get;
            set;
        }
        public Position InitPosition
        {
            get;
            set;
        }
        public Position GoalPosition
        {
            get;
            set;
        }


    }
}
