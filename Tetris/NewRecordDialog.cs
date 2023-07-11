
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
	public partial class NewRecordDialog : Form
	{
		public string UserName {get; set;}
		public NewRecordDialog()
		{
			InitializeComponent();
			if(UserName!="")
				UNameTextBox.Text=UserName;
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			UNameTextBox.Text=UNameTextBox.Text.Trim(' ');
			if(UNameTextBox.Text=="") return;
			UserName=UNameTextBox.Text;
			DialogResult=DialogResult.OK;
		}
	}
}
