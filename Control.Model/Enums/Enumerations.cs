using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.Enums
{
    public enum StatusInvoice
    {
        [Description("GERADA")]
        Gerada = 1
    }

    public enum CustomerType
    {
        [Description("FÍSICA")]
        Fisica = 1,
        [Description("JURÍDICA")]
        Juridica = 2
    }

    public enum ProviderType
    {
        [Description("FÍSICA")]
        Fisica = 1,
        [Description("JURÍDICA")]
        Juridica = 2
    }
}
