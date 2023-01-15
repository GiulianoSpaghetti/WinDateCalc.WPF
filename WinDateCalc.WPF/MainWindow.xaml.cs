using Microsoft.Win32;
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

namespace WinDateCalc.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RegistryKey k1;
        public MainWindow()
        {
            InitializeComponent();
            int anno=-1, mese=-1, giorno = -1;
            RegistryKey k = Registry.CurrentUser.OpenSubKey("SOFTWARE", true);
            k1=k.OpenSubKey("WinDateCalc", true);
            if (k1 != null) {
                anno = (int)k1.GetValue("anno", -1);
                mese = (int)k1.GetValue("mese", -1);
                giorno = (int)k1.GetValue("giorno", -1);
            }  else
                k1 = k.CreateSubKey("WinDateCalc");

            if (anno==-1)
            {            
                if (k1==null)
                {
                    MessageBox.Show("Impossibile creare la chiave di registro, i dati non verranno salvati.", "Errore");
                    return;
                }
                k1.SetValue("anno", 2023);
                k1.SetValue("mese", 8);
                k1.SetValue("giorno", 15);
                anno = 2023;
                mese = 8;
                giorno = 15;
            }
            data.DisplayDate = new DateTime(anno, mese, giorno);
        }

        private void calcola_Click(object sender, EventArgs e)
        { 
            if (data.SelectedDate==null)
            {
                risultato.Content = "Selezionare una data";
                risultato.Foreground = Brushes.Red;
                return;
            }
            DateTime d = DateTime.Now;
            TimeSpan differenza = (TimeSpan)(data.SelectedDate - d);
            risultato.Content = $"Mancano {differenza.Days} giorni, {differenza.Hours} ore e {differenza.Minutes} minuti.";
            risultato.Foreground = Brushes.Black;
            k1.SetValue("anno", data.SelectedDate.Value.Year);
            k1.SetValue("mese", data.SelectedDate.Value.Month);
            k1.SetValue("giorno", data.SelectedDate.Value.Day);
        }

        private async void info_Click(object sender, EventArgs e)
        {  
//            await Navigation.PushAsync(new InfoPage());
        }
    }
}
