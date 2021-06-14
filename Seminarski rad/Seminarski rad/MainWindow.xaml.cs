﻿using System;
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
		public static List<Label> ListaPolja = new List<Label>();
		public static Label[,] Polja = new Label[15, 15];
		public static int[,] pomocnaMatrica = new int[15, 15];
		static Engine engine = new Engine();
		public bool raditajmer=false;
		public int brojcanik=1;
		public static List<Button> nivoButtoni = new List<Button>();


		public MainWindow()
		{
			InitializeComponent();
			sledecePolje.x = 6;
			sledecePolje.y = 6;
			this.KeyDown += new KeyEventHandler(IKeyDown);
			nivoButtoni.Add(nivo1Button);
			nivoButtoni.Add(nivo2Button);
			nivoButtoni.Add(nivo3Button);
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
		}

		private void IKeyDown(object sender, KeyEventArgs e)
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

		static int interval;

		void SetTimer(int interval)
		{
			tajmer = new Timer(interval);
			tajmer.Elapsed += OnTimerElapsed;
			tajmer.AutoReset = true;
			tajmer.Enabled = true;
			raditajmer = true;
		}
		private void OnTimerElapsed(object sender, ElapsedEventArgs e)
		{
			this.Dispatcher.Invoke(() =>
			{
				engine.UpdateGame();
			});
			/*if (walls)
			{
				try
				{
					if ((sledecePolje.x >= 15) || (sledecePolje.y >= 15) || (sledecePolje.x < 0) || (sledecePolje.y < 0))
					{
						throw new IndexOutOfRangeException();
					}
					else
					{
						this.Dispatcher.Invoke(() =>
						{
							engine.spawnujHranu();
							engine.daLiZmijaJede();
							engine.pomeriZmiju();
						});
					}
				}
				catch (IndexOutOfRangeException)
				{
					tajmer.Enabled = false;
					MessageBox.Show("Izasao si van table");
				}
			}
			else
			{
				this.Dispatcher.Invoke(() =>
				{
					engine.odrediSledecePolje();
				});

			}

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
			pokusajPromenitiNivo(nivo1Button, 1000);

		}

		private void Nivo2Button_Click(object sender, RoutedEventArgs e)
		{
			pokusajPromenitiNivo(nivo2Button, 500);
		}

		private void Nivo3Button_Click(object sender, RoutedEventArgs e)
		{
			pokusajPromenitiNivo(nivo3Button, 300);
		}

		private void StartButton_Click(object sender, RoutedEventArgs e)
		{
			postaviFont();
			try
            {
                if (interval == 0)
                {
                    throw new LevelNotSelectedException();
                }

                if (brojcanik == 1)
                {
					SetTimer(interval);
					engine.PokreniEngine();
                    brojcanik++;
                }
				tajmer.Enabled = true;
                if (tajmer.Enabled)
                {
                    startButton.Content = "START";
					tajmer.Enabled = true;
				}
            }
            catch (LevelNotSelectedException)
            {
                MessageBox.Show("Nisi odabrao nivo!");
            }
            catch (ChangeLevelWhileIngameException)
            {
                MessageBox.Show("Ne moze se menjati nivo dok je igra u toku");
            }
            
			
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

		public void promeniBojuNivoButtona(Button b)
		{
			foreach (Button dugme in nivoButtoni)
			{
				if (dugme == b)
					dugme.Background = Brushes.Red;
				else
					dugme.Background = Brushes.White;
			}
		}
		public void pokusajPromenitiNivo(Button b, int vreme)
		{
			try
			{
				if (raditajmer == true)
				{
					throw new ChangeLevelWhileIngameException();
				}
				promeniBojuNivoButtona(b);
				interval = vreme;
			}
			catch (ChangeLevelWhileIngameException)
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
					throw new PausedWhileNotIngameException();
				}
				tajmer.Enabled = false;
				startButton.Content = "RESUME";
			}
			catch (PausedWhileNotIngameException)
			{
				MessageBox.Show("Ne mozes pauzirati igru dok je ne pokrenes!");
			}
		}	
	}
}