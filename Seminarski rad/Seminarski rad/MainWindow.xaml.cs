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
using System.Timers;
using Timer = System.Timers.Timer;

namespace Seminarski_rad
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	/// 
	//test komentar
	public partial class MainWindow : Window
	{
		private static Timer tajmer;
		public static List<Label> ListaPolja = new List<Label>();
		public static Label[,] Polja = new Label[15, 15];
		static Engine engine = new Engine();
		public MainWindow()
		{

			InitializeComponent();
			foreach (var v in Grid1.Children)
				if (ListaPolja.Count < 225)
					if (v is Label)
					{
						ListaPolja.Add((Label)v);
					}
					else { }
				else
					break;

			int brojac = 0;
			for (int i = 0; i < 15; i++)
			{

				for (int j = 0; j < 15; j++)
				{
					Polja[i, j] = ListaPolja[brojac];
					brojac++;
				}
			}
		}

		static int interval;

		//public int Interval { get; private set; }

		void SetTimer(int interval)
		{
			tajmer = new Timer(interval);
			tajmer.Elapsed += OnTimerElapsed;
			tajmer.AutoReset = true;
			tajmer.Enabled = true;
		}
		private void OnTimerElapsed(object sender, ElapsedEventArgs e)
		{
			this.Dispatcher.Invoke(() =>
			{
				Polje000.Content = DateTime.Now;
			});
			/*if (tajmer != null)
            {
                tajmer.Stop();
                tajmer.Dispose();
            }*/
		}
		private void Nivo1Button_Click(object sender, RoutedEventArgs e)
		{
			nivo2Button.Background = Brushes.White;
			nivo3Button.Background = Brushes.White;
			nivo1Button.Background = Brushes.Red;
			interval = 1000;
		}

		private void Nivo2Button_Click(object sender, RoutedEventArgs e)
		{
			nivo1Button.Background = Brushes.White;
			nivo3Button.Background = Brushes.White;
			nivo2Button.Background = Brushes.Red;
			interval = 500;
		}

		private void Nivo3Button_Click(object sender, RoutedEventArgs e)
		{
			nivo1Button.Background = Brushes.White;
			nivo2Button.Background = Brushes.White;
			nivo3Button.Background = Brushes.Red;
			interval = 300;
		}

		private void StartButton_Click(object sender, RoutedEventArgs e)
		{
			postaviFont();
			SetTimer(interval);
			engine.PokreniEngine();
		}

		private void darkThemeButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (Label l in ListaPolja)
			{
				l.Background = new SolidColorBrush(Color.FromRgb(32, 32, 32));
				Grid1.Background = new SolidColorBrush(Color.FromRgb(0, 102, 51));
			}
		}

		private void lightThemeButton_Click(object sender, RoutedEventArgs e)
		{
			foreach (Label l in ListaPolja)
			{
				l.Background = Brushes.White;
				Grid1.Background = Brushes.LightGreen;
			}

		}
		private void postaviFont()
		{
			foreach (Label l in ListaPolja)
			{
				l.FontSize = 30;
			}
		}
	}
}