using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMirror.Models
{
    public class GoodsOptionData
    {
        public int optionId { get; set; }
        public string goodsName { get; set; }
        public string optionName { get; set; }
        public string optionImage { get; set; }
        public bool isInMarket { get; set; }
        public string location { get; set; }
        public int stock { get; set; }
    }
}
