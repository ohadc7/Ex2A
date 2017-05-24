// ***********************************************************************
// Assembly         : GUI
// Author           : ohad
// Created          : 05-15-2017
//
// Last Modified By : ohad
// Last Modified On : 05-19-2017
// ***********************************************************************
// <copyright file="SettingsWindow.xaml.cs" company="">
//     Copyright ©  2017
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <seealso cref="System.Windows.Window" />
    /// <seealso cref="System.Windows.Markup.IComponentConnector" />
    public partial class SettingsWindow : Window
    {
        /// <summary>
        /// The vm
        /// </summary>
        private SettingsViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
        /// </summary>
        public SettingsWindow()
        {
            InitializeComponent();
            vm = new SettingsViewModel();
            this.DataContext = vm;
        }
        /// <summary>
        /// Handles the Click event of the btnOK control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BtnOK_Click(object sender, RoutedEventArgs e)
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
        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow win = (MainWindow)Application.Current.MainWindow;
            win.Show();
            this.Close();
        }
    }
}
