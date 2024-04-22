using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diagnostic_médical_automatique
{
    public class Traitement
    {
        public int idTraitement {  get; set; }
        public string nomTraitement {  set; get; }
        public string descriptionTraitement { set; get; }
        private string durée {  set; get; }
        private string effetSecondaire { set; get; }

        public Traitement(int idTraitement, string nomTraitement, string descriptionTraitement, string durée, string effetSecondaire)
        {
            this.idTraitement = idTraitement;
            this.nomTraitement = nomTraitement;
            this.descriptionTraitement = descriptionTraitement;
            this.durée = durée;
            this.effetSecondaire = effetSecondaire;
        }

        public Traitement()
        {
        }

        public override string? ToString()
        {

            return $"Traitement ID: {idTraitement}, Nom: {nomTraitement}, Description: {descriptionTraitement}, Durée: {durée}, Effets secondaires: {effetSecondaire}";
        }
    }
}
