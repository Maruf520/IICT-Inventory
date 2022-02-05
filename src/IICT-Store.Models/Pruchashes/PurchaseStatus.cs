using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IICT_Store.Models.Pruchashes
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PurchaseStatus
    {
        [Display(Name = "Pending")]
        Pending,
        [Display(Name = "Rejected")]
        Rejected,
        [Display(Name = "Confirmed")]
        Confirmed
    }
}
