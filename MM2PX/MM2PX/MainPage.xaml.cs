using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using BRY;

namespace MM2PX
{
	public partial class MainPage : ContentPage
	{
		CMM2PX m_cmm = new CMM2PX();
		public MainPage()
		{
			InitializeComponent();

			btn00.Exec = BRY.MM2PX_EXEC.K00;
			btn01.Exec = BRY.MM2PX_EXEC.K01;
			btn02.Exec = BRY.MM2PX_EXEC.K02;
			btn03.Exec = BRY.MM2PX_EXEC.K03;
			btn04.Exec = BRY.MM2PX_EXEC.K04;
			btn05.Exec = BRY.MM2PX_EXEC.K05;
			btn06.Exec = BRY.MM2PX_EXEC.K06;
			btn07.Exec = BRY.MM2PX_EXEC.K07;
			btn08.Exec = BRY.MM2PX_EXEC.K08;
			btn09.Exec = BRY.MM2PX_EXEC.K09;

			btnDot.Exec = BRY.MM2PX_EXEC.DOT;
			btnBS.Exec = BRY.MM2PX_EXEC.BS;
			btnCL.Exec = BRY.MM2PX_EXEC.CL;
			btnMM.Exec = BRY.MM2PX_EXEC.MM;
			btnMMS.Exec = BRY.MM2PX_EXEC.MMS;
			btnDPI.Exec = BRY.MM2PX_EXEC.DPI;
			btnPX.Exec = BRY.MM2PX_EXEC.PX;

			btn00.Clicked += Btn_Clicked;
			btn01.Clicked += Btn_Clicked;
			btn02.Clicked += Btn_Clicked;
			btn03.Clicked += Btn_Clicked;
			btn04.Clicked += Btn_Clicked;
			btn05.Clicked += Btn_Clicked;
			btn06.Clicked += Btn_Clicked;
			btn07.Clicked += Btn_Clicked;
			btn08.Clicked += Btn_Clicked;
			btn09.Clicked += Btn_Clicked;
			btnDot.Clicked += Btn_Clicked;
			btnBS.Clicked += Btn_Clicked;
			btnCL.Clicked += Btn_Clicked;
			btnMM.Clicked += Btn_Clicked;
			btnMMS.Clicked += Btn_Clicked;
			btnDPI.Clicked += Btn_Clicked;
			btnPX.Clicked += Btn_Clicked;

			m_cmm.ValueChanged += M_cmm_ValueChanged;
			m_cmm.ModeChanged += M_cmm_ModeChanged;
			Disp();
			SetImode();
		}

		private void M_cmm_ModeChanged(object sender, EventArgs e)
		{
			SetImode();
		}
		public void SetImode()
		{
			MM2PX_IMODE im = m_cmm.IMODE;

			switch (im)
			{
				case MM2PX_IMODE.MM:
					btnMM.TextColor = Color.White;
					btnMMS.TextColor = Color.Black;
					btnDPI.TextColor = Color.Black;
					btnPX.TextColor = Color.Black;

					btnMM.BackgroundColor = Color.Gray;
					btnMMS.BackgroundColor = Color.LightGray;
					btnDPI.BackgroundColor = Color.LightGray;
					btnPX.BackgroundColor = Color.LightGray;

					tbMM.BackgroundColor = Color.FromRgb(0xff - 0x10, 0xff - 0x10, 0xff);
					tbMMS.BackgroundColor = Color.FromRgb(0xEA, 0xEA, 0xEA);
					tbDPI.BackgroundColor = Color.FromRgb(0xff, 0xff, 0xff);
					tbPX.BackgroundColor = Color.FromRgb(0xEA, 0xEA, 0xEA);

					break;
				case MM2PX_IMODE.MMS:
					btnMM.TextColor = Color.Black;
					btnMMS.TextColor = Color.White;
					btnDPI.TextColor = Color.Black;
					btnPX.TextColor = Color.Black;

					btnMM.BackgroundColor = Color.LightGray;
					btnMMS.BackgroundColor = Color.Gray;
					btnDPI.BackgroundColor = Color.LightGray;
					btnPX.BackgroundColor = Color.LightGray;
					tbMM.BackgroundColor = Color.FromRgb(0xff, 0xff, 0xff);
					tbMMS.BackgroundColor = Color.FromRgb(0xEA - 0x10, 0xEA - 0x10, 0xEA);
					tbDPI.BackgroundColor = Color.FromRgb(0xff, 0xff, 0xff);
					tbPX.BackgroundColor = Color.FromRgb(0xEA, 0xEA, 0xEA);

					break;
				case MM2PX_IMODE.DPI:
					btnMM.TextColor = Color.Black;
					btnMMS.TextColor = Color.Black;
					btnDPI.TextColor = Color.White;
					btnPX.TextColor = Color.Black;

					btnMM.BackgroundColor = Color.LightGray;
					btnMMS.BackgroundColor = Color.LightGray;
					btnDPI.BackgroundColor = Color.Gray;
					btnPX.BackgroundColor = Color.LightGray;
					tbMM.BackgroundColor = Color.FromRgb(0xff, 0xff, 0xff);
					tbMMS.BackgroundColor = Color.FromRgb(0xEA , 0xEA, 0xEA);
					tbDPI.BackgroundColor = Color.FromRgb(0xff - 0x10, 0xff - 0x10, 0xff);
					tbPX.BackgroundColor = Color.FromRgb(0xEA, 0xEA, 0xEA);
					break;
				case MM2PX_IMODE.PX:
					btnMM.TextColor = Color.Black;
					btnMMS.TextColor = Color.Black;
					btnDPI.TextColor = Color.Black;
					btnPX.TextColor = Color.White;

					btnMM.BackgroundColor = Color.LightGray;
					btnMMS.BackgroundColor = Color.LightGray;
					btnDPI.BackgroundColor = Color.LightGray;
					btnPX.BackgroundColor = Color.Gray;

					tbMM.BackgroundColor = Color.FromRgb(0xff, 0xff, 0xff);
					tbMMS.BackgroundColor = Color.FromRgb(0xEA, 0xEA, 0xEA);
					tbDPI.BackgroundColor = Color.FromRgb(0xff, 0xff, 0xff);
					tbPX.BackgroundColor = Color.FromRgb(0xEA - 0x10, 0xEA - 0x10, 0xEA);
					break;
			}
		}


		private void M_cmm_ValueChanged(object sender, EventArgs e)
		{
			Disp();
		}
		public void Disp()
		{
			tbMM.Text = m_cmm.MM;
			tbMMS.Text = m_cmm.MMS;
			tbDPI.Text = m_cmm.DPI;
			tbPX.Text = m_cmm.PX;
		}
		private void Btn_Clicked(object sender, EventArgs e)
		{
			CalcBtn b = (CalcBtn)sender;
			m_cmm.Exec(b.Exec);
		}
	}
}
