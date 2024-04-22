using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diagnostic_médical_automatique
{
    public class Patient:Personne
    {

        private string groupeSainguin { get; set; }
        public string assurance { get; private set; }



        public Patient(int idPersonne, string nom, string prénom, DateOnly dateNaissance, string adresse , string groupeSainguin, string assurance) : base(idPersonne, nom, prénom, dateNaissance, adresse)
        {
            this.groupeSainguin = groupeSainguin;
            this.assurance = assurance;
        }

        public override string ToString()
        {
     
            return base.ToString() + $" Groupe Sanguin: {groupeSainguin}, Assurance: {assurance}";
        }
    }
}
