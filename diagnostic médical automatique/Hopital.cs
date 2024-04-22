using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diagnostic_médical_automatique
{
    public class Hopital { 
   
        public int idHopital {  get; set; }
        public string nomHopital {  get; set; }
        public string adresseHopital {  get; set; }
        public List<Médecin> medecins { get; set; }
        public List<Patient> patients { get; set; }

        public Hopital(int idHopital, string nomHopital, string adresseHopital)
        {
            this.idHopital = idHopital;
            this.nomHopital = nomHopital;
            this.adresseHopital = adresseHopital;
            medecins = new List<Médecin>();
            patients = new List<Patient>();
        }

        public void AjouterMedecin(Médecin medecin)
        {
            this.medecins.Add(medecin);
        }

        public void SupprimerMedecin(Médecin medecin)
        {
            medecins.Remove(medecin);
        }

        public void AjouterPatient(Patient patient)
        {
            patients.Add(patient);
        }

        public void SupprimerPatient(Patient patient)
        {
            patients.Remove(patient);
        }

        public Médecin TrouverMedecinParNom(string nom)
        {
            return medecins.FirstOrDefault(m => m.nom == nom);
        }

        public Patient TrouverPatientParNom(string nom)
        {
            return patients.FirstOrDefault(p => p.nom == nom);
        }

        public override string? ToString()
        {
            return $"Hopital: {nomHopital}, Adresse: {adresseHopital}, Médecins: {medecins.Count}, Patients: {patients.Count}";
        }
        public void afficherMedecins()
        {
            Console.WriteLine("Liste des médecins dans l'hôpital :");
            foreach (Médecin medecin in this.medecins)
            {
                Console.WriteLine($"ID : {medecin.idPersonne}, Nom : {medecin.nom}, Spécialité : {medecin.spécialité}");
            }
        }
    }
}
