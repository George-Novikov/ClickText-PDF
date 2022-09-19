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
        private void CKey_KeyDown(object sender, KeyEventArgs e)
        {
            CopySelection1();
        }
        private void CopySelection1()
        {
            if (mainDocument.Blocks != null && rtb.Selection.Text != null)
            {
                using (StreamWriter writer = new StreamWriter("buffer.txt", true))
                {
                    writer.WriteLine(rtb.Selection.Text);
                }

                try
                {
                    TextPointer endPosition = rtb.CaretPosition;
                    TextPointer startPosition = endPosition.GetPositionAtOffset(-rtb.Selection.Text.Length);
                    var textRange = new TextRange(startPosition, endPosition);
                    textRange.ApplyPropertyValue(
                        TextElement.BackgroundProperty,
                        new SolidColorBrush(Colors.PaleGoldenrod));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                /*try
                {
                    TextRange content = new TextRange(mainDocument.ContentStart, mainDocument.ContentEnd);
                    int firstIndex = content.Text.IndexOf(rtb.Selection.Text);
                    int lastIndex = firstIndex + rtb.Selection.Text.Length+1;

                    TextPointer pointer = mainDocument.ContentStart;
                    while (pointer.GetPointerContext(LogicalDirection.Forward) != TextPointerContext.Text)
                    {
                        pointer = pointer.GetNextContextPosition(LogicalDirection.Forward);
                    }
                    TextPointer startPos = pointer.GetPositionAtOffset(firstIndex);
                    TextPointer endPos = pointer.GetPositionAtOffset(lastIndex);
                    var textRange = new TextRange(startPos, endPos);
                    textRange.ApplyPropertyValue(
                        TextElement.BackgroundProperty,
                        new SolidColorBrush(Colors.CornflowerBlue));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }*/
            }
        }
        private void Help_MouseDown()
        {
            //TO DO: make a help window
        }
        private void Help_MouseOver()
        {
            //TO DO: make an event to change color of help button ellipse
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
