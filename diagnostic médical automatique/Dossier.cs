using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diagnostic_médical_automatique
{
    public class Dossier
    {
        public int numéroDossier {  get; set; }
        public DateTime dateCreation {  get; set; }
        public Médecin médecin { get; set; }
        public Patient patient { get; set; }
        public List<Diagnostic> HistoriqueConsultations { get; set; } = new List<Diagnostic>();
        public List<Traitement> TraitementsPrescrits { get; set; } = new List<Traitement>();

        public List<RendezVous> rendezVous { get; set; } = new List<RendezVous>();

        public Dossier(int numéroDossier, DateTime dateCreation, Patient patient)
        {
            this.numéroDossier = numéroDossier;
            this.dateCreation = dateCreation;
            this.patient = patient;


    }

    public Dossier()
        {
        }

    public override string ToString()
        {
            string result = $"Numéro de Dossier: {numéroDossier}\n" +
                            $"Date de Création: {dateCreation.ToShortDateString()}\n" +
                            $"Médecin Responsable: {médecin.ToString()}\n" +
                            $"Patient: {patient.ToString()}\n" +
                            "Historique des Consultations:\n";

            foreach (var consultation in HistoriqueConsultations)
            {
                result += $"{consultation.ToString()}\n";
            }
            result += "Traitements Prescrits:\n";
            foreach (var traitement in TraitementsPrescrits)
            {
                result += $"{traitement.ToString() }\n";
            }
            result += "Les rendez Vous :\n";
            foreach (var ren in rendezVous)
            {
                result += $"{ren.ToString()}\n";
            }
            return result;
        }

        public void AjouterConsultation(Diagnostic consultation)
        {
            HistoriqueConsultations.Add(consultation);
        }

        public void AjouterTraitement(Traitement traitement)
        {
            TraitementsPrescrits.Add(traitement);
        }

        public void AjouterRendezVous(RendezVous rendezvous)
        {
            rendezVous.Add(rendezvous);
        }
        public void SupprimerRendezVous(int idRendezVous)
        {
            this.rendezVous.RemoveAll(r => r.idRendezVous == idRendezVous);
        }

    }



}
