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
        Console.WriteLine("Ahoj");

        private void CreateChessBoard()
        {
            ChessGrid.Children.Clear();
            ChessGrid.RowDefinitions.Clear();
            ChessGrid.ColumnDefinitions.Clear();

            for (int i = 0; i < size; i++)
            {
                ChessGrid.RowDefinitions.Add(new RowDefinition());
                ChessGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    var cellGrid = new Grid();

                    var rect = new Rectangle
                    {
                        Fill = ((row + col) % 2 == 0) ? color1 : color2,
                        Stroke = Brushes.Black,
                        StrokeThickness = 1
                    };
                    cellGrid.Children.Add(rect);

                    // Čísla 1–8 vpravo, 1 nahoře
                    if (col == size - 1)
                    {
                        var numberLabel = new TextBlock
                        {
                            Text = (row + 1).ToString(),
                            FontWeight = FontWeights.Bold,
                            FontSize = 12,
                            HorizontalAlignment = HorizontalAlignment.Right,
                            VerticalAlignment = VerticalAlignment.Top,
                            Margin = new Thickness(0, 2, 2, 0)
                        };
                        cellGrid.Children.Add(numberLabel);
                    }

                    // Písmena h–a dole, a úplně vpravo
                    if (row == size - 1)
                    {
                        var letterLabel = new TextBlock
                        {
                            Text = ((char)('a' + col)).ToString(),
                            FontWeight = FontWeights.Bold,
                            FontSize = 12,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Bottom,
                            Margin = new Thickness(2, 0, 0, 2)
                        };
                        cellGrid.Children.Add(letterLabel);
                    }

                    Grid.SetRow(cellGrid, row);
                    Grid.SetColumn(cellGrid, col);
                    ChessGrid.Children.Add(cellGrid);
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