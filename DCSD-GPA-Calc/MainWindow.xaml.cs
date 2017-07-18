using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace DCSD_GPA_Calc
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

        private Dictionary<string, string> subjectsWithGrades = new Dictionary<string, string>();

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            if (txtNIBMId.Text.Length > 0)
            {
                string id = CleanString(txtNIBMId.Text);

                bool saveFile = (chkBoxFileSave.IsChecked == true);

                string url = "https://www.nibm.lk/students/exams/results?q=" + id;                

                string page = " ";
                
                new Thread(() => {

                    Thread.Sleep(100);
                    progressBar.Dispatcher.BeginInvoke(
                        (Action)(() => {
                            progressBar.IsIndeterminate = true;
                            progressBar.Visibility = Visibility.Visible;
                        }));

                    try
                    {
                        page = Website.GetWebsite(url);

                        ParseWebpage(page);

                        if (subjectsWithGrades.Count > 0)
                        {

                            Calculate calculate = new Calculate();

                            double result = calculate.GPA(subjectsWithGrades);

                            Thread.Sleep(1000);
                            lblGPA.Dispatcher.BeginInvoke(
                                (Action)(() => { lblGPA.Content = "GPA: " + Math.Round(result, 2).ToString(); }));

                            if (saveFile)
                            {
                                string pathToDesktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\NIBM-DCSD-RESULTS.txt";
                                
                                StreamWriter streamWriter = new StreamWriter(pathToDesktop);

                                foreach (var subject in subjectsWithGrades)
                                {
                                    streamWriter.WriteLine(subject.Key + "\t" + subject.Value);
                                }

                                streamWriter.WriteLine();

                                streamWriter.WriteLine("GPA: " + Math.Round(result, 3).ToString());

                                streamWriter.Close();
                                
                                MessageBox.Show("Your results are saved at " + pathToDesktop, "Results Saved!", MessageBoxButton.OK, MessageBoxImage.Information);

                            }

                            subjectsWithGrades.Clear();                            
                        }

                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Can't connect to the internet! Please check your connection!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    Thread.Sleep(100);
                    progressBar.Dispatcher.BeginInvoke(
                        (Action)(() => {
                            progressBar.IsIndeterminate = false;
                            progressBar.Visibility = Visibility.Hidden;
                        }));

                }).Start();

            } 
            else
            {
                MessageBox.Show("Please enter a ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }

        }

        private void ParseWebpage(string page)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);

            List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='w0']")
            .Descendants("tr")
            .Skip(1)
            .Where(tr => tr.Elements("td").Count() > 1)
            .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
            .ToList();

            if (table.Count > 0)
            {
                for (int i = 0; i < table.Count; i++)
                {
                    if (!subjectsWithGrades.ContainsKey(table[i][1]))
                    {
                        subjectsWithGrades.Add(table[i][1], table[i][3]);
                    }

                }
            }
            else
            {
                MessageBox.Show("Please enter a valid ID!", "Error", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private static string CleanString(string id)
        {
            return Regex.Replace(id, @"[^\w\.@-]", "");
        }
    }
}
