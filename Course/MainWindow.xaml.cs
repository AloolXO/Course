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
using System.Data.Entity;
using System.Diagnostics;
using System.Data.SQLite;
using System.Data;

namespace Course
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SQLiteConnection DB;

        public MainWindow()
        {
            InitializeComponent();

           /* db = new ApplicationContext();
            db.Applics.Load();
            this.DataContext = db.Applics.Local.ToBindingList();*/
            
        }

        private void OpenOneBook(object sender, RoutedEventArgs e)
        {
            string name = "";
            if (AppNameList.Text != "")
            {
                
                SQLiteCommand sQLiteCommand = DB.CreateCommand();
                sQLiteCommand.CommandText = "select * from Applics JOIN Books where Name like '%' || @AppNameList || '%'  AND AppId=BookId";
                sQLiteCommand.Parameters.Add("@AppNameList", System.Data.DbType.String).Value = AppNameList.Text;
                SQLiteDataReader sQLite = sQLiteCommand.ExecuteReader();
                if (sQLite.HasRows)
                {
                    while (sQLite.Read())
                    {
                        name = sQLite["Book"].ToString();
                        break;
                    }
                }
                else
                {
                    tDate.Text = AppNameList.Text;
                }
            }
            if (name != "")
            {
                Process.Start(name);
            }
            else
                MessageBox.Show("Для данной программы не был найден учебник!");
        }

        private void MainLoad(object sender, RoutedEventArgs e)
        {
            DB = new SQLiteConnection("Data Source=Course.db; Version=3");
            DB.Open();

            var sqlCommand = new SQLiteCommand("select Name from Applics", DB);
            var sqlAdapter = new SQLiteDataAdapter(sqlCommand);
            DataSet dataTable = new DataSet();
            sqlAdapter.Fill(dataTable, "Applics");
            AppNameList.ItemsSource = dataTable.Tables[0].DefaultView;
            AppNameList.DisplayMemberPath = dataTable.Tables[0].Columns["Name"].ToString();

        }

        private void Update(object sender, RoutedEventArgs e)
        {
            if (AppNameList.Text != "")
            {
                string s = "", p ="";
                tDate.Text = "";
                tAbout.Text = "";
                tType.Text = "";
                OneBook.Content = "";
                TwoBook.Content = "";
                SQLiteCommand sQLiteCommand = DB.CreateCommand();
                sQLiteCommand.CommandText = "select * from Applics, Books where Name like '%' || @AppNameList || '%'";
                sQLiteCommand.Parameters.Add("@AppNameList", System.Data.DbType.String).Value = AppNameList.Text;
                SQLiteDataReader sQLite = sQLiteCommand.ExecuteReader();
                if (sQLite.HasRows)
                {
                    while (sQLite.Read())
                    {
                        tDate.Text += sQLite["Date"] + "\r";
                        tAbout.Text += sQLite["About"] + "\r";
                        s += sQLite["WebSite"]; 
                        p += sQLite["Picture"];
                        tType.Text += sQLite["Type"] + "\r";
                        OneBook.Content += AppNameList.Text + " (Первый учебник)";
                        TwoBook.Content += AppNameList.Text + " (Второй учебник)";
                        break;
                    }
                    Uri uri = new Uri(s.ToLowerInvariant());
                    Site.NavigateUri = uri;
                    BitmapImage bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.UriSource = new Uri(p, UriKind.Relative);
                    bmp.EndInit();
                    picture.Source = bmp;
                } 
                else
                {
                    tDate.Text = AppNameList.Text;
                }
                
            }
        }

        private void Web_Site(object sender, RoutedEventArgs e)
        {
            string name = "";
            if (AppNameList.Text != "")
            {

                SQLiteCommand sQLiteCommand = DB.CreateCommand();
                sQLiteCommand.CommandText = "select * from Applics where Name like '%' || @AppNameList || '%'";
                sQLiteCommand.Parameters.Add("@AppNameList", System.Data.DbType.String).Value = AppNameList.Text;
                SQLiteDataReader sQLite = sQLiteCommand.ExecuteReader();
                if (sQLite.HasRows)
                {
                    while (sQLite.Read())
                    {
                        name += sQLite["WebSite"];
                        break;
                    }
                }
                else
                {
                    tDate.Text = AppNameList.Text;
                }
            }
            if (name != "")
            {
                Process.Start(name);
            }
            else
                MessageBox.Show("Для данной программы не был найден официальный сайт!");
        }

        private void OpenTwoBook(object sender, RoutedEventArgs e)
        {
            string name = "";
            if (AppNameList.Text != "")
            {

                SQLiteCommand sQLiteCommand = DB.CreateCommand();
                sQLiteCommand.CommandText = "select * from Applics JOIN Books where Name like '%' || @AppNameList || '%'  AND AppId=BookId";
                sQLiteCommand.Parameters.Add("@AppNameList", System.Data.DbType.String).Value = AppNameList.Text;
                SQLiteDataReader sQLite = sQLiteCommand.ExecuteReader();
                if (sQLite.HasRows)
                {
                    while (sQLite.Read())
                    {
                        name = sQLite["Book"].ToString();
                        
                    }
                }
                else
                {
                    tDate.Text = AppNameList.Text;
                }
            }
            if (name != "")
            {
                Process.Start(name);
            }
            else
                MessageBox.Show("Для данной программы не был найден учебник!");
        }
    }
}
