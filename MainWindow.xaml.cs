using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UglyToad.PdfPig.Writer;
using UglyToad.PdfPig;
using UglyToad.PdfPig.Content;
using Microsoft.Win32;
using System.IO;

namespace ClickText_PDF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "PDF Documents (*.pdf)|*.pdf";

            if (ofd.ShowDialog() == true)
            {
                LoadPdf(ofd.FileName);
            }
        }
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            CopySelection1();
        }
        private void Addition_Click(object sender, RoutedEventArgs e)
        {
            CopySelection2();
        }
        private void CKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C)
            {
                CopySelection1();
            }
            if (e.Key == Key.D)
            {
                CopySelection2();
            }
        }
        private void CopySelection1()
        {
            if (mainDocument.Blocks != null && rtb.Selection.Text != null)
            {
                using (StreamWriter writer = new StreamWriter("copied.txt", true))
                {
                    writer.WriteLine(rtb.Selection.Text);
                }
                try
                {
                    TextPointer startPos = rtb.Selection.Start;
                    TextPointer endPos = rtb.Selection.End;
                    var textRange = new TextRange(startPos, endPos);
                    textRange.ApplyPropertyValue(
                        TextElement.BackgroundProperty,
                        new SolidColorBrush(Colors.PaleGoldenrod));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void CopySelection2()
        {
            if (mainDocument.Blocks != null && rtb.Selection.Text != null)
            {
                using (StreamWriter writer = new StreamWriter("addition.txt", true))
                {
                    writer.WriteLine(rtb.Selection.Text);
                }
                try
                {
                    TextPointer startPos = rtb.Selection.Start;
                    TextPointer endPos = rtb.Selection.End;
                    var textRange = new TextRange(startPos, endPos);
                    textRange.ApplyPropertyValue(
                        TextElement.BackgroundProperty,
                        new SolidColorBrush(Colors.LightPink));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Help_MouseDown(object sender, MouseButtonEventArgs e)
        {
            helpGroubBox_Visibility();
        }
        private void Help_XClick(object sender, RoutedEventArgs e)
        {
            helpGroubBox_Visibility();
        }
        private void helpGroubBox_Visibility()
        {
            if (helpGroupBox.Visibility == Visibility.Hidden)
            {
                helpGroupBox.Visibility = Visibility.Visible;
            }
            else
            {
                helpGroupBox.Visibility = Visibility.Hidden;
            }
        }
        private void Help_MouseEnter(object sender, MouseEventArgs e)
        {
            helpBackGround.Fill=new SolidColorBrush(Colors.CornflowerBlue);
        }
        private void Help_MouseLeave(object sender, MouseEventArgs e)
        {
            helpBackGround.Fill = new SolidColorBrush(Colors.Gray);
        }
        private void LoadPdf(string path)
        {
            using (PdfDocument document = PdfDocument.Open(path))
            {
                foreach (Page page in document.GetPages())
                {
                    Paragraph paragraph = new Paragraph(new Run(page.Text));
                    mainDocument.Blocks.Add(paragraph);
                }
            }
        }
    }
}
