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
using System.Threading.Tasks;
using System.Threading;
using System.Collections.ObjectModel;

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

          private ImageBrush characterImageBrush = new ImageBrush(new BitmapImage(new Uri("user.jpg", UriKind.Relative)));

          private ImageBrush endDoorBrush = new ImageBrush(new BitmapImage(new Uri("End.jpg", UriKind.Relative)));


       
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



        public Position CurrentPosition
        {
            get { return (Position)GetValue(CurrentPositionProperty); }
            set { SetValue(CurrentPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentPositionProperty =
            DependencyProperty.Register("CurrentPosition", typeof(Position), typeof(MazeUserControl));

        private Position currentPosition;
        private double WidthOfBlock;
        private double HeightOfBlock;

        private Image StartImage;
        // private Image EndImage;
        private HashSet<Point> WallsSet = new HashSet<Point>();

        private Hashtable WallsCollection = new Hashtable();
        public void Draw()
        {
            WidthOfBlock =  MazeCanvas.Width / Cols;
            HeightOfBlock = MazeCanvas.Height / Rows;
            StartImage = new Image
            {
                Width = WidthOfBlock,
                Height = HeightOfBlock,
                Source = new BitmapImage(new Uri("user.jpg", UriKind.Relative))
            };
            Canvas.SetLeft(StartImage, InitPosition.Row* HeightOfBlock);
            Canvas.SetTop(StartImage, InitPosition.Col*WidthOfBlock);
          /*  EndImage = new Image
            {
                Width = WidthOfBlock,
                Height = HeightOfBlock,
                Source = new BitmapImage(new Uri("End.jpg", UriKind.Relative))
            };
            Canvas.SetLeft(EndImage, GoalPosition.Row * HeightOfBlock);
            Canvas.SetTop(EndImage, GoalPosition.Col * WidthOfBlock);
          */
            int x = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {

                    var rec = new Path
                    {
                        Data = new RectangleGeometry(new Rect(j *  HeightOfBlock, i * WidthOfBlock, WidthOfBlock, HeightOfBlock)),
                        Fill = Brushes.White,
                        Stroke = Brushes.Black,
                        StrokeThickness = 2
                    };
                    
                    if (MazeString[x].Equals('1'))
                    {
                        rec.Fill = Brushes.Black;
                        WallsCollection.Add(new Point(i, j),new Point(0,0));
                        WallsSet.Add(new Point(i, j));
                        
                    }
                    if(new Position(i, j).Equals(GoalPosition))
                    {
                        rec.Fill = endDoorBrush;
                    }
                   
                    MazeCanvas.Children.Add(rec);

                    x++;
                }
            }
            currentPosition = InitPosition;
            MazeCanvas.Children.Add(StartImage);
            //MazeCanvas.Children.Add(EndImage);
        }

        public void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                Point Left = new Point(currentPosition.Col - 1, currentPosition.Row);

                if (WallsSet.Contains(Left))
                {
                    currentPosition.Col -= 1;
                    Canvas.SetLeft(StartImage, currentPosition.Col * HeightOfBlock);
                    Canvas.SetTop(StartImage, currentPosition.Row * WidthOfBlock);
                }
            }
            if (e.Key == Key.Right)
            {
                Point Right = new Point(currentPosition.Col + 1, currentPosition.Row);
                if (WallsSet.Contains(Right))
                {
                    currentPosition.Col += 1;
                    Canvas.SetLeft(StartImage, currentPosition.Col * HeightOfBlock);
                    Canvas.SetTop(StartImage, currentPosition.Row * WidthOfBlock);
                }
            }
            if (e.Key == Key.Up)
            {
                Point Up = new Point(currentPosition.Col, currentPosition.Row - 1);
                if (WallsSet.Contains(Up))
                {
                    currentPosition.Row -= 1;
                    Canvas.SetLeft(StartImage, currentPosition.Col * HeightOfBlock);
                    Canvas.SetTop(StartImage, currentPosition.Row * WidthOfBlock);
                }
            }
            if (e.Key == Key.Down)
            {
                Point Down = new Point(currentPosition.Col, currentPosition.Row + 1);
                if (WallsSet.Contains(Down))
                {
                    currentPosition.Row += 1;
                    Canvas.SetLeft(StartImage, currentPosition.Col * HeightOfBlock);
                    Canvas.SetTop(StartImage, currentPosition.Row * WidthOfBlock);
                }
            }
        }

        public void animation(Position p)
        {
            Thread.Sleep(700);
            Dispatcher.Invoke(() =>
            {
                currentPosition = p;
                Canvas.SetLeft(StartImage, currentPosition.Col * HeightOfBlock);
                Canvas.SetTop(StartImage, currentPosition.Row * WidthOfBlock);
            });
            
        }

        

    }
}