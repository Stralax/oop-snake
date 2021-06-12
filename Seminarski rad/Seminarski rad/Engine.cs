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
		static int DuzinaZmije = 0;
		static int xZmije = 6;
		static int yZmije = 6;
		static int brojac = 0;
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

			MainWindow.Polja[xpomocni, ypomocni].Background = Brushes.White;
			MainWindow.pomocnaMatrica[xpomocni, ypomocni] = 1000;
		}


		public void pomeriZmiju()
		{
			if (MainWindow.stanjeZmije == 1)
			{
				obojiPolje(xZmije - 1, yZmije, Brushes.Red);
				xZmije -= 1;
				MainWindow.Polja[xZmije + 1, yZmije].Background = Brushes.Blue;
				obrisiPolje();
			}
			if (MainWindow.stanjeZmije == 2)
			{
				obojiPolje(xZmije, yZmije - 1, Brushes.Red);
				yZmije -= 1;
				MainWindow.Polja[xZmije, yZmije + 1].Background = Brushes.Blue;
				obrisiPolje();
			}
			if (MainWindow.stanjeZmije == 3)
			{
				obojiPolje(xZmije + 1, yZmije, Brushes.Red);
				xZmije += 1;
				MainWindow.Polja[xZmije - 1, yZmije].Background = Brushes.Blue;
				obrisiPolje();
			}
			if (MainWindow.stanjeZmije == 4)
			{
				obojiPolje(xZmije, yZmije + 1, Brushes.Red);
				yZmije += 1;
				MainWindow.Polja[xZmije, yZmije - 1].Background = Brushes.Blue;
				obrisiPolje();
			}

		}

	}
}
