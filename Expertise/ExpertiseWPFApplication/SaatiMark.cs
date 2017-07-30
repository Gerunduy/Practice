using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertiseWPFApplication
{
    public class SaatiMark
    {
        public SaatiMark(SaatiValue value)
        {
            this.value = value;         
            switch (this.value)
            {
                case SaatiValue.not_selected:
                    this.name_value = "Не выбранно";
                    this.name_color = System.Windows.Media.Brushes.DarkRed;
                    this.Mark = 0;
                    break;
                case SaatiValue.v9_Absolutely_superior:
                    this.name_value = "Абсолютно превосходит";
                    this.name_color = System.Windows.Media.Brushes.DarkRed;
                    this.Mark = 9;
                    break;
                case SaatiValue.v8_Intermediate_value:
                    this.name_value = "<Промежуточное значение>";
                    this.name_color = System.Windows.Media.Brushes.Crimson;
                    this.Mark = 8;
                    break;
                case SaatiValue.v7_Explicitly_exceeds:
                    this.name_value = "Явно превосходит";
                    this.name_color = System.Windows.Media.Brushes.Red;
                    this.Mark = 7;
                    break;
                case SaatiValue.v6_Intermediate_value:
                    this.name_value = "<Промежуточное значение>";
                    this.name_color = System.Windows.Media.Brushes.OrangeRed;
                    this.Mark = 6;
                    break;
                case SaatiValue.v5_Significantly_superior:
                    this.name_value = "Значительно превосходит";
                    this.name_color = System.Windows.Media.Brushes.Tomato;
                    this.Mark = 5;
                    break;
                case SaatiValue.v4_Intermediate_value:
                    this.name_value = "<Промежуточное значение>";
                    this.name_color = System.Windows.Media.Brushes.LightCoral;
                    this.Mark = 4;
                    break;
                case SaatiValue.v3_Slightly_superior:
                    this.name_value = "Незначительно превосходит";
                    this.name_color = System.Windows.Media.Brushes.PaleVioletRed;
                    this.Mark = 3;
                    break;
                case SaatiValue.v2_Intermediate_value:
                    this.name_value = "<Промежуточное значение>";
                    this.name_color = System.Windows.Media.Brushes.LightPink;
                    this.Mark = 2;
                    break;
                case SaatiValue.v1_Equivalent:
                    this.name_value = "Равнозначно";
                    this.name_color = System.Windows.Media.Brushes.Black;
                    this.Mark = 1;
                    break;
                case SaatiValue.v_2_Intermediate_value:
                    this.name_value = "<Промежуточное значение>";
                    this.name_color = System.Windows.Media.Brushes.LightBlue;
                    this.Mark = 0.5;
                    break;
                case SaatiValue.v_3_Slightly_inferior:
                    this.name_value = "Незначительно уступает";
                    this.name_color = System.Windows.Media.Brushes.LightSkyBlue;
                    this.Mark = 0.333;
                    break;
                case SaatiValue.v_4_Intermediate_value:
                    this.name_value = "<Промежуточное значение>";
                    this.name_color = System.Windows.Media.Brushes.DeepSkyBlue;
                    this.Mark = 0.25;
                    break;
                case SaatiValue.v_5_Significantly_inferior:
                    this.name_value = "Значительно уступает";
                    this.name_color = System.Windows.Media.Brushes.DodgerBlue;
                    this.Mark = 0.2;
                    break;
                case SaatiValue.v_6_Intermediate_value:
                    this.name_value = "<Промежуточное значение>";
                    this.name_color = System.Windows.Media.Brushes.RoyalBlue;
                    this.Mark = 0.166;
                    break;
                case SaatiValue.v_7_Obviously_inferior:
                    this.name_value = "Явно уступает";
                    this.name_color = System.Windows.Media.Brushes.Blue;
                    this.Mark = 0.142;
                    break;
                case SaatiValue.v_8_Intermediate_value:
                    this.name_value = "<Промежуточное значение>";
                    this.name_color = System.Windows.Media.Brushes.MidnightBlue;
                    this.Mark = 0.125;
                    break;
                case SaatiValue.v_9_Absolutely_inferior:
                    this.name_value = "Абсолютно уступает";
                    this.name_color = System.Windows.Media.Brushes.Navy;
                    this.Mark = 0.111;
                    break;
            }
        }

        public SaatiValue value { get; set; }
        public string name_value { get; set; }
        public System.Windows.Media.Brush name_color { get; set; }
        public double Mark { get; set; }
    }
    public enum SaatiValue
    {
        not_selected,
        v9_Absolutely_superior,
        v8_Intermediate_value,
        v7_Explicitly_exceeds,
        v6_Intermediate_value,
        v5_Significantly_superior,
        v4_Intermediate_value,
        v3_Slightly_superior,
        v2_Intermediate_value,
        v1_Equivalent,
        v_2_Intermediate_value,
        v_3_Slightly_inferior,
        v_4_Intermediate_value,
        v_5_Significantly_inferior,
        v_6_Intermediate_value,
        v_7_Obviously_inferior,
        v_8_Intermediate_value,
        v_9_Absolutely_inferior
    }
}
