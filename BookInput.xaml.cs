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

namespace biblia
{
    /// <summary>
    /// Interaction logic for BookInput.xaml
    /// </summary>
    public partial class BookInput : Window
    {
        public BookInput()
        {
            InitializeComponent();
            // adds UI
            System.Windows.Controls.Label[] labels = new System.Windows.Controls.Label[6];
            System.Windows.Controls.TextBox[] textboxes = new System.Windows.Controls.TextBox[6];
            string[] inputs = { "ID", "Title", "Author", "Year", "Publisher", "Avaliability" };
            string[] inputsxdpl = { "ID", "Tytul", "Autor", "Rok", "Wydawca", "Dostepnosc" };
            int[] widths = { 350, 230, 110, 125, 145 };
            int[] margins = { 100, 460, 700, 820, 955, 1110 };
            for (int i = 0; i < 5; i++)
            {
                labels[i] = new Label();
                textboxes[i] = new TextBox();
                labels[i].Content = inputsxdpl[i];
                labels[i].Name = "Label" + inputs[i];
                labels[i].Height = 40;
                labels[i].Width = widths[i];
                labels[i].FontSize = 24;
                labels[i].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                labels[i].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                labels[i].Margin = new Thickness(margins[i], 20, 0, 0);
                textboxes[i].Name = "TextBox" + inputs[i].ToString();
                textboxes[i].Height = 30;
                textboxes[i].Width = widths[i];
                textboxes[i].FontSize = 24;
                textboxes[i].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                textboxes[i].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                textboxes[i].Margin = new Thickness(margins[i], 65, 0, 0);
                grid.Children.Add(labels[i]);
                grid.Children.Add(textboxes[i]);
            }
            System.Windows.Controls.Button GOButton = new Button();
            GOButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            GOButton.VerticalAlignment = System.Windows.VerticalAlignment.Top;
            GOButton.Margin = new Thickness(margins[5], 65, 0, 0);
            GOButton.Height = 30;
            GOButton.Width = 50;
            GOButton.Name = "GOButton";
            GOButton.FontSize = 20;
            GOButton.Content = "GO";
            grid.Children.Add(GOButton);
            GOButton.Click += GOButton_Click;

            // button click :D
            void GOButton_Click(object sender, EventArgs e)
            {
                if (Int32.TryParse(textboxes[3].Text, out int xd))
                {

                }
            }
        }
    }
}