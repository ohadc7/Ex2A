using GUI.ViewModel;
using System.Windows.Controls;

namespace GUI.Controls
{
    /// <summary>
    /// Interaction logic for MazeDefinitionsUserControl.xaml
    /// </summary>
    public partial class MazeDefinitionsUserControl : UserControl
    {
        private MazeDefinitionsUserControlVM mdusvm;

        public MazeDefinitionsUserControl()
        {
            InitializeComponent();
            this.mdusvm = new MazeDefinitionsUserControlVM();
            this.DataContext = mdusvm;
        }

        private void txtMazeName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtRows_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtCols_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
