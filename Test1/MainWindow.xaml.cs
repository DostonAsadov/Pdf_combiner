using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Shapes;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using static System.Net.Mime.MediaTypeNames;


namespace Test1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Get some file names
        const string sng_folder = @"C:\Users\Asus\Desktop\test\";
        string[] files = [];                           
        //string final_path_inputed = "";
        //string yangi_Document_ismi = "";
        string final_path = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;

            bool? success = fileDialog.ShowDialog();
            if (success == true)
            {
             
                string path_of_file = fileDialog.FileName;
                string content_of_file = File.ReadAllText(path_of_file);
                files = Directory.GetFiles(System.IO.Path.Combine(sng_folder, content_of_file)); // taking files path

                tbInfo.Text = content_of_file;
            }
            else
            {
                //didnt pick anything
            }
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            
            // Open the output document
            PdfDocument yangi_Document = new PdfDocument();

            // Iterate files
            foreach (string file in files)
            {
                // Open the document to import pages from it.
                PdfDocument input_Document = PdfReader.Open(file, PdfDocumentOpenMode.Import);

                // Iterate pages
                int count = input_Document.PageCount;
                for (int i = 0; i < count; i++)
                {
                    // Get the page from the external document...
                    PdfPage page = input_Document.Pages[i];
                    // ...and add it to the output document.
                    yangi_Document.AddPage(page);
                }

            }


            yangi_Document.Options.CompressContentStreams = true;
            yangi_Document.Options.NoCompression = false;
            final_path = @"C:\Users\Asus\Desktop\test\12345.pdf";                 //final_path_inputed + "\\" + yangi_Document_ismi;   // Fullname of new pdf file
            yangi_Document.Save(final_path);                                      // Final path for save

            //Process.Start(final_path);
        }
    }
}
