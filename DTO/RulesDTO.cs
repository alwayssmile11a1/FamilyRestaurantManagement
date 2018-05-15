using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    class RulesDTO
    {
        public Decimal MaxDebtAmount { get; set; }
        public int MinStockBeforeImporting { get; set; }

        public RulesDTO()
        {
            MaxDebtAmount = 0;
            MinStockBeforeImporting = 0;

        }


        public RulesDTO(Decimal maxDebAmount, int minStockBeforeImporting)
        {
            MaxDebtAmount = maxDebAmount;
            MinStockBeforeImporting = minStockBeforeImporting;
        }

    }
}
