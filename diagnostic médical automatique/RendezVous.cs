using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diagnostic_médical_automatique
{
    public class RendezVous
    {
        public int idRendezVous {  get; set; }
        public DateTime dateHeure { get; set; }
        public string motif {  get; set; }

        public RendezVous(int idRendezVous, DateTime dateHeure, string motif)
        {
            this.idRendezVous = idRendezVous;
            this.dateHeure = dateHeure;
            this.motif = motif;
        }
        public override string ToString()
        {
            return $"RendezVous ID: {idRendezVous}, Date: {dateHeure}, Motif: {motif}";
        }
    }
}
