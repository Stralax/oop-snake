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
		static int DuzinaZmije = 3;
		public Engine()
		{
			

		}
		public void PokreniEngine()
		{
			MainWindow.Polja[6, 6].Background = Brushes.Red;
			MainWindow.Polja[6, 6].Content = "=>";
			MainWindow.Polja[6, 5].Background = Brushes.Blue;
			MainWindow.Polja[6, 4].Background = Brushes.Blue;
		}
		public void OrijentisiZmiju()
		{ 
			
		}

	}
}
