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

namespace Seminarski_rad
{
	public class Engine
	{
		public static int DuzinaZmije = 2;
		public static int xZmije = 6;
		public static int yZmije = 6;
		static int brojac = 0;
		static bool hrana = false;
		Random rnd = new Random();
		public static int stanjeZmije = 0;
		public static bool daLiJede = false;
		public static bool walls = true;
		public Engine()
		{


		}

		public void ObojiPolje(int x, int y, Brush b)
		{
			MainWindow.Polja[x, y].Background = b;
			brojac += 1;
			MainWindow.pomocnaMatrica[x, y] = brojac;
		}

		public void PokreniEngine()
		{
			ObojiPolje(6, 4, Brushes.Blue);
			ObojiPolje(6, 5, Brushes.Blue);
			ObojiPolje(6, 6, Brushes.Red);
			SledecePolje.x = 5;
			SledecePolje.y = 5;

		}
		
		public void ObrisiPolje()
		{

			int min = Int32.MaxValue - 1;
			int xpomocni = 0;
			int ypomocni = 0;
			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 15; j++)
				{
					if (MainWindow.pomocnaMatrica[i, j] < min)
					{
						min = MainWindow.pomocnaMatrica[i, j];
						xpomocni = i;
						ypomocni = j;
					}
				}
			}
			if (daLiJede) //ukoliko zmija treba da poraste, polje nece biti obrisano
			{
				DuzinaZmije++;
				hrana = false;
			}
			else //ukoliko zmija ne treba da poraste, brise se poslednje polje zmije
			{
				if (MainWindow.darkTheme)
				{
					MainWindow.Polja[xpomocni, ypomocni].Background = new SolidColorBrush(Color.FromRgb(32, 32, 32));
				}
				else
				{
					MainWindow.Polja[xpomocni, ypomocni].Background = Brushes.White;
				}

				MainWindow.pomocnaMatrica[xpomocni, ypomocni] = Int32.MaxValue;
			}
		}

		public void OdrediSledecePolje()
		{
			MainWindow.Polja[SledecePolje.x, SledecePolje.y].Content = "";
			if (stanjeZmije == 1)
			{
				if (walls)
				{
					SledecePolje.x = Engine.xZmije - 1;
					SledecePolje.y = Engine.yZmije;
				}
				else
				{
					if (SledecePolje.x == 0)
					{
						SledecePolje.x = SledecePolje.x + 14;
						SledecePolje.y = Engine.yZmije;
					}
					else
					{
						SledecePolje.x = Engine.xZmije - 1;
						SledecePolje.y = Engine.yZmije;
					}
				}
				if((SledecePolje.x >= 0) & (SledecePolje.x <= 14) & (SledecePolje.y >= 0) & (SledecePolje.y <= 14))
					MainWindow.Polja[SledecePolje.x, SledecePolje.y].Content = "↑";
			}
			if (stanjeZmije == 2)
			{
				if (walls)
				{
					SledecePolje.x = Engine.xZmije;
					SledecePolje.y = Engine.yZmije - 1;
				}
				else
				{
					if (SledecePolje.y == 0)
					{
						SledecePolje.x = Engine.xZmije;
						SledecePolje.y = SledecePolje.y + 14;
					}

					else
					{
						SledecePolje.x = Engine.xZmije;
						SledecePolje.y = Engine.yZmije - 1;
					}
				}
				if ((SledecePolje.x >= 0) & (SledecePolje.x <= 14) & (SledecePolje.y >= 0) & (SledecePolje.y <= 14))
					MainWindow.Polja[SledecePolje.x, SledecePolje.y].Content = "←";
			}
			if (stanjeZmije == 3)
			{
				if (walls)
				{
					SledecePolje.x = Engine.xZmije + 1;
					SledecePolje.y = Engine.yZmije;
				}
				else
				{
						if (SledecePolje.x == 14)
						{
							SledecePolje.x = SledecePolje.x - 14;
							SledecePolje.y = Engine.yZmije;

						}
						else
						{
							SledecePolje.x = Engine.xZmije + 1;
							SledecePolje.y = Engine.yZmije;
						}
				}
				if ((SledecePolje.x >= 0) & (SledecePolje.x <= 14) & (SledecePolje.y >= 0) & (SledecePolje.y <= 14))
					MainWindow.Polja[SledecePolje.x, SledecePolje.y].Content = "↓";
			}
			if (stanjeZmije == 4)
			{
				if (walls)
				{
					SledecePolje.x = Engine.xZmije;
					SledecePolje.y = Engine.yZmije + 1;
				}
				else
				{
					if (SledecePolje.y == 14)
					{
						SledecePolje.x = Engine.xZmije;
						SledecePolje.y = SledecePolje.y - 14;
					}
					else
					{
						SledecePolje.x = Engine.xZmije;
						SledecePolje.y = Engine.yZmije + 1;
					}
				}
				if ((SledecePolje.x >= 0) & (SledecePolje.x <= 14) & (SledecePolje.y >= 0) & (SledecePolje.y <= 14))
					MainWindow.Polja[SledecePolje.x, SledecePolje.y].Content = "→";
			}

		}
		public void DaLiZmijaJede()
		{
			if (MainWindow.Polja[SledecePolje.x, SledecePolje.y].Background == Brushes.Yellow)
			{
				daLiJede = true;
			}
			else
				daLiJede = false;
		}


		public void PomeriZmiju()
		{
			if (stanjeZmije !=0)
			{
				MainWindow.Polja[xZmije, yZmije].Background = Brushes.Blue;
				xZmije = SledecePolje.x;
				yZmije = SledecePolje.y;
				ObojiPolje(xZmije, yZmije, Brushes.Red);
				ObrisiPolje();
			}

		}
		public void StvoriHranu()
		{
			while (hrana == false)
			{
				int x = rnd.Next(15);
				int y = rnd.Next(15);
				if ((MainWindow.Polja[x, y].Background != Brushes.Blue) & (MainWindow.Polja[x, y].Background != Brushes.Red))
				{
					MainWindow.Polja[x, y].Background = Brushes.Yellow;
					hrana = true;
				}
			}
		}
		public void AzurirajIgru()
		{
			OdrediSledecePolje();
			if (walls)
			{
				try
				{
					if ((SledecePolje.x >= 15) || (SledecePolje.y >= 15) || (SledecePolje.x < 0) || (SledecePolje.y < 0))
					{
						throw new IndexOutOfRangeException();
					}
					if (MainWindow.Polja[SledecePolje.x, SledecePolje.y].Background == Brushes.Blue)
					{
						throw new InvalidMoveException();
					}
					StvoriHranu();
					DaLiZmijaJede();
					PomeriZmiju();
				}
				catch (IndexOutOfRangeException)
				{
					MainWindow.tajmer.Enabled = false;
					MainWindow.gameOverLabelLista[0].Content = "GAME OVER";
					MessageBox.Show("Izasao si van table, pokusaj ponovo");
					RestartujIgru();
				}
				catch (InvalidMoveException)
				{
					MainWindow.tajmer.Enabled = false;
					MainWindow.gameOverLabelLista[0].Content = "GAME OVER";
					MessageBox.Show("Ujeo si se za rep, pokusaj ponovo");
					RestartujIgru();
				}
			}
			else
			{
				try
				{
					if (MainWindow.Polja[SledecePolje.x, SledecePolje.y].Background == Brushes.Blue)
					{
						throw new InvalidMoveException();
					}
					StvoriHranu();
					DaLiZmijaJede();
					PomeriZmiju();
				}
				catch (InvalidMoveException)
				{
					{
						MainWindow.tajmer.Enabled = false;
						MainWindow.gameOverLabelLista[0].Content = "GAME OVER";
						MessageBox.Show("Ujeo si se za rep, pokusaj ponovo");
						RestartujIgru();
					}
				}
			}
		}
		public void RestartujIgru()
		{
			DuzinaZmije = 2;
			MainWindow.interval = 0;
			MainWindow.raditajmer = false;
			MainWindow.brojcanik = 1;
			xZmije = 6;
			yZmije = 6;
			for (int i = 0; i < 15; i++)
			{
				for (int j = 0; j < 15; j++)
				{
					MainWindow.pomocnaMatrica[i, j] = Int32.MaxValue;
				}
			}
			SledecePolje.x = 0;
			SledecePolje.y = 0;
			hrana = false;
			MainWindow.nivoButtonOdabran = 0;
			if (MainWindow.darkTheme)
			{
				MainWindow.darkTheme = false;
				MainWindow.OfarbajTablu();
			}
			if (!MainWindow.darkTheme)
			{
				MainWindow.darkTheme = true;
				MainWindow.OfarbajTablu();
			}
			MainWindow.gameOverLabelLista[0].Content = "";
			foreach (Label l in MainWindow.ListaPolja)
			{
				l.Content = "";
			}
		}

	}
}
