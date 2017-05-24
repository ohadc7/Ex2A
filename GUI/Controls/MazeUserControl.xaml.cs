// ***********************************************************************
// Assembly         : GUI
// Author           : ohad
// Created          : 05-15-2017
//
// Last Modified By : ohad
// Last Modified On : 05-24-2017
// ***********************************************************************
// <copyright file="MazeUserControl.xaml.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace GUI.Controls
{
    /// <summary>
    /// Interaction logic for MazeUserControl.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MazeUserControl : UserControl
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="MazeUserControl"/> class.
        /// </summary>
        public MazeUserControl()
        {
            InitializeComponent();
          
        }

        /// <summary>
        /// The end door brush
        /// </summary>
        private ImageBrush endDoorBrush = new ImageBrush(new BitmapImage(new Uri("End.jpg", UriKind.Relative)));



        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>The rows.</value>
        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The rows property
        /// </summary>
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeUserControl), new PropertyMetadata(0));

        /// <summary>
        /// Gets or sets the cols.
        /// </summary>
        /// <value>The cols.</value>
        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cols.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The cols property
        /// </summary>
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeUserControl), new PropertyMetadata(0));



        /// <summary>
        /// Gets or sets the name of the maze.
        /// </summary>
        /// <value>The name of the maze.</value>
        public string MazeName
        {
            get { return (string)GetValue(MazeNameProperty); }
            set { SetValue(MazeNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeName.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The maze name property
        /// </summary>
        public static readonly DependencyProperty MazeNameProperty =
            DependencyProperty.Register("MazeName", typeof(string), typeof(MazeUserControl));




        /// <summary>
        /// Gets or sets the maze string.
        /// </summary>
        /// <value>The maze string.</value>
        public string MazeString
        {
            get { return (string)GetValue(MazeStringProperty); }
            set { SetValue(MazeStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MazeString.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The maze string property
        /// </summary>
        public static readonly DependencyProperty MazeStringProperty =
            DependencyProperty.Register("MazeString", typeof(string), typeof(MazeUserControl));


        /// <summary>
        /// Gets or sets the initialize position.
        /// </summary>
        /// <value>The initialize position.</value>
        public Position InitPosition
        {
            get { return (Position)GetValue(InitPositionProperty); }
            set { SetValue(InitPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitPosition.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The initialize position property
        /// </summary>
        public static readonly DependencyProperty InitPositionProperty =
            DependencyProperty.Register("InitPosition", typeof(Position), typeof(MazeUserControl));




        /// <summary>
        /// Gets or sets the goal position.
        /// </summary>
        /// <value>The goal position.</value>
        public Position GoalPosition
        {
            get { return (Position)GetValue(GoalPositionProperty); }
            set { SetValue(GoalPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoalPosition.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The goal position property
        /// </summary>
        public static readonly DependencyProperty GoalPositionProperty =
            DependencyProperty.Register("GoalPosition", typeof(Position), typeof(MazeUserControl));




        /// <summary>
        /// Gets or sets the solve string.
        /// </summary>
        /// <value>The solve string.</value>
        public string SolveString
        {
            get { return (string)GetValue(SolveStringProperty); }
            set { SetValue(SolveStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SolveString.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The solve string property
        /// </summary>
        public static readonly DependencyProperty SolveStringProperty =
            DependencyProperty.Register("SolveString", typeof(string), typeof(MazeUserControl));





        /// <summary>
        /// Gets or sets a value indicating whether [solve finish].
        /// </summary>
        /// <value><c>true</c> if [solve finish]; otherwise, <c>false</c>.</value>
        public bool SolveFinish
        {
            get { return (bool)GetValue(SolveFinishProperty); }
            set { SetValue(SolveFinishProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SolveFinish.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The solve finish property
        /// </summary>
        public static readonly DependencyProperty SolveFinishProperty =
            DependencyProperty.Register("SolveFinish", typeof(bool), typeof(MazeUserControl));



        /// <summary>
        /// Gets or sets a value indicating whether [finish game].
        /// </summary>
        /// <value><c>true</c> if [finish game]; otherwise, <c>false</c>.</value>
        public bool FinishGame
        {
            get { return (bool)GetValue(FinishGameProperty); }
            set { SetValue(FinishGameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FinishGame.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The finish game property
        /// </summary>
        public static readonly DependencyProperty FinishGameProperty =
            DependencyProperty.Register("FinishGame", typeof(bool), typeof(MazeUserControl));




        /// <summary>
        /// The current position
        /// </summary>
        public Position currentPosition;
        /// <summary>
        /// The width of block
        /// </summary>
        private double WidthOfBlock;
        /// <summary>
        /// The height of block
        /// </summary>
        private double HeightOfBlock;

        /// <summary>
        /// The start image
        /// </summary>
        private Image StartImage;
        /// <summary>
        /// The walls set
        /// </summary>
        private HashSet<Point> WallsSet = new HashSet<Point>();

        /// <summary>
        /// Draws this instance.
        /// </summary>
        public void Draw()
        {
            WidthOfBlock = MazeCanvas.Width / Cols;
            HeightOfBlock = MazeCanvas.Height / Rows;
            canvasBorder.Height = Rows * WidthOfBlock;
            canvasBorder.Width = Cols * HeightOfBlock;
            canvasBorder.BorderBrush = Brushes.Black;
            canvasBorder.BorderThickness = new Thickness(3);
            StartImage = new Image
            {
                Width = WidthOfBlock,
                Height = HeightOfBlock,
                Source = new BitmapImage(new Uri("User.png", UriKind.Relative))
            };
            Canvas.SetLeft(StartImage, InitPosition.Col * WidthOfBlock);
            Canvas.SetTop(StartImage, InitPosition.Row * HeightOfBlock );
            int x = 0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {

                    var rec = new Path
                    {
                        Data = new RectangleGeometry(new Rect(j * WidthOfBlock, i * HeightOfBlock, WidthOfBlock, HeightOfBlock)),
                        Fill = Brushes.White,
                        StrokeThickness = 2
                    };
                    
                    if (MazeString[x].Equals('1'))
                    {
                        rec.Fill = Brushes.Black;
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
        }

        /// <summary>
        /// Handles the <see cref="E:KeyDownHandler" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        public void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            Direction d = new Direction();
            switch (e.Key)
            {
                case Key.Left:
                    d = Direction.Left;
                    break;
                case Key.Right:
                    d = Direction.Right;
                    break;
                case Key.Up:
                    d = Direction.Up;
                    break;
                case Key.Down:
                    d = Direction.Down;
                    break;
            }
            Move(d);
        }


        /// <summary>
        /// Called when [opponent move handler].
        /// </summary>
        /// <param name="message">The message.</param>
        public void OnOpponentMoveHandler(string message)
        {
            var data = (JObject)JsonConvert.DeserializeObject(message);
            string directionString = data["Direction"].Value<string>();
            Direction direction = Direction.Unknown;
            switch (directionString)
            {
                case "Up":
                    direction = Direction.Up;
                    break;
                case "Down":
                    direction = Direction.Down;
                    break;
                case "Left":
                    direction = Direction.Left;
                    break;
                case "Right":
                    direction = Direction.Right;
                    break;
            }

            Dispatcher.Invoke(() =>
            {
                Move(direction);
            });
        }



        /// <summary>
        /// Animations the specified p.
        /// </summary>
        /// <param name="p">The p.</param>
        public void Animation(Position p)
        {
            Thread.Sleep(300);
            Dispatcher.Invoke(() =>
            {
                currentPosition = p;
                Canvas.SetLeft(StartImage, currentPosition.Col * WidthOfBlock);
                Canvas.SetTop(StartImage, currentPosition.Row * HeightOfBlock);
                if (currentPosition.Equals(GoalPosition))
                {
                    SolveFinish = true;
                }
            });
            
        }

        /// <summary>
        /// Restarts this instance.
        /// </summary>
        public void Restart()
        {
            currentPosition = InitPosition;
            Canvas.SetLeft(StartImage, currentPosition.Col * WidthOfBlock);
            Canvas.SetTop(StartImage, currentPosition.Row * HeightOfBlock);
        }


        /// <summary>
        /// Moves the specified direction.
        /// </summary>
        /// <param name="direction">The direction.</param>
        private void Move(Direction direction)
        {

            if (direction == Direction.Left)
            {
                Point left = new Point(currentPosition.Row, currentPosition.Col - 1);
                if (!WallsSet.Contains(left) && (left.Y != -1))
                {
                    currentPosition.Col -= 1;
                    Canvas.SetLeft(StartImage, currentPosition.Col * HeightOfBlock);
                    Canvas.SetTop(StartImage, currentPosition.Row * WidthOfBlock);
                }
            }
            if (direction == Direction.Right)
            {
                Point right = new Point(currentPosition.Row, currentPosition.Col + 1);
                if (!WallsSet.Contains(right) && (right.Y < Cols))
                {
                    currentPosition.Col += 1;
                    Canvas.SetLeft(StartImage, currentPosition.Col * HeightOfBlock);
                    Canvas.SetTop(StartImage, currentPosition.Row * WidthOfBlock);
                }
            }
            if (direction == Direction.Up)
            {
                Point up = new Point(currentPosition.Row - 1, currentPosition.Col);
                if (!WallsSet.Contains(up) && (up.X != -1))
                {
                    currentPosition.Row -= 1;
                    Canvas.SetLeft(StartImage, currentPosition.Col * HeightOfBlock);
                    Canvas.SetTop(StartImage, currentPosition.Row * WidthOfBlock);
                }
            }
            if (direction == Direction.Down)
            {
                Point down = new Point(currentPosition.Row + 1, currentPosition.Col);
                if (!WallsSet.Contains(down) && (down.X < Rows))
                {
                    currentPosition.Row += 1;
                    Canvas.SetLeft(StartImage, currentPosition.Col * HeightOfBlock);
                    Canvas.SetTop(StartImage, currentPosition.Row * WidthOfBlock);
                }
            }
            if (currentPosition.Equals(GoalPosition))
            {
                FinishGame = true;
            }

        }

    }
}