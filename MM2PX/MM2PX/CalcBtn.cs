using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
namespace BRY

{
	public class CalcBtn : Xamarin.Forms.Button
	{
		private MM2PX_EXEC m_Exec = MM2PX_EXEC.NONE;
		public MM2PX_EXEC Exec
		{
			get { return m_Exec; }
			set
			{
				m_Exec = value;
			}
		}
		public CalcBtn()
		{
			this.FontSize = 24;
			this.Margin = 0;
			this.BorderWidth = 1;
			this.Padding = 0;
			this.BorderColor = Color.Black;
			this.BackgroundColor = Color.White;
		}
	}
}
