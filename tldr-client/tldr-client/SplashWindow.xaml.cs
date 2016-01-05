using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace tldr_client
{
    public partial class SplashWindow : Window
    {
        string strColor;

        public SplashWindow()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }catch (Exception){}
        }

        private void lblExit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtIP.Text != string.Empty && txtName.Text != string.Empty && strColor != string.Empty)
            {
                MainWindow main = new MainWindow(txtName.Text, strColor, txtIP.Text);
                App.Current.MainWindow = main;
                this.Close();
                main.Show();
            }       
        }

        private void radRed_Checked(object sender, RoutedEventArgs e)
        {
            strColor = "#FFFF3B3B";
        }

        private void radOrange_Checked(object sender, RoutedEventArgs e)
        {
            strColor = "#FFFF943B";
        }

        private void radGreen_Checked(object sender, RoutedEventArgs e)
        {
            strColor = "#FF51CF51";
        }

        private void radBlue_Checked(object sender, RoutedEventArgs e)
        {
            strColor = "#FF51CFCF";
        }

        private void radPurple_Checked(object sender, RoutedEventArgs e)
        {
            strColor = "#FF7351CF";
        }

        private void radPink_Checked(object sender, RoutedEventArgs e)
        {
            strColor = "#FFCF51BE";
        }
    }
}
