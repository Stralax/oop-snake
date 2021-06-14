using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Seminarski_rad
{
	public class Engine
	{
		static int DuzinaZmije = 2;
		public static int xZmije = 6;
		public static int yZmije = 6;
		static int brojac = 0;
		static bool hrana = false;
		Random rnd = new Random();
		public static int stanjeZmije = 0;
		public static bool daLiJede = false;
		public bool walls = false;
		public Engine()
		{


		}

		public void obojiPolje(int x, int y, Brush b)
		{
			MainWindow.Polja[x, y].Background = b;
			brojac += 1;
			MainWindow.pomocnaMatrica[x, y] = brojac;
		}

		public void PokreniEngine()
		{
			obojiPolje(6, 4, Brushes.Blue);
			obojiPolje(6, 5, Brushes.Blue);
			obojiPolje(6, 6, Brushes.Red);

			//MainWindow.Polja[6, 6].Content = "=>";
		}

		public void obrisiPolje()
		{

			int min = 500;
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
				MainWindow.Polja[xpomocni, ypomocni].Background = Brushes.Blue;
				DuzinaZmije++;
				MainWindow.pomocnaMatrica[xpomocni, ypomocni] = min - 1;
				hrana = false;
			}
			else //ukoliko zmija ne treba da poraste, brise se poslednje polje zmije
			{
				MainWindow.Polja[xpomocni, ypomocni].Background = Brushes.White;
				MainWindow.pomocnaMatrica[xpomocni, ypomocni] = 1000;
			}
		}

		public void odrediSledecePolje()
		{
			if (stanjeZmije == 1)
			{
				if (walls)
				{
					sledecePolje.x = Engine.xZmije - 1;
					sledecePolje.y = Engine.yZmije;
				}
				else
				{
					if (sledecePolje.x == 0)
					{
						sledecePolje.x = 15;
						sledecePolje.y = Engine.yZmije;
					}
					else
					{
						sledecePolje.x = Engine.xZmije - 1;
						sledecePolje.y = Engine.yZmije;
					}
				}
			}
			if (stanjeZmije == 2)
			{
				if (walls)
				{
					sledecePolje.x = Engine.xZmije;
					sledecePolje.y = Engine.yZmije - 1;
				}
				else
				{
					if (sledecePolje.y == 0)
					{
						sledecePolje.x = Engine.xZmije;
						sledecePolje.y = 15;
					}

					else
					{
						sledecePolje.x = Engine.xZmije;
						sledecePolje.y = Engine.yZmije - 1;
					}
				}
			}
			if (stanjeZmije == 3)
			{
				if (walls)
				{
					sledecePolje.x = Engine.xZmije + 1;
					sledecePolje.y = Engine.yZmije;
				}
				else
				{
						if (sledecePolje.x == 15)
						{
							sledecePolje.x = 0;
							sledecePolje.y = Engine.yZmije;
						}
						else
						{
							sledecePolje.x = Engine.xZmije + 1;
							sledecePolje.y = Engine.yZmije;
						}
				}
			}
			if (stanjeZmije == 4)
			{
				if (walls)
				{
					sledecePolje.x = Engine.xZmije;
					sledecePolje.y = Engine.yZmije + 1;
				}
				else
				{
					if (sledecePolje.y == 15)
					{
						sledecePolje.x = Engine.xZmije;
						sledecePolje.y = 0;
					}
					else
					{
						sledecePolje.x = Engine.xZmije;
						sledecePolje.y = Engine.yZmije + 1;
					}
				}
			}
		}
		public void daLiZmijaJede()
		{
			if (MainWindow.Polja[sledecePolje.x, sledecePolje.y].Background == Brushes.Yellow)
			{
				daLiJede = true;
			}
			else
				daLiJede = false;
		}


		public void pomeriZmiju()
		{
			if (stanjeZmije !=0)
			{
				MainWindow.Polja[xZmije, yZmije].Background = Brushes.Blue;
				xZmije = sledecePolje.x;
				yZmije = sledecePolje.y;
				obojiPolje(xZmije, yZmije, Brushes.Red);
				obrisiPolje();
			}

		}
		public void spawnujHranu()
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
		public void UpdateGame()
		{
			odrediSledecePolje();
			if (walls)
			{
				try
				{
					if ((sledecePolje.x >= 15) || (sledecePolje.y >= 15) || (sledecePolje.x < 0) || (sledecePolje.y < 0))
					{
						throw new IndexOutOfRangeException();
					}
					spawnujHranu();
					daLiZmijaJede();
					pomeriZmiju();
				}
				catch (IndexOutOfRangeException)
				{
					MainWindow.tajmer.Enabled = false;
					MessageBox.Show("Izasao si van table");
				}
			}
			else
			{
				spawnujHranu();
				daLiZmijaJede();
				pomeriZmiju();
			}
		}

	}
}
