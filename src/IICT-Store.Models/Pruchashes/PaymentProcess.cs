﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IICT_Store.Models.Pruchashes
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentProcess
    {
        Direct,
        RFQ,
        OTM
    }
}
