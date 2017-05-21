using GUI.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace GUI.Controls
{
    /// <summary>
    /// Interaction logic for MazeDefinitionsUserControl.xaml
    /// </summary>
    public partial class MazeDefinitionsUserControl : UserControl
    {
        public MazeDefinitionsUserControl()
        {
            InitializeComponent();
           
        }



        public int RowsFromSettings
        {
            get { return (int)GetValue(RowsFromSettingsProperty); }
            set { SetValue(RowsFromSettingsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RowsFromSettings.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsFromSettingsProperty =
            DependencyProperty.Register("RowsFromSettings", typeof(int), typeof(MazeDefinitionsUserControl));




        public int ColsFromSettings
        {
            get { return (int)GetValue(ColsFromSettingsProperty); }
            set { SetValue(ColsFromSettingsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ColsFromSettings.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsFromSettingsProperty =
            DependencyProperty.Register("ColsFromSettings", typeof(int), typeof(MazeDefinitionsUserControl));
      }
}
