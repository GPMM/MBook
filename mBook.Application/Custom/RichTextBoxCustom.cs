using System.Windows.Forms;

namespace MBook.Custom
{
    public partial class RichTextBoxCustom : RichTextBox
    {
        public RichTextBoxCustom()
        {
            InitializeComponent();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                //This makes the control's background transparent
                CreateParams CP = base.CreateParams;
                CP.ExStyle |= 0x20;
                return CP;
            }
        }
    }
}
