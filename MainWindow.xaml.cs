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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace test_safe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StartButton.Click += Start;
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)sizeComboBox.SelectedItem;
            string content = ((TextBlock)selectedItem.Content).Text.ToString();
            int size = int.Parse(content);

            GameWindow game = new GameWindow(size);
            game.Owner = this;
            game.Show();
            this.Hide();
        }
    }
}
