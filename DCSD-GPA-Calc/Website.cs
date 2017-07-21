using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DCSD_GPA_Calc
{
    static class Website
    {
        public static Dictionary<string, string> ParseWebpage(string url)
        {
            WebClient webClient = new WebClient();
            string page = webClient.DownloadString(url);

            Dictionary<string, string> subjectsWithGrades = new Dictionary<string, string>();

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

            return subjectsWithGrades;
        }
    }
}
