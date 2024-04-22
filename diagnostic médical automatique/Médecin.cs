using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diagnostic_médical_automatique
{
    public  class Médecin:UtilisateurBase
    {
        public string spécialité {  get; set; }
        public string numéroLicence { get; set; }

        public List<Dossier> dossiers { get; set; }=new List<Dossier>();




        public Médecin(int idPersonne, string nom, string prénom, DateOnly dateNaissance, string adresse, string spécialité, string numéroLicence, string Identifiant, string MotDePasse) : base(idPersonne, nom, prénom, dateNaissance, adresse, Identifiant, MotDePasse)
        {   
            this.spécialité = spécialité;
            this.numéroLicence = numéroLicence;
        }

        public void AjouterDossier(Dossier dossier)
        {
            dossiers.Add(dossier);
        }

        public void SupprimerDossier(int numéroDossier)
        {
            dossiers.RemoveAll(d => d.numéroDossier == numéroDossier);
        }
       

        public Dossier TrouverDossierParIdPatient(int idPatient)
        {
            foreach (var dossier in dossiers)
            {
                if (dossier.patient.idPersonne == idPatient)
                {
                    return dossier;
                }
            }
            return null; 
        }

        public List <RendezVous> TrouverRendezVousParIdPatient(int idPatient)
        {
            foreach (var dossier in dossiers)
            {
                if (dossier.patient.idPersonne == idPatient)
                {  
                    return dossier.rendezVous;
                }
            }
            return null;
        }

        public List<Patient> RechercherPatientDossiersParNomPrenom(string nom, string prenom)
        
            {
                // Sélectionner les patients des dossiers qui correspondent au nom et prénom fournis
                var patientsTrouvés = dossiers.Where(d => d.patient.nom.Equals(nom, StringComparison.OrdinalIgnoreCase)
                                                       && d.patient.prénom.Equals(prenom, StringComparison.OrdinalIgnoreCase))
                                              .Select(d => d.patient)
                                              .Distinct()
                                              .ToList();
                return patientsTrouvés;
            }




        public override string ToString()
        {
            return base.ToString() + $", Spécialité: {spécialité}, Numéro de Licence: {numéroLicence}";
        }


        }
}
