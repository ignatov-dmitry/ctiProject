using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logics
{
    public static class Charts
    {
        public static string BackgroundColor(int i)
        {
            string[] backgroundColor = {
                "rgba(128,75,34,.4)",
                "rgba(200,55,8,.4)",
                "rgba(0,70,8,.4)",
                "rgba(0,70,70,.4)",
                "rgba(80,70,70,.4)",
                "rgba(80,70,255,.4)",
                "rgba(79,150,4,.4)",
                "rgba(8,180,5,.4)",
                "rgba(134,180,15,.4)",
                "rgba(170,25,15,.4)",
                "rgba(254,255,0,.4)"
            };

            return backgroundColor[i];
        }

        public static string Colors(int i)
        {
            string[] COLORS = {
                "#4dc9f6",
                "#f67019",
                "#f53794",
                "#537bc4",
                "#acc236",
                "#166a8f",
                "#00a950",
                "#58595b",
                "#8549ba",
                "#4852f6",
                "#df5019",
                "#f587fd",
                "#537bc4",
                "#acc236",
                "#166a8f",
                "#005240",
                "#585482",
                "#854dd2"
            };

            return COLORS[i];
        }

    }
}
