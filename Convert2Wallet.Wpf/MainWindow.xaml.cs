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
using Convert2Wallet_Core;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using Microsoft.Win32;

namespace Convert2Wallet.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private readonly Func<int, > _createNewPassbookWindow;
        //private readonly Func<int, EditBookingWindow> _editBookingWindowCreator;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btn_CreateNewPassbook(object sender, RoutedEventArgs e)
        {
            EditPassbookWindow editPassbookWindow = new EditPassbookWindow();
            // Beim Öffnen des EditPassbookWindows soll das MainWindow versteckt werden
            this.Hide();
            editPassbookWindow.ShowDialog();
            // Beim Schließen des EditPassbookWindows soll das MainWindow wieder angezeigt werden
            this.Show();
        }

        private void Btn_LoadDataFromFile(object sender, RoutedEventArgs e)
        {
            // Öffnet ein Fenster zum Auswahl einer Datei
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // Mit diesem Filter können nur PDF-Dokumente geöffnet werden
            openFileDialog.Filter = "PDF Datei (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == true) // Öffnet ein Dialogfenster zur Auswahl der PDF-Datei
            {
                // open Pdf File
                var pdfDocument = new PdfDocument(new PdfReader(openFileDialog.FileName));
                // Create a text extraction strategy
                var extractionStrategy = new LocationTextExtractionStrategy();

                var text = new StringBuilder();

                for (var i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
                {
                    var page = pdfDocument.GetPage(i);
                    var currentText = PdfTextExtractor.GetTextFromPage(page, extractionStrategy);
                    text.Append(currentText);
                }

                // Close the PDF document
                pdfDocument.Close();

                // EditPassbookWindow wird erstellt und der eingelesene Text wird mitgesendet
                EditPassbookWindow editPassbookWindow = new EditPassbookWindow(text.ToString());
                this.Hide();
                editPassbookWindow.ShowDialog();
                this.Show();

            }
        }

        private void Btn_LoadDataFromText(object sender, RoutedEventArgs e)
        {
            // Öffnet das TextblockFenster
            TextBlockDialog textBlockDialog = new TextBlockDialog();
            textBlockDialog.ShowDialog();

            // Eingegebener Text aus TextblockFenster wird abgespeichert
            string inputText = textBlockDialog.InputText.Text;
            // EditPassbookWindow wird erstellt und der eingelesene Text wird mitgesendet
            EditPassbookWindow editPassbookWindow = new EditPassbookWindow(inputText);
            this.Hide();
            editPassbookWindow.ShowDialog();
            this.Show();
        }
    }
}
