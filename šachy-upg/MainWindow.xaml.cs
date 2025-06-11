using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace šachy_upg
{
    public partial class MainWindow : Window
    {
        private readonly int size = 8;
        private Brush color1 = Brushes.White;
        private Brush color2 = Brushes.Gray;
        private bool colorToggle = false;

        public MainWindow()
        {
            InitializeComponent();
            CreateChessBoard();
        }

        private void CreateChessBoard()
        {
            ChessGrid.Children.Clear();
            ChessGrid.RowDefinitions.Clear();
            ChessGrid.ColumnDefinitions.Clear();

            // Přidáme řádky a sloupce (8 políček + 2 okraje pro popisky)
            for (int i = 0; i < size + 2; i++)
            {
                ChessGrid.RowDefinitions.Add(new RowDefinition());
                ChessGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // Přidání písmen (a-h) nahoře a dole
            for (int col = 0; col < size; col++)
            {
                var topLabel = new TextBlock()
                {
                    Text = ((char)('a' + col)).ToString(),
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(topLabel, 0);
                Grid.SetColumn(topLabel, col + 1);
                ChessGrid.Children.Add(topLabel);

                var bottomLabel = new TextBlock()
                {
                    Text = ((char)('a' + col)).ToString(),
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(bottomLabel, size + 1);
                Grid.SetColumn(bottomLabel, col + 1);
                ChessGrid.Children.Add(bottomLabel);
            }
            Console.WriteLine("ahoj"); 
            // Přidání čísel (8-1) vlevo i vpravo
            for (int row = 0; row < size; row++)
            {
                var leftLabel = new TextBlock()
                {
                    Text = (size - row).ToString(),
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(leftLabel, row + 1);
                Grid.SetColumn(leftLabel, 0);
                ChessGrid.Children.Add(leftLabel);

                var rightLabel = new TextBlock()
                {
                    Text = (size - row).ToString(),
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Grid.SetRow(rightLabel, row + 1);
                Grid.SetColumn(rightLabel, size + 1);
                ChessGrid.Children.Add(rightLabel);
            }

            // Vytvoření políček šachovnice
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    var rect = new Rectangle
                    {
                        Fill = ((row + col) % 2 == 0) ? color1 : color2,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };
                    Grid.SetRow(rect, row + 1);
                    Grid.SetColumn(rect, col + 1);
                    ChessGrid.Children.Add(rect);
                }
            }
        }

        private void ChangeColors_Click(object sender, RoutedEventArgs e)
        {
            colorToggle = !colorToggle;
            if (colorToggle)
            {
                color1 = Brushes.Brown;
                color2 = Brushes.RosyBrown;
            }
            else
            {
                color1 = Brushes.White;
                color2 = Brushes.Gray;
            }
            CreateChessBoard();
        }
    }
}