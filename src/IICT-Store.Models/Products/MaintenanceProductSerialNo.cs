using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Models.Products
{
    public class MaintenanceProductSerialNo : BaseModel
    {
        public string Name { get; set; }
        public long ProductNoId { get; set; }
        public bool IsRepaired { get; set; }
        public long MaintananceProductId { get; set; }
    }
}
