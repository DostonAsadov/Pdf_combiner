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
        string[] selected_folders = [];
        string[] files = [];
        string new_name_of_file = "";
        string final_path = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SelectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog inserted_text_file = new OpenFileDialog();
            inserted_text_file.Multiselect = true;

            bool? success = inserted_text_file.ShowDialog();
            if (success == true)
            {
             
                string path_of_file = inserted_text_file.FileName;  //Text fileni tuliq yulini olyapman
                string content_of_file = File.ReadAllText(path_of_file); //Faylni tuliq yulidan foydalanib, uni ichidagi malumotlarni uqiyapmiz
                //files = Directory.GetFiles(System.IO.Path.Combine(sng_folder, content_of_file)); //SNG+Odamni ismi=odamni pdflari turgan papkani yuli(papka,fayl emas)
                //new_name_of_file = content_of_file; //text fayldagi yozildan malumotlar.
                selected_folders = File.ReadAllLines(path_of_file);  //txt dagi arrayni oldik

                tbInfo.Text = "Ruyhat olindi";      //textbox da contentni chiqarish
            }
            else
            {
                //didnt pick anything
            }
        }

     
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (string  name_of_people in selected_folders)
            {
                string folder_of_people = name_of_people;
                
                files = Directory.GetFiles(System.IO.Path.Combine(sng_folder, folder_of_people));

                PdfDocument yangi_Document = new PdfDocument();     // Open the output document

                foreach (string file in files)                      // Iterate files
                {
                    PdfDocument input_Document = PdfReader.Open(file, PdfDocumentOpenMode.Import);    // Open the document to import pages from it.

                    int count = input_Document.PageCount;           // Iterate pages
                    for (int i = 0; i < count; i++)
                    {
                        PdfPage page = input_Document.Pages[i];     // Get the page from the external document...
                        yangi_Document.AddPage(page);               // ...and add it to the output document.
                    }

                }

                final_path = string.Concat(System.IO.Path.Combine(sng_folder), name_of_people, ".pdf");                 // Fullname of new pdf file

                yangi_Document.Options.CompressContentStreams = true;
                yangi_Document.Options.NoCompression = false;
                yangi_Document.Save(final_path);                                                                // Final path for save
                //Process.Start(final_path);                                                                    //srazu pdf ochib ketadi.
            }
        }
    }
}
