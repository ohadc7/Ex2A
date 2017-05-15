using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GUI.ViewModel;
using MazeLib;

namespace GUI.Controls
{
    /// <summary>
    /// Interaction logic for MazeUserControl.xaml
    /// </summary>
    public partial class MazeUserControl : UserControl
    {
        private MazeUserControlViewModel mazeUserControlViewModel;

        public MazeUserControl()
        {
            InitializeComponent();
            mazeUserControlViewModel = new MazeUserControlViewModel();
            this.DataContext = mazeUserControlViewModel;
           // this.Draw();
        }



        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeUserControl), new PropertyMetadata(0));




        public int test
        {
            get { return (int)GetValue(testProperty); }
            set { SetValue(testProperty, value); }
        }

        // Using a DependencyProperty as the backing store for test.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty testProperty =
            DependencyProperty.Register("test", typeof(int), typeof(MazeUserControl), new PropertyMetadata(0));



 

        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("ColsProperty", typeof(int), typeof(MazeUserControl), new PropertyMetadata(0));



        public int Maze
        {
            get { return (int)GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("MazeProperty", typeof(int), typeof(MazeUserControl), new PropertyMetadata(0));



        public int HeightOfBlock
        {
            get { return (int)GetValue(HeightOfBlockProperty); }
            set { SetValue(HeightOfBlockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HeightOfBlock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeightOfBlockProperty =
            DependencyProperty.Register("HeightOfBlock", typeof(int), typeof(MazeUserControl), new PropertyMetadata(0));



        public int WidthOfBlock
        {
            get { return (int)GetValue(WidthOfBlockProperty); }
            set { SetValue(WidthOfBlockProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WidthOfBlock.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WidthOfBlockProperty =
            DependencyProperty.Register("WidthOfBlock", typeof(int), typeof(MazeUserControl), new PropertyMetadata(0));



        public int InitPosition
        {
            get { return (int)GetValue(InitPositionProperty); }
            set { SetValue(InitPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitPositionProperty =
            DependencyProperty.Register("InitPosition", typeof(int), typeof(MazeUserControl), new PropertyMetadata(0));




        public int GoalPosition
        {
            get { return (int)GetValue(GoalPositionProperty); }
            set { SetValue(GoalPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoalPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoalPositionProperty =
            DependencyProperty.Register("GoalPosition", typeof(int), typeof(MazeUserControl), new PropertyMetadata(0));


        public void Draw()
        {


            int x = test;
            int y = x;

            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {

                    var rec = new Path
                    {
                        Data = new RectangleGeometry(new Rect(j *  WidthOfBlock, i * HeightOfBlock, WidthOfBlock, HeightOfBlock)),
                        Fill = Brushes.Black,
                        Stroke = Brushes.Black,
                        StrokeThickness = 2
                    };

                    /*if (CellType.Free == MyMaze[i, j])
                    {
                        rec.Fill = Brushes.White;


                    }*/
                    if (new Position(i, j).Equals(InitPosition))
                    {
                        rec.Fill = Brushes.Blue;
                    }
                    if (new Position(i, j).Equals(GoalPosition))
                    {
                        rec.Fill = Brushes.Red;
                    }
                    MyCanvas.Children.Add(rec);


                }
            }
        }
    }
}