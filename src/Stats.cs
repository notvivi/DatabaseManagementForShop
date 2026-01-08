using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbProjekt.src
{
    public class Stats
    {
        public int PocetArtefaktu { get; set; }
        public decimal Nejdrazsi { get; set; }
        public decimal Nejlevnejsi { get; set; }
        public int PocetObjednavek { get; set; }

        public override string ToString()
        {
            return
                $"Počet artefaktů v nabídce: {PocetArtefaktu}\n" +
                $"Nejdražší artefakt: {Nejdrazsi}\n" +
                $"Nejlevnější artefakt: {Nejlevnejsi}\n" +
                $"Počet objednávek: {PocetObjednavek}";
        }
    }
}
