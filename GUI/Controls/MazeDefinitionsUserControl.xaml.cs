// ***********************************************************************
// Assembly         : GUI
// Author           : ohad
// Created          : 05-15-2017
//
// Last Modified By : ohad
// Last Modified On : 05-21-2017
// ***********************************************************************
// <copyright file="MazeDefinitionsUserControl.xaml.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
using GUI.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace GUI.Controls
{
    /// <summary>
    /// Interaction logic for MazeDefinitionsUserControl.xaml
    /// </summary>
    /// <seealso cref="System.Windows.Controls.UserControl" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class MazeDefinitionsUserControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MazeDefinitionsUserControl"/> class.
        /// </summary>
        public MazeDefinitionsUserControl()
        {
            InitializeComponent();
           
        }



        /// <summary>
        /// Gets or sets the rows from settings.
        /// </summary>
        /// <value>The rows from settings.</value>
        public int RowsFromSettings
        {
            get { return (int)GetValue(RowsFromSettingsProperty); }
            set { SetValue(RowsFromSettingsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RowsFromSettings.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The rows from settings property
        /// </summary>
        public static readonly DependencyProperty RowsFromSettingsProperty =
            DependencyProperty.Register("RowsFromSettings", typeof(int), typeof(MazeDefinitionsUserControl));




        /// <summary>
        /// Gets or sets the cols from settings.
        /// </summary>
        /// <value>The cols from settings.</value>
        public int ColsFromSettings
        {
            get { return (int)GetValue(ColsFromSettingsProperty); }
            set { SetValue(ColsFromSettingsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColsFromSettings.  This enables animation, styling, binding, etc...
        /// <summary>
        /// The cols from settings property
        /// </summary>
        public static readonly DependencyProperty ColsFromSettingsProperty =
            DependencyProperty.Register("ColsFromSettings", typeof(int), typeof(MazeDefinitionsUserControl));
      }
}
