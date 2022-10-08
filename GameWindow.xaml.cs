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

namespace test_safe
{
    public partial class GameWindow : Window
    {
        private int size;
        private Game viewModel;
        private BoolToStringConverter converter;

        public GameWindow(int _size)
        {
            InitializeComponent();

            size = _size;

            viewModel = new Game(_size);
            viewModel.CreateGame();

            converter = new BoolToStringConverter();
            converter.TrueValue = "-";
            converter.FalseValue = "|";

            CreateGrid();
        }

        void CreateGrid()
        {
            for (int i = 0; i < size; i++)
            {
                ColumnDefinition col = new ColumnDefinition();
                gameGrid.ColumnDefinitions.Add(col);

                RowDefinition row = new RowDefinition();
                gameGrid.RowDefinitions.Add(row);
            }

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Button _safeButton = new Button();
                    _safeButton.Width = 64;
                    _safeButton.Height = 64;
                    _safeButton.Click += SwitchButton;

                    //there could be a better solution to this abomination, but that'll do
                    //basically, considering that max size is 10 which is 1010 = 4 bits in binary, we can store both indices in 1 byte
                    //and then store that to correctly remember button's represented index
                    _safeButton.Tag = (byte)((byte)j + ((byte)i << 4));

                    gameGrid.Children.Add(_safeButton);
                    Grid.SetRow(_safeButton, i);
                    Grid.SetColumn(_safeButton, j);

                    Binding _binding = new Binding("State");
                    _binding.Source = viewModel.GameField[i * size + j];
                    _binding.Converter = converter;
                    _binding.Mode = BindingMode.OneWay;
                    _safeButton.SetBinding(Button.ContentProperty, _binding);
                }
            }
        }

        void SwitchButton(object sender, RoutedEventArgs e)
        {
            Button senderButton = (Button)sender;
            int tag = (byte)senderButton.Tag;
            int j = tag & 15;
            int i = (tag & (15 << 4)) >> 4;

            viewModel.Switch(i, j);

            if (viewModel.CheckIfOpen())
                MessageBox.Show("Парам-пам! Сейф открыт!");
        }

        protected override void OnClosed(EventArgs e)
        {
            Owner.Show();
            base.OnClosed(e);
        }
    }
}
