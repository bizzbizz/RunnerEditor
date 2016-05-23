using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RunnerWorldEditor
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			RunnerEngine.EndlessLevelGenerator.ConfigureDistricts(new RunnerEngine.District[] {
				new RunnerEngine.District
				{
					Background = 0,
					Houses = new []
					{
						new RunnerEngine.Objects.House(0, 7, RunnerEngine.Enums.CatLanes.DangerLane1, 2,2,2),
						new RunnerEngine.Objects.House(1, 7, RunnerEngine.Enums.CatLanes.DangerLane2, 2,2,2),
						new RunnerEngine.Objects.House(2, 7, RunnerEngine.Enums.CatLanes.DangerLane3, 2,2,2),
						new RunnerEngine.Objects.House(3, 7, RunnerEngine.Enums.CatLanes.DangerLane1, 2,2,2),
					}
				},
				new RunnerEngine.District
				{
					Background = 0,
					Houses = new []
					{
						new RunnerEngine.Objects.House(4, 7, RunnerEngine.Enums.CatLanes.DangerLane1, 2,2,2),
						new RunnerEngine.Objects.House(5, 7, RunnerEngine.Enums.CatLanes.DangerLane2, 2,2,2),
						new RunnerEngine.Objects.House(6, 7, RunnerEngine.Enums.CatLanes.DangerLane3, 2,2,2),
						new RunnerEngine.Objects.House(7, 7, RunnerEngine.Enums.CatLanes.DangerLane1, 2,2,2),
						new RunnerEngine.Objects.House(8, 7, RunnerEngine.Enums.CatLanes.DangerLane1, 2,2,2),
						new RunnerEngine.Objects.House(9, 7, RunnerEngine.Enums.CatLanes.DangerLane1, 2,2,2),
						new RunnerEngine.Objects.House(10, 7, RunnerEngine.Enums.CatLanes.DangerLane1, 2,2,2),
					}
				},
				new RunnerEngine.District
				{
					Background = 0,
					Houses = new []
					{
						new RunnerEngine.Objects.House(11, 7, RunnerEngine.Enums.CatLanes.None,2,2,2),
					}
				},
			});
		}
		int x = 0;
		private void button1_Click(object sender, EventArgs e)
		{
			var sec = RunnerEngine.EndlessLevelGenerator.GetNextSector(x * 12);
			if(sec.build!=null)
			foreach (var item in sec.build)
			{
				listBox2.Items.Add(item);
			}
			foreach (var item in sec.Children)
			{
				listBox1.Items.Add(item);
			}

			x++;
		}
	}
}
