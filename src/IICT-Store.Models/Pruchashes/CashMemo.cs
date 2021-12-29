using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models.Pruchashes
{
    public class CashMemo : BaseModel
    {
        public int PurchashedId { get; set; }
        public Purchashed Purchashed { get; set; }
        public string ImageUrl { get; set; }
    }
}
