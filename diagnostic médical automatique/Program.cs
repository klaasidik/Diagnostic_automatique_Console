using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace diagnostic_médical_automatique
{
    class Program
    {
        static void Main(string[] args)
        {
            // Chemin vers votre fichier CSV
            string filePathTrain = @"C:\Users\klaas\Downloads\Documents\train.csv";
            string filePathTest= @"C:\Users\klaas\Downloads\Documents\test.csv";

            // Liste pour stocker les données
            List<Diagnostic> dataTrain = new List<Diagnostic>();
            List<Diagnostic> dataTest = new List<Diagnostic>();
            int i = 0;
            // Lire le fichier CSV ligne par ligne
            using (var reader = new StreamReader(filePathTrain))
            {
                var line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    var values = line.Split(',');
                    string cp = values[0];
                    string thal = values[1];
                    string ca = values[2];
                    double oldpeak = double.Parse(values[3], CultureInfo.InvariantCulture);
                    double thalach = double.Parse(values[4], CultureInfo.InvariantCulture);
                    bool target = values[5] == "1";
                    Diagnostic diagnostic = new Diagnostic(i, cp, thal, ca, oldpeak, thalach, target);
                    dataTrain.Add(diagnostic);
                    i++;
                }
            }

            using (var reader = new StreamReader(filePathTest))
            {
                var line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    var values = line.Split(',');
                    string cp = values[0];
                    string thal = values[1];
                    string ca = values[2];
                    double oldpeak = double.Parse(values[3], CultureInfo.InvariantCulture);
                    double thalach = double.Parse(values[4], CultureInfo.InvariantCulture);
                    bool target = values[5] == "1";
                    Diagnostic diagnostic = new Diagnostic(i, cp, thal, ca, oldpeak, thalach, target);
                    dataTest.Add(diagnostic);
                    i++;
                }
            }
            double? s;            
            List<string> attributes = new List<string> { "cp", "thal", "ca", "oldpeak", "thalach" };
            DecisionTree arbre = new DecisionTree();
            
            arbre.BuildTree(dataTrain, attributes);
            arbre.AfficherArbre(arbre.BuildTree(dataTrain, attributes));
            Console.WriteLine($"la précision={arbre.Evaluate(dataTest)}");
            arbre.ConfusionMatrix(dataTest);

            // Créer 5 instances de Traitement 
            Traitement traitement1 = new Traitement(1, "Bêta-bloquants", "Les bêta-bloquants sont utilisés pour traiter l'hypertension artérielle, l'angine de poitrine, et certains problèmes de rythme cardiaque.", "Varie selon la condition du patient", "Fatigue, problèmes de sommeil, froid aux mains et aux pieds");
            Traitement traitement2 = new Traitement(2, "Inhibiteurs de l'enzyme de conversion de l'angiotensine (IECA)", "Les IECA sont utilisés pour traiter l'hypertension artérielle, l'insuffisance cardiaque, et après une crise cardiaque.", "Varie selon la condition du patient", "Toux sèche, étourdissements, faiblesse");
            Traitement traitement3 = new Traitement(3, "Anticoagulants", "Les anticoagulants sont utilisés pour prévenir la formation de caillots sanguins et réduire le risque d'accident vasculaire cérébral et d'embolie pulmonaire.", "Varie selon la condition du patient", "Saignements excessifs, ecchymoses, faiblesse");
            Traitement traitement4 = new Traitement(4, "Statines", "Les statines sont utilisées pour réduire le cholestérol et prévenir les maladies cardiovasculaires chez les personnes à risque élevé.", "À long terme", "Douleurs musculaires, faiblesse, douleurs abdominales");
            Traitement traitement5 = new Traitement(5, "Diurétiques", "Les diurétiques sont utilisés pour traiter l'hypertension artérielle, l'insuffisance cardiaque congestive, et l'œdème.", "Varie selon la condition du patient", "Déshydratation, déséquilibre électrolytique, crampes musculaires");

            // Créer 5 instances de Diagnostic d
            Diagnostic diagnostic1 = new Diagnostic(1, "0.0", "1.0", "0.0", 2.3, 150, true);
          
            Diagnostic diagnostic2 = new Diagnostic(2, "1.0", "2.0", "1.0", 1.5, 170, false);
            Diagnostic diagnostic3 = new Diagnostic(3, "3.0", "3.0", "2.0", 3.2, 140, true);
            Diagnostic diagnostic4 = new Diagnostic(4, "2.0", "2.0", "3.0", 0.9, 160, true);
            Diagnostic diagnostic5 = new Diagnostic(5, "0.0", "3", "1", 2.6, 155, true);

            // Créer 3 instances de Médecin spécialisées en cardiologie
            Médecin medecin1 = new Médecin(1, "Dupont", "Jean", new DateOnly(1980, 5, 15), "123 rue de la liberte", "Cardiologue", "12345", "doc1", "password1");
            Médecin medecin2 = new Médecin(2, "Durand", "Marie", new DateOnly(1975, 10, 20), "456 avenue ", "Cardiologue", "67890", "doc2", "password2");
            Médecin medecin3 = new Médecin(3, "Martin", "Paul", new DateOnly(1985, 8, 30), "789 boulevard ", "Cardiologue", "54321", "doc3", "password3");

            // Créer 5 instances de Patient
            Patient patient1 = new Patient(1, "Dubois", "Jeanne", new DateOnly(1988, 3, 12), "789 avenue ", "AB+", "Mutuelle XYZ");
            Patient patient2 = new Patient(2, "Lefevre", "Pierre", new DateOnly(1975, 7, 25), "456 rue ", "O-", "Assurance ABC");
            Patient patient3 = new Patient(3, "Moreau", "Sophie", new DateOnly(1990, 5, 3), "123 boulevard ", "A+", "Assurance ABC");
            Patient patient4 = new Patient(4, "Garcia", "David", new DateOnly(1982, 11, 18), "1010 chemin ", "B-", "Mutuelle XYZ");
            Patient patient5 = new Patient(5, "Bouchard", "Marie", new DateOnly(1970, 9, 8), "222 rue ", "AB+", "Assurance ABC");

            // Créer 5 dossiers 
            Dossier dossier1 = new Dossier(1, DateTime.Now, patient1);
            dossier1.médecin = medecin1;
            dossier1.AjouterConsultation(diagnostic1);
            dossier1.AjouterTraitement(traitement1);
            medecin1.AjouterDossier(dossier1);

            Dossier dossier2 = new Dossier(2, DateTime.Now, patient2);
            dossier2.médecin = medecin2;
            dossier2.AjouterConsultation(diagnostic2);
            dossier2.AjouterTraitement(traitement2);
            medecin2.AjouterDossier(dossier2);

            Dossier dossier3 = new Dossier(3, DateTime.Now, patient3);
            dossier3.médecin = medecin3;
            dossier3.AjouterConsultation(diagnostic3);
            dossier3.AjouterTraitement(traitement3);
            medecin3.AjouterDossier(dossier3);

            Dossier dossier4 = new Dossier(4, DateTime.Now, patient4);
            dossier4.médecin = medecin1; 
            dossier4.AjouterConsultation(diagnostic4);
            dossier4.AjouterTraitement(traitement4);
            medecin1.AjouterDossier(dossier4);

            Dossier dossier5 = new Dossier(5, DateTime.Now, patient5);
            dossier5.médecin = medecin2; 
            dossier5.AjouterConsultation(diagnostic5);
            dossier5.AjouterTraitement(traitement5);
            medecin2.AjouterDossier(dossier5);

            // Créer 2 rendez-vous pour chaque patient 
            RendezVous rdv1Patient1 = new RendezVous(1, new DateTime(2024, 3, 10, 9, 0, 0), "Consultation régulière");
            RendezVous rdv2Patient1 = new RendezVous(2, new DateTime(2024, 3, 15, 14, 30, 0), "Suivi après traitement");
            dossier1.AjouterRendezVous(rdv1Patient1);
            dossier1.AjouterRendezVous(rdv2Patient1);

            RendezVous rdv1Patient2 = new RendezVous(3, new DateTime(2024, 3, 11, 10, 0, 0), "Bilan de santé");
            RendezVous rdv2Patient2 = new RendezVous(4, new DateTime(2024, 3, 18, 11, 0, 0), "Analyse de résultats");
            dossier2.AjouterRendezVous(rdv1Patient2);
            dossier2.AjouterRendezVous(rdv2Patient2);

            RendezVous rdv1Patient3 = new RendezVous(5, new DateTime(2024, 3, 12, 15, 0, 0), "Consultation de suivi");
            RendezVous rdv2Patient3 = new RendezVous(6, new DateTime(2024, 3, 19, 16, 0, 0), "Examen complémentaire");
            dossier3.AjouterRendezVous(rdv1Patient3);
            dossier3.AjouterRendezVous(rdv2Patient3);

            RendezVous rdv1Patient4 = new RendezVous(7, new DateTime(2024, 3, 13, 8, 30, 0), "Consultation cardiaque");
            RendezVous rdv2Patient4 = new RendezVous(8, new DateTime(2024, 3, 20, 9, 30, 0), "Suivi de traitement");
            dossier4.AjouterRendezVous(rdv1Patient4);
            dossier4.AjouterRendezVous(rdv2Patient4);

            RendezVous rdv1Patient5 = new RendezVous(9, new DateTime(2024, 3, 14, 12, 0, 0), "Consultation préventive");
            RendezVous rdv2Patient5 = new RendezVous(10, new DateTime(2024, 3, 21, 13, 0, 0), "Vérification des symptômes");
            dossier5.AjouterRendezVous(rdv1Patient5);
            dossier5.AjouterRendezVous(rdv2Patient5);

            // Créer hôpital 
            Hopital hopital = new Hopital(1, "Hôpital Central", "123 Avenue de la Santé");

            // Ajouter les médecins 
            hopital.AjouterMedecin(medecin1);
            hopital.AjouterMedecin(medecin2);
            hopital.AjouterMedecin(medecin3);

            // Ajouter les patients 
            hopital.AjouterPatient(patient1);
            hopital.AjouterPatient(patient2);
            hopital.AjouterPatient(patient3);
            hopital.AjouterPatient(patient4);
            hopital.AjouterPatient(patient5);

            // Créer un administrateur
            Administrateur administrateur = new Administrateur(1, "Dupont", "Jean", new DateOnly(1980, 5, 15), "123 rue de la santé", "Administrateur système", "admin", "admin_password");
            Console.WriteLine("\n ***************** \n");
            administrateur.SeConnecter();
            Console.WriteLine("\n ***************** \n");
            Console.WriteLine(hopital.ToString());
            Console.WriteLine("\n ***************** \n");

            // Afficher la liste des médecins
            hopital.afficherMedecins();
            Console.WriteLine("\n ***************** \n");

            List<Dossier> dossiers = hopital.medecins[0].dossiers;
            Console.WriteLine($"Liste des dossiers de le medecin {hopital.medecins[0].prénom} {hopital.medecins[0].nom} :");
            Console.WriteLine("\n ***************** \n");

            foreach (Dossier dossier in dossiers)
            {
                Console.WriteLine(dossier.ToString());
            }
            Console.WriteLine($"On va estimer le dignostic de dernier dossier: \t {hopital.medecins[0].dossiers[1].HistoriqueConsultations[0].EstimerTargetAvecArbre(arbre)}");

        }
    }
}
