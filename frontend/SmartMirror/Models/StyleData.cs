using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirror.Models
{
    public class StyleData
    {
        public int styleId { get; set; }
        public List<GoodsOptionData> goodsOptionsData { get; set; }
        public string makeupImage { get; set; }
    }
}
