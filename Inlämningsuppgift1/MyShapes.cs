using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Inlämningsuppgift1
{
    class MyShapes
    {
        public List<ConnectionPoint> connectionPoints { get; set; }
        public Shape shape { get; set; }
        public TextBlock textBlock { get; set; }
        private List<Line> lines = new List<Line>();
        public List<Line> linesConnected
        {
            get
            {
                return lines;
            }
            set
            {
                lines = value;
            }
        }
        public int textBlockXRelationToShape { get; set; }
        public int textBlockYRelationToShape { get; set; }
        
    }
}
