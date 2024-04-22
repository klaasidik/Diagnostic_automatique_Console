using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace diagnostic_médical_automatique
{
    public class Noeud
    {
        public string critere { get; set; }
        public double? valeur { get; set; }
        public Dictionary<string, Noeud> enfants { get; set; }
        public string prediction { get; set; }
        public bool estFeuille { get; set; }

        public override string? ToString()
        {
            return $"critere {critere}, valeur: {valeur}, prediction: {prediction}, estFeuille: {estFeuille}";
        }


    }
}
