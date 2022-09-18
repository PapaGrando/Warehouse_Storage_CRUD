using Storage.Core.Models.Storage;
using System.Collections.Generic;
using System.Linq;

namespace WarehouseCRUD.Storage.Helpers
{
    public static class AreaHelper
    {
        /// <summary>
        /// Хелпер, который помогает выставить Номера подзон(стеллажей) по позиции в передаваемом списке. Отсчет от 1
        /// </summary>
        public static void SetSubAreaNoByListPos(IList<SubArea> list)
        {
            if (!list.Any())
                return;

            var numerator = 1;

            foreach (var subArea in list)
                subArea.NoOfSubArea = numerator++;
        }
    }
}
