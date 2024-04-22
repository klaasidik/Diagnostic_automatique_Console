using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diagnostic_médical_automatique
{
    public abstract class UtilisateurBase : Personne, IUtilisateur
    {
        public string identifiant { get; set; }
        public string motDePasse { get; set; }
        public List<DateTime> HistoriqueConnexions { get; private set; } = new List<DateTime>();
        protected UtilisateurBase(int idPersonne, string nom, string prénom, DateOnly dateNaissance, string adresse, string Identifiant, string MotDePasse) : base(idPersonne, nom, prénom, dateNaissance, adresse)
        {
            this.identifiant=Identifiant;
            this.motDePasse=MotDePasse;
        }
        public void SeConnecter()
        {
            Console.WriteLine($"Utilisateur {nom} {prénom} est connecté");
            HistoriqueConnexions.Add(DateTime.Now);
        }
        public  void SeDeconnecter()
        {
            Console.WriteLine($"Utilisateur {nom} {prénom} est deconnecté");
        }

        public override string? ToString()
        {
            return base.ToString() + $"  Identifiant: {identifiant}";
        }
    }
}
