using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GUI.ViewModel;
using MazeLib;
using System.Linq;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Input;
using System.Collections;
using System.Collections.Generic;

namespace GUI.Controls
{
    /// <summary>
    /// Interaction logic for MazeUserControl.xaml
    /// </summary>
    public partial class MazeUserControl : UserControl
    {

        public MazeUserControl()
        {
            InitializeComponent();
          
        }

        private ImageBrush characterImageBrush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/user.jpg")));

        private ImageBrush endDoorBrush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Images/end.jpg")));


        private Dictionary<Point, Path> recPlaceDict = new Dictionary<Point, Path>();

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeUserControl), new PropertyMetadata(0));

        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeUserControl), new PropertyMetadata(0));


       
        public string MazeName
        {
            get { return (string)GetValue(MazeNameProperty); }
            set { SetValue(MazeNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeNameProperty =
            DependencyProperty.Register("MazeName", typeof(string), typeof(MazeUserControl));




        public string MazeString
        {
            get { return (string)GetValue(MazeStringProperty); }
            set { SetValue(MazeStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeStringProperty =
            DependencyProperty.Register("MazeString", typeof(string), typeof(MazeUserControl));

        
        public Position InitPosition
        {
            get { return (Position)GetValue(InitPositionProperty); }
            set { SetValue(InitPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitPositionProperty =
            DependencyProperty.Register("InitPosition", typeof(Position), typeof(MazeUserControl));




        public Position GoalPosition
        {
            get { return (Position)GetValue(GoalPositionProperty); }
            set { SetValue(GoalPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoalPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoalPositionProperty =
            DependencyProperty.Register("GoalPosition", typeof(Position), typeof(MazeUserControl));




        public string SolveString
        {
            get { return (string)GetValue(SolveStringProperty); }
            set { SetValue(SolveStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SolveString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SolveStringProperty =
            DependencyProperty.Register("SolveString", typeof(string), typeof(MazeUserControl));


        private int initX, initY;
        private Path initRec;
        int widthOfBlock = 30;//(int)MazeCanvas.ActualWidth/Rows;
        int HeightOfBlock = 30;//(int)MazeCanvas.ActualHeight/Cols;
        private Rect moveRect;

        public void Draw()
        {

            

            int x = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {

                    var rec = new Path
                    {
                        Data = new RectangleGeometry(new Rect(j *  widthOfBlock, i * HeightOfBlock, widthOfBlock, HeightOfBlock)),
                        Fill = Brushes.Black,
                        Stroke = Brushes.Black,
                        StrokeThickness = 2
                    };
                    
                    if (MazeString[x].Equals('0'))
                    {
                        rec.Fill = Brushes.White;
                        
                    }
                    if (new Position(i, j).Equals(InitPosition))
                    {
                        rec.Fill = characterImageBrush;
                        initX = i;
                        initY = j;
                        initRec = rec;
                        moveRect = new Rect(initX, initY, widthOfBlock, HeightOfBlock);

                    }
                    if (new Position(i, j).Equals(GoalPosition))
                    {
                        rec.Fill = endDoorBrush;
                    }
                    recPlaceDict.Add(new Point(i, j), rec);
                    MazeCanvas.Children.Add(rec);

                    x++;
                }
            }
        }
        public void Solve()
        {
            Rect animationRect = new Rect(initX, initY, widthOfBlock, HeightOfBlock);
            foreach (char c in SolveString)
            {
                switch (c)
                {
                    case '0':
                        {
                            animationRect.X -= 1;
                            //initRec.Data = new RectangleGeometry(animationRect);
                            recPlaceDict[new Point(initX - 1, initY)].Fill = characterImageBrush;
                            System.Threading.Thread.Sleep(100);
                            break;
                        }
                    case '1':
                        {
                            animationRect.X += 1;
                            //initRec.Data = new RectangleGeometry(animationRect);
                            recPlaceDict[new Point(initX + 1, initY)].Fill = characterImageBrush;
                            System.Threading.Thread.Sleep(100);
                            break;
                        }
                    case '2':
                        {
                            animationRect.Y -= 1;
                            //initRec.Data = new RectangleGeometry(animationRect);
                            recPlaceDict[new Point(initX , initY-1)].Fill = characterImageBrush;
                            System.Threading.Thread.Sleep(100);
                            break;
                        }
                    case '3':
                        {
                            animationRect.Y += 1;
                            //initRec.Data = new RectangleGeometry(animationRect);
                            recPlaceDict[new Point(initX , initY+1)].Fill = characterImageBrush;
                            System.Threading.Thread.Sleep(100);
                            break;
                        }
                    default:
                        break;
                }
            }
        }

        public void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveRect.X -= 1;
                initRec.Data = new RectangleGeometry(moveRect);
            }
            if (e.Key == Key.Right)
            {
                moveRect.X += 1;
                initRec.Data = new RectangleGeometry(moveRect);
            }
            if (e.Key == Key.Up)
            {
                moveRect.Y -= 1;
                initRec.Data = new RectangleGeometry(moveRect);
            }
            if (e.Key == Key.Down)
            {
                moveRect.Y += 1;
                initRec.Data = new RectangleGeometry(moveRect);
            }
        }

    }
}