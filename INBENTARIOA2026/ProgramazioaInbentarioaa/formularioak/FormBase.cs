using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbentarioa.formularioak
{
  
        public class FormBase : Form
        {
            public FormBase()
            {
                // Diseinatzailean ondo ikusteko eta tamaina aldatzean margotzeko
                this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                             ControlStyles.UserPaint |
                             ControlStyles.OptimizedDoubleBuffer |
                             ControlStyles.ResizeRedraw, true);
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                // Segurtasunagatik, area baduela ziurtatu
                if (this.ClientRectangle.Width <= 0 || this.ClientRectangle.Height <= 0) return;

                Color colorInicio = ColorTranslator.FromHtml("#C2CBED");
                Color colorFin = ColorTranslator.FromHtml("#003FA1");

                using (LinearGradientBrush brush = new LinearGradientBrush(
                    this.ClientRectangle,
                    colorInicio,
                    colorFin,
                    LinearGradientMode.Horizontal))
                {
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }
            }
        }
    
}
