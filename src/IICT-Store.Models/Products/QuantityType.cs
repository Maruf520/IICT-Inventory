using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IICT_Store.Models.Products
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum QuantityType
    {
        Feet,
        Meter,
        Piece,
        Liter,
        Box,
        KG
    }
}
