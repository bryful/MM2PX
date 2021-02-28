using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BRY
{
	public enum MM2PX_IMODE
	{
		MM,
		MMS,
		DPI,
		PX
	}
	public enum MM2PX_EXEC
	{
		NONE = -1,
		K00 = 0,
		K01,
		K02,
		K03,
		K04,
		K05,
		K06,
		K07,
		K08,
		K09,
		DOT,
		BS,
		CL,
		MM,
		MMS,
		DPI,
		PX
	}

	// **************************************************
	public class CMM2PX
	{
		public static readonly string[] EXE_TEXT = new string[]
		{
			"0",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8",
			"9",
			".",
			"BS",
			"CL",
			"MM",
			"MMs",
			"DPI",
			"PX"
		};
		#region Event
		public event EventHandler ModeChanged;
		protected virtual void OnModeChanged(EventArgs e)
		{
			if (ModeChanged != null)
			{
				ModeChanged(this, e);
			}
		}
		public event EventHandler ValueChanged;
		protected virtual void OnValueChanged(EventArgs e)
		{
			if (ValueChanged != null)
			{
				ValueChanged(this, e);
			}
		}
		#endregion

		private MM2PX_IMODE m_imode = MM2PX_IMODE.MM;
		public MM2PX_IMODE IMODE
		{
			get { return m_imode; }
		}
		private double m_mm = 0;
		public string MM
		{
			get 
			{
				if (m_imode == MM2PX_IMODE.MM)
				{
					return m_InputStr;
				}
				else
				{
					return string.Format("{0}", m_mm);
				}
			}
		}
		private int m_mms = 1;
		public string MMS
		{
			get 
			{
				if (m_imode == MM2PX_IMODE.MMS)
				{
					return m_InputStr;
				}
				else
				{
					return string.Format("{0}", m_mms);
				}
			}
		}
		private double m_dpi = 144;
		public string DPI
		{
			get 
			{
				if (m_imode == MM2PX_IMODE.DPI)
				{
					return m_InputStr;
				}
				else
				{
					return string.Format("{0}", m_dpi);
				}
			}
		}
		private double m_px = 0;
		public string PX
		{
			get
			{
				if (m_imode == MM2PX_IMODE.PX)
				{
					return m_InputStr;
				}
				else
				{
					return string.Format("{0}", m_px);
				}
			}
		}

		private string m_InputStr = "";
		// **************************************************
		public CMM2PX ()
		{
			m_imode = MM2PX_IMODE.MM;
			Clear();
		}
		// **************************************************
		public void Clear()
		{
			if (m_imode == MM2PX_IMODE.MMS)
			{
				m_mms = 1;
			}
			if (m_imode == MM2PX_IMODE.DPI)
			{
				m_dpi = 144;
			}
			else
			{
				m_mm = 0;
				m_px = 0;
				m_InputStr = "0";
			}
			OnValueChanged(new EventArgs());
		}
		// **************************************************
		public void SetIModeMM(){ SetIMode(MM2PX_IMODE.MM); }
		public void SetIModeMMS() { SetIMode(MM2PX_IMODE.MMS); }
		public void SetIModeDPI() { SetIMode(MM2PX_IMODE.DPI); }
		public void SetIModePX() { SetIMode(MM2PX_IMODE.PX); }
		public void SetIMode(MM2PX_IMODE im)
		{
			if (m_imode != im)
			{
				m_imode = im;
			}
			ToInputStr();
			OnModeChanged(new EventArgs());
		}
		// **************************************************
		public void InputBS()
		{
			if (m_InputStr != "")
			{
				m_InputStr = m_InputStr.Substring(0, m_InputStr.Length - 1);

			}
			FromInputStr();
			calc();
		}
		// **************************************************
		public void InputDot()
		{
			if (m_imode== MM2PX_IMODE.MMS)
			{
				return;
			}else if ((m_InputStr == "0")|| (m_InputStr == ""))
			{
				m_InputStr = "0.";
			}
			else
			{
				int idx = m_InputStr.IndexOf(".");
				if (idx<0)
				{
					m_InputStr += ".";
				}
			}
			FromInputStr();
			calc();
		}
		// **************************************************
		public void InputNum(MM2PX_EXEC exec)
		{
			if( (exec>= MM2PX_EXEC.K00)&&(exec <= MM2PX_EXEC.K09))
			{
				string c = string.Format("{0}", (int)exec);
				if (m_InputStr == "0")
				{
					m_InputStr = c;
				}
				else
				{
					m_InputStr += c;
				}
				FromInputStr();
				calc();
			}
		}
		// **************************************************
		private void ToInputStr()
		{
			double v = 0;
			switch (m_imode)
			{
				case MM2PX_IMODE.MM:
					v = m_mm;
					break;
				case MM2PX_IMODE.MMS:
					v = (int)m_mms;
					break;
				case MM2PX_IMODE.DPI:
					v = m_dpi;
					break;
				case MM2PX_IMODE.PX:
					v = m_px;
					break;
			}
			m_InputStr = string.Format("{0}", v);

		}
		// **************************************************
		private void FromInputStr()
		{
			double v = 0;
			if (m_InputStr != "")
			{
				double vv = 0;
				if (double.TryParse(m_InputStr,out vv))
				{
					v = vv;
				}
			}

			switch (m_imode)
			{
				case MM2PX_IMODE.MM:
					m_mm = v;
					break;
				case MM2PX_IMODE.MMS:
					m_mms = (int)v;
					break;
				case MM2PX_IMODE.DPI:
					m_dpi = v;
					break;
				case MM2PX_IMODE.PX:
					m_px = v;
					break;
			}

		}
		public void calc()
		{
			double v = 0;
			switch (m_imode)
			{
				case MM2PX_IMODE.MM:
				case MM2PX_IMODE.MMS:
				case MM2PX_IMODE.DPI:
					v = mm2pp(m_mm * (double)m_mms, m_dpi);
					v = (double)((int)(v * 10000 + 0.5)) / 10000;
					m_px = v;

					OnValueChanged(new EventArgs());
					break;
				case MM2PX_IMODE.PX:
					if (m_mms <= 0)
					{
						v = 0;
					}
					else
					{
						v = pp2mm(m_px , m_dpi) / (double)m_mms;
						v = (double)((int)(v * 10000 + 0.5)) / 10000;
					}
					m_mm = v;
					OnValueChanged(new EventArgs());
					break;
			}

		}
		static public double pp2mm(double p, double dpi = 300)
		{
			if (dpi <= 0) dpi = 1;
			return (double)(p * 25.4 / dpi);
		}
		static public double mm2pp(double m, double dpi = 300)
		{
			return (double)((m * dpi / 25.4));
		}
		public void Exec(MM2PX_EXEC exec)
		{
			switch(exec)
			{
				case MM2PX_EXEC.K00:
				case MM2PX_EXEC.K01:
				case MM2PX_EXEC.K02:
				case MM2PX_EXEC.K03:
				case MM2PX_EXEC.K04:
				case MM2PX_EXEC.K05:
				case MM2PX_EXEC.K06:
				case MM2PX_EXEC.K07:
				case MM2PX_EXEC.K08:
				case MM2PX_EXEC.K09:
					InputNum(exec);
					break;
				case MM2PX_EXEC.DOT:
					InputDot();
					break;
				case MM2PX_EXEC.BS:
					InputBS();
					break;
				case MM2PX_EXEC.CL:
					Clear();
					break;
				case MM2PX_EXEC.MM:
					SetIModeMM();
					break;
				case MM2PX_EXEC.MMS:
					SetIModeMMS();
					break;
				case MM2PX_EXEC.DPI:
					SetIModeDPI();
					break;
				case MM2PX_EXEC.PX:
					SetIModePX();
					break;
			}
		}
	}
	// **************************************************
}
