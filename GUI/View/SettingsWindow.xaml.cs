using System.Windows;
using GUI.Model;
using GUI.ViewModel;
using System.Windows.Data;
using System.Windows.Controls;

namespace GUI.View
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private SettingsViewModel vm;
       
        public SettingsWindow()
        {
            InitializeComponent();
            vm = new SettingsViewModel();
            this.DataContext = vm;
        }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression be = txtIP.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = txtPort.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = txtRows.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = txtCols.GetBindingExpression(TextBox.TextProperty);
            be.UpdateSource();
            be = cboSearchAlgo.GetBindingExpression(ComboBox.SelectedIndexProperty);
            be.UpdateSource();
            
            vm.SaveSettings();
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}
