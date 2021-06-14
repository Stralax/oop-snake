using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

		/*public void povecajZmiju() TRENUTNO SUVISNA FUNKCIJA, obrisiPolje() SE BAVI I RASTOM ZMIJE
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

			MainWindow.Polja[xpomocni, ypomocni].Background = Brushes.Blue;
			DuzinaZmije++;
			MainWindow.pomocnaMatrica[xpomocni, ypomocni] = min - 1;
		}
		*/
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
			if (MainWindow.daLiJede) //ukoliko zmija treba da poraste, polje nece biti obrisano
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

		public void pomeriZmiju()
		{
			if (MainWindow.stanjeZmije == 1)
			{
				xZmije -= 1;
				obojiPolje(xZmije, yZmije, Brushes.Red);
				MainWindow.Polja[xZmije + 1, yZmije].Background = Brushes.Blue;
				obrisiPolje();
			}
			if (MainWindow.stanjeZmije == 2)
			{
				yZmije -= 1;
				obojiPolje(xZmije, yZmije, Brushes.Red);
				MainWindow.Polja[xZmije, yZmije + 1].Background = Brushes.Blue;
				obrisiPolje();
			}
			if (MainWindow.stanjeZmije == 3)
			{
				xZmije += 1;
				obojiPolje(xZmije, yZmije, Brushes.Red);
				MainWindow.Polja[xZmije - 1, yZmije].Background = Brushes.Blue;
				obrisiPolje();
			}
			if (MainWindow.stanjeZmije == 4)
			{
				yZmije += 1;
				obojiPolje(xZmije, yZmije, Brushes.Red);
				MainWindow.Polja[xZmije, yZmije - 1].Background = Brushes.Blue;
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

	}
}
