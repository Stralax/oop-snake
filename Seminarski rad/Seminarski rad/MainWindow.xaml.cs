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

	public partial class MainWindow : Window
	{
		private static Timer tajmer;
		public static List<Label> ListaPolja = new List<Label>();
		public static Label[,] Polja = new Label[15, 15];
		public static int[,] pomocnaMatrica = new int[15, 15];
		static Engine engine = new Engine();
		public static int stanjeZmije;
		public static bool daLiJede = false;
		static (int, int) sledecePolje = (0, 0);


		public MainWindow()
		{
			InitializeComponent();
			this.KeyDown += new KeyEventHandler(IKeyDown);

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
					pomocnaMatrica[i, j] = 1000;
					Polja[i, j] = ListaPolja[brojac];
					brojac++;
				}
			}
			Polja[10, 10].Background = Brushes.Yellow;
			Polja[14, 14].Background = Brushes.Yellow;
		}

		private void IKeyDown(object sender, KeyEventArgs e)
		{
			if ((e.Key == Key.W) || (e.Key == Key.Up))
				stanjeZmije = 1;
			if ((e.Key == Key.A) || (e.Key == Key.Left))
				stanjeZmije = 2;
			if ((e.Key == Key.S) || (e.Key == Key.Down))
				stanjeZmije = 3;
			if ((e.Key == Key.D) || (e.Key == Key.Right))
				stanjeZmije = 4;

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
				odrediSledecePolje();
				daLiZmijaJede();
				engine.pomeriZmiju();
			});
			/*this.Dispatcher.Invoke(() =>
			{
				if (daLiJede)
				{
					this.Dispatcher.Invoke(() =>
					{
						engine.povecajZmiju();
					});
				}
			});
			}

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
		private void odrediSledecePolje()
		{
			if (stanjeZmije == 1)
			{
				sledecePolje.Item1 = Engine.xZmije - 1;
				sledecePolje.Item2 = Engine.yZmije;
			}
			if (stanjeZmije == 2)
			{
				sledecePolje.Item1 = Engine.xZmije;
				sledecePolje.Item2 = Engine.yZmije - 1;
			}
			if (stanjeZmije == 3)
			{
				sledecePolje.Item1 = Engine.xZmije + 1;
				sledecePolje.Item2 = Engine.yZmije;
			}
			if (stanjeZmije == 4)
			{
				sledecePolje.Item1 = Engine.xZmije;
				sledecePolje.Item2 = Engine.yZmije + 1;
			}
		}
		public static void daLiZmijaJede()
		{
			if (Polja[sledecePolje.Item1, sledecePolje.Item2].Background == Brushes.Yellow)
			{
				daLiJede = true;
			}
			else
				daLiJede = false;
		}
	}
}