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
		public static Timer tajmer;
		private static bool tajmerPostoji = false;
		public static List<Label> ListaPolja = new List<Label>();
		public static Label[,] Polja = new Label[15, 15];
		public static int[,] pomocnaMatrica = new int[15, 15];
		static Engine engine = new Engine();
		public static bool raditajmer = false;
		public static int brojcanik = 1;
		public static List<Button> nivoButtoni = new List<Button>();
		public static bool darkTheme = false;
		public static int nivoButtonOdabran = 0;
		//sledece tri liste su potrebne za staticku funkciju
		private static List<Grid> gridLista= new List<Grid>();
		private static List<Button> ThemeButtonLista = new List<Button>();
		public static List<Label> gameOverLabelLista = new List<Label>();



		public MainWindow()
		{
			InitializeComponent();
			this.KeyDown += new KeyEventHandler(IKeyDown);
			nivoButtoni.Add(nivo1Button);
			nivoButtoni.Add(nivo2Button);
			nivoButtoni.Add(nivo3Button);
			gridLista.Add(Grid1);
			ThemeButtonLista.Add(ThemeButton);
			gameOverLabelLista.Add(gameOverLabel);
			foreach (var v in Grid1.Children)
			{
				if (v is Button)
				{
					Button pomocni = (Button)v;
					pomocni.Background = new SolidColorBrush(Color.FromRgb(224, 224, 224));
				}
			}
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
					pomocnaMatrica[i, j] = Int32.MaxValue;
					Polja[i, j] = ListaPolja[brojac];
					brojac++;
				}
			}
			postaviFont();
			gameOverLabel.HorizontalContentAlignment = HorizontalAlignment.Center;
		}

		private void IKeyDown(object sender, KeyEventArgs e)
		{
			if (tajmerPostoji)
			{
				if (tajmer.Enabled)
				{
					if ((e.Key == Key.W) || (e.Key == Key.Up))
						Engine.stanjeZmije = 1;
					if ((e.Key == Key.A) || (e.Key == Key.Left))
						Engine.stanjeZmije = 2;
					if ((e.Key == Key.S) || (e.Key == Key.Down))
						Engine.stanjeZmije = 3;
					if ((e.Key == Key.D) || (e.Key == Key.Right))
						Engine.stanjeZmije = 4;
				}
				if (e.Key == Key.P)
				{
					if (tajmer.Enabled)
					{
						tajmer.Enabled = false;
						startButton.Content = "RESUME";
					}
					else
					{
						tajmer.Enabled = true;
						startButton.Content = "START";
					}
				}
			}
		}


		public static int interval;

		void SetTimer(int interval)
		{
			tajmer = new Timer(interval);
			tajmer.Elapsed += OnTimerElapsed;
			tajmer.AutoReset = true;
			tajmer.Enabled = true;
			raditajmer = true;
			tajmerPostoji = true;
		}
		private void OnTimerElapsed(object sender, ElapsedEventArgs e)
		{
			this.Dispatcher.Invoke(() =>
			{
				engine.AzurirajIgru();
				Polje000.Content = Engine.DuzinaZmije;
			});
		}
		private void Nivo1Button_Click(object sender, RoutedEventArgs e)
		{
			pokusajPromenitiNivo(nivo1Button, 250);

		}

		private void Nivo2Button_Click(object sender, RoutedEventArgs e)
		{
			pokusajPromenitiNivo(nivo2Button, 175);
		}

		private void Nivo3Button_Click(object sender, RoutedEventArgs e)
		{
			pokusajPromenitiNivo(nivo3Button, 100);
		}

		private void StartButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (interval == 0)
				{
					throw new OptionNotSelectedException();
				}

				if (brojcanik == 1)
				{
					SetTimer(interval);
					engine.PokreniEngine();
					Engine.stanjeZmije = 0;
					brojcanik++;
				}
				tajmer.Enabled = true;
				if (tajmer.Enabled)
				{
					startButton.Content = "START";
					tajmer.Enabled = true;
				}
			}
			catch (OptionNotSelectedException)
			{
				MessageBox.Show("Nisi odabrao nivo!");
			}

		}

		private void postaviFont()
		{
			foreach (Label l in ListaPolja)
			{
				l.FontSize = 25;
				l.HorizontalContentAlignment = HorizontalAlignment.Center;
			}
		}

		public void promeniBojuNivoButtona(Button b)
		{
			foreach (Button dugme in nivoButtoni)
			{
				if (dugme == b)
				{
					dugme.Background = Brushes.Red;
					nivoButtonOdabran = nivoButtoni.IndexOf(b) + 1;
				}
				else
				{
					if (darkTheme)
						dugme.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
					else
						dugme.Background = new SolidColorBrush(Color.FromRgb(224, 224, 224));

				}
			}
		}
		public void pokusajPromenitiNivo(Button b, int vreme)
		{
			try
			{
				if (raditajmer == true)
				{
					throw new AlreadyIngameException();
				}
				promeniBojuNivoButtona(b);
				interval = vreme;
			}
			catch (AlreadyIngameException)
			{
				tajmer.Enabled = false;
				startButton.Content = "RESUME";
				MessageBox.Show("Ne moze se menjati nivo dok je igra u toku");
			}
		}
		private void pauseButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (raditajmer == false)
				{
					throw new NotIngameException();
				}
				tajmer.Enabled = false;
				startButton.Content = "RESUME";
			}
			catch (NotIngameException)
			{
				MessageBox.Show("Ne mozes pauzirati igru dok je ne pokrenes!");
			}
		}

		private void modeButton_Click(object sender, RoutedEventArgs e)
		{

			try
			{
				if (raditajmer == true)
				{
					throw new AlreadyIngameException();
				}
				if (Engine.walls)
				{
					Engine.walls = false;
					modeButton.Content = "Mode: No Walls";
				}
				else
				{
					Engine.walls = true;
					modeButton.Content = "Mode: Walls";
				}
			}
			catch (AlreadyIngameException)
			{
				tajmer.Enabled = false;
				startButton.Content = "RESUME";
				MessageBox.Show("Ne mozes menjati mod igre dok si u igri");
			}



		}

		public static void OfarbajTablu()
		{
			if (darkTheme == false)
			{
				Polja[0,0].Foreground = Brushes.White;
				darkTheme = true;
				ThemeButtonLista[0].Content = "Theme: Dark";
				foreach (Label l in ListaPolja)
				{
					l.Background = new SolidColorBrush(Color.FromRgb(32, 32, 32));

				}
				gridLista[0].Background = new SolidColorBrush(Color.FromRgb(64, 64, 64));
				foreach (var b in gridLista[0].Children)
				{
					if (b is Button)
					{
						Button pomocni = (Button)b;
						if (nivoButtonOdabran == 0)
						{
							pomocni.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
						}
						else
						{
							if (nivoButtoni[nivoButtonOdabran - 1] == b)
							{
								pomocni.Background = Brushes.Red;
							}
							else
							{
								pomocni.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
							}
						}
					}
				}
			}
			else
			{
				Polja[0,0].Foreground = Brushes.Black;
				darkTheme = false;
				ThemeButtonLista[0].Content = "Theme: Light";
				foreach (Label l in ListaPolja)
				{
					l.Background = Brushes.White;
				}
				gridLista[0].Background = Brushes.LightGreen;
				foreach (var b in gridLista[0].Children)
				{
					if (b is Button)
					{
						Button pomocni = (Button)b;
						if (nivoButtonOdabran == 0)
						{
							pomocni.Background = new SolidColorBrush(Color.FromRgb(224, 224, 224));
						}
						else
						{
							if (nivoButtoni[nivoButtonOdabran - 1] == b)
							{
								pomocni.Background = Brushes.Red;
							}
							else
							{
								pomocni.Background = new SolidColorBrush(Color.FromRgb(224, 224, 224));
							}
						}
					}
				}
			}
		}

		private void ThemeButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (raditajmer == true)
					throw new AlreadyIngameException();
				OfarbajTablu();

			}
			catch (AlreadyIngameException)
			{
				tajmer.Enabled = false;
				startButton.Content = "RESUME";
				MessageBox.Show("Ne mozes menjati temu dok si u igri");
			}

		}
	}
}