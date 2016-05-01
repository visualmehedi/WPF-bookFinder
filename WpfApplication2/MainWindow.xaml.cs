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
using System.Xml;

namespace WpfApplication2
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

        //check if the number is a Fibonacci number
        private void Check(object sender, RoutedEventArgs e)
        {
            if (this.IsDigitsOnly(txtBox1.Text.ToString()) && txtBox1.Text != "")
            {
                if (this.isFibonacci(Int32.Parse(txtBox1.Text.ToString())))
                    lbl1.Content = "True";
                else
                    lbl1.Content = "false";
            }
            else
            {
                MessageBox.Show("Pls Whole Numbers Only", "Error");
            }
        }

        //check input, string of whole number
        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        // Returns true if n is a Fibinacci Number, else false
        private bool isFibonacci(int n)
        {
            // n is Fibinacci if one of 5*n*n + 4 or 5*n*n - 4 or both
            // is a perferct square
            return isPerfectSquare(5 * n * n + 4) || isPerfectSquare(5 * n * n - 4);
        }

        // A utility function that returns true if x is perfect square
        private bool isPerfectSquare(int x)
        {
            int s = (int)Math.Sqrt(x);
            return (s * s == x);
        }

        //search in xml file for author, title, genre, description
        private void XmlSearch(object sender, RoutedEventArgs e)
        {
            String parameter = txtBox2.Text.ToLower(); //make textbox input in lower case
            int i = 1;
            String faka = "\"" + parameter + "\"" + " word related books are......\n\n";
            string userName = Environment.UserName; //get PC user name
            string filePath = "C:\\Users\\" + userName + "\\Desktop\\books.xml"; //making the xml file path
            XmlDocument xDoc = new XmlDocument(); //create new instance of XmlDocument
            xDoc.Load(filePath); //loading xml file
            foreach (XmlNode node in xDoc.SelectNodes("catalog/book"))//getting all book node
            {
                String author = node.SelectSingleNode("author").InnerText.ToLower();//get author
                String title = node.SelectSingleNode("title").InnerText.ToLower();//get title
                String genre = node.SelectSingleNode("genre").InnerText.ToLower();//get genre
                String description = node.SelectSingleNode("description").InnerText.ToLower();//get description
                /*
                 * check inputed string with XML document
                 */
                if (author.IndexOf(parameter) != -1 || title.IndexOf(parameter) != -1 || genre.IndexOf(parameter) != -1 || description.IndexOf(parameter) != -1)
                {
                    faka += i + ". " + node.SelectSingleNode("title").InnerText + " by " + node.SelectSingleNode("author").InnerText + "\n";
                    i++;
                }
            }
            //show final search result
            MessageBox.Show(faka, "Result...");
        }
    }
}
