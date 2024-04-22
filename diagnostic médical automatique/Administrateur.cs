using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diagnostic_médical_automatique
{
    public class Administrateur:UtilisateurBase
    {

        private string poste {  get; set; }

        public Administrateur(int idPersonne, string nom, string prénom, DateOnly dateNaissance, string adresse, string poste, string Identifiant, string MotDePasse) : base(idPersonne, nom, prénom, dateNaissance, adresse, Identifiant, MotDePasse)
        {
            this.poste = poste;
        }

        public override string ToString()
        {
            return base.ToString() + $"Poste: {poste}";
        }


    }
}
