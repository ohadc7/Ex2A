using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUI.Model;
using MazeGeneratorLib;
using Adapter;
using System.Windows.Controls;


namespace GUI.ViewModel
{
    class MazeUserControlViewModel : ViewModel
    {
        private MazeUserControlModel mazeUserControlModel;

        public MazeUserControlViewModel()
        {
            this.mazeUserControlModel = new MazeUserControlModel();
        }


        public int Rows2 {
           get { return 10; }
            set {
                mazeUserControlModel.Rows = value;
                NotifyPropertyChanged("Rows");
            }
        }

        public int Cols
        {
            get { return mazeUserControlModel.Cols; }
            set
            {
                mazeUserControlModel.Cols = value;
                NotifyPropertyChanged("Cols");
            }
        }
        public int HeightOfBlock
        {
            get { return mazeUserControlModel.HeightOfBlock; }
            set
            {
                mazeUserControlModel.HeightOfBlock = value;
                NotifyPropertyChanged("HeightOfBlock");
            }
        }

        public int WidthOfBlock
        {
            get { return mazeUserControlModel.WidthOfBlock; }
            set
            {
                mazeUserControlModel.WidthOfBlock = value;
                NotifyPropertyChanged("WidthOfBlock");
            }
        }
        public string Maze
        {
            get { return mazeUserControlModel.Maze; }
            set
            {
                mazeUserControlModel.Maze = value;
                NotifyPropertyChanged("Maze");
            }
        }

        public int bla
        {
            get
            {
                return 2;
            }
            set
            {
                bla = value;
            }
        }

        /* public void Draw()
         {
             var dfsMazeGenerator = new DFSMazeGenerator();
             var MyMaze = dfsMazeGenerator.Generate(15, 15);
             var SM = new SearchableMaze(MyMaze);
             PointState startPoint = new PointState(MyMaze.InitialPos);
             PointState endPoint = new PointState(MyMaze.GoalPos);





         }*/
    }
}
