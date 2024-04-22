using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DecisionTreeLibrary
{
    public class DecisionTree : IDecisionTree
    {
        public Noeud racine { get; set; } 
        public DecisionTree() { }

        public bool IsNumericAttribute(List<Diagnostic> data, string attributeName) 
        {
            // Vérifier si la liste est vide
            if (data == null || data.Count == 0) return false;

            // Utiliser la réflexion pour obtenir le type de la propriété 
            PropertyInfo propertyInfo = typeof(Diagnostic).GetProperty(attributeName);

            // Si la propriété n'existe pas, retourner false
            if (propertyInfo == null) return false;

            // Obtenir le type de la propriété
            Type propertyType = propertyInfo.PropertyType;

            // Vérifier si le type de la propriété est numérique
            bool isNumeric = propertyType == typeof(int) || propertyType == typeof(double) ||
                         propertyType == typeof(float) || propertyType == typeof(decimal) ||
                         propertyType == typeof(long) || propertyType == typeof(short) ||
                         propertyType == typeof(uint) || propertyType == typeof(ulong) ||
                         propertyType == typeof(ushort) || propertyType == typeof(byte) ||
                         propertyType == typeof(sbyte);

            return isNumeric;

            }

        public double CalculateEntropy(List<Diagnostic> data)
        {
            // Groupement des éléments de 'data' par leur propriété 'Target' et comptage du nombre d'éléments dans chaque groupe.
            var comptageLabelsClasse = data.GroupBy(d => d.Target).ToDictionary(g => g.Key, g => g.Count());

            double entropie = 0.0;
            foreach (var comptageLabel in comptageLabelsClasse)
            {
                double probabilite = comptageLabel.Value / (double)data.Count;
                entropie -= probabilite * Math.Log(probabilite, 2);
            }
            return entropie;
           
        }

        public double CalculateInformationGain(List<Diagnostic> data, string attributeName)
        {   // Calcul de l'entropie totale de l'ensemble des données
            double entropieTotale = CalculateEntropy(data);

            // Extraction des valeurs distinctes de l'attribut spécifié à partir de l'ensemble des données.
            var valeursAttribut = data.Select(d => d.GetValueByName(attributeName)).Distinct();

            double entropiePonderee = 0.0;
            foreach (var valeur in valeursAttribut)
            {
                // Filtrage de 'data' pour obtenir un sous-ensemble d'éléments où la valeur de l'attribut correspond à la valeur courante dans la boucle.
                var sousEnsemble = data.Where(d => d.GetValueByName(attributeName).Equals(valeur)).ToList();

                // Calcul de l'entropie pour le sous-ensemble filtré
                double entropieSousEnsemble = CalculateEntropy(sousEnsemble);


                entropiePonderee += (sousEnsemble.Count / (double)data.Count) * entropieSousEnsemble;
            }

            return entropieTotale - entropiePonderee;
  
        }

        public double CalculateInformationGainNumeric(List<Diagnostic> data, string attributeName, out double? splitValue)
        {
            splitValue = null;
            double maxInformationGain = 0.0;
            double meilleureValeurDivision = 0.0;

            // Récupère toutes les valeurs possibles pour l'attribut et les trie
            var valeursAttribut = data.Select(d => Convert.ToDouble(d.GetValueByName(attributeName))).Distinct().OrderBy(v => v).ToList();
            
            // Calcule l'entropie avant la division
            double entropieTotaleAvantDivision = CalculateEntropy(data);

            for (int i = 0; i < valeursAttribut.Count ; i++)

            {
                    
                    double valeurDivisionCourante = valeursAttribut[i];
               
                // Divise les données en fonction de la valeur de division
                var donneesDessous = data.Where(d => Convert.ToDouble(d.GetValueByName(attributeName)) <= valeurDivisionCourante).ToList();
                var donneesDessus = data.Where(d => Convert.ToDouble(d.GetValueByName(attributeName)) > valeurDivisionCourante).ToList();

                // Calcule l'entropie après la division
                double entropieDessous = CalculateEntropy(donneesDessous);
                double entropieDessus = CalculateEntropy(donneesDessus);

                // Calcule le gain d'information
                double informationGain = entropieTotaleAvantDivision - ((donneesDessous.Count / (double)data.Count) * entropieDessous + (donneesDessus.Count / (double)data.Count) * entropieDessus);
            
                // Met à jour le meilleur gain d'information et la meilleure valeur de division
                if (informationGain >= maxInformationGain)
                {
                    maxInformationGain = informationGain;
                    meilleureValeurDivision = valeurDivisionCourante;
                    splitValue = valeurDivisionCourante;
                }
            }
            
            return maxInformationGain;
        }

        public  bool GetMostCommonClass(List<Diagnostic> data)
        {
            // Compter le nombre de target (true ou false pour la présence de maladie)
            int countTrue = data.Count(d => d.Target);
            int countFalse = data.Count(d => !d.Target);

            // Retourner la classe la plus commune
            return countTrue >= countFalse;
        }

        public  string GetBestAttribute(List<Diagnostic> data, List<string> attributes, out double? splitValue)
        {
            double maxGain =-1;
            string meilleurAttribut = null;
            splitValue = null;
            double? meilleurValeurDivision = null;

               foreach (var attribute in attributes)
            {
                double? valeurDivisionCourant = null;
                double gain;
                // Vérifie si l'attribut actuel est numérique ou catégorique pour calculer le gain 
                if (IsNumericAttribute(data, attribute))
                {
                    gain = CalculateInformationGainNumeric(data,attribute, out valeurDivisionCourant);
           
                }
                else
                {
                    gain = CalculateInformationGain(data,attribute);
                }
                //Mettre à jour le meilleur attribut si le gain est le plus élevé
                if (gain > maxGain)
                {
                    maxGain = gain;
                    meilleurAttribut = attribute;
                    meilleurValeurDivision = valeurDivisionCourant;
                }
            }

            splitValue = meilleurValeurDivision;
            return meilleurAttribut;
        }
        public List<object> GetAttributeValues(List<Diagnostic> data, string bestAttribute)
        {
            // Récupérez les valeurs de l'attribut pour tous les éléments dans data
            var valeurs = data.Select(element => element.GetValueByName(bestAttribute)).Distinct().ToList();

            
            return valeurs;
            
        }

        public Noeud BuildTree(List<Diagnostic> data, List<string> attributes)
        {   

            //  si toutes les instances ont la même classe, retournez un nœud feuille
            if (data.All(d => d.Target == data.First().Target))
            {
                this.racine = new Noeud
                {
                    prediction = data.First().Target.ToString(),
                    estFeuille = true

                };
                return this.racine;
            }
            // Si la liste des attributs est vide, retournez le nœud feuille avec la classe la plus commune
            if (!attributes.Any())
            {

                this.racine =  new Noeud
                {
                    prediction = GetMostCommonClass(data).ToString(),
                    estFeuille=true
                };
                return this.racine;
            }
            // Sélectionnez le meilleur attribut pour diviser les données
            double? valeurDivision;
            string meilleurAttribut = GetBestAttribute(data, attributes, out valeurDivision);

            // Créez le nœud racine pour cet attribut
            Noeud rootNode = new Noeud
            {
                critere = meilleurAttribut,
                valeur = valeurDivision,
                enfants = new Dictionary<string, Noeud>()
            };
            // Divisez les données et construisez les branches récursivement
            // Pour chaque valeur unique de l'attribut, créez un sous-ensemble de données et construisez un sous-arbre récursivement
            if (IsNumericAttribute(data, meilleurAttribut))
            {
                var sousEnsselbleInferieur = data.Where(d => Convert.ToDouble(d.GetValueByName(meilleurAttribut)) <= valeurDivision).ToList();
                var sousEnsselbleSuperieur = data.Where(d => Convert.ToDouble(d.GetValueByName(meilleurAttribut)) > valeurDivision).ToList();
                var sousEnssembleAttributs = attributes.Where(a => a != meilleurAttribut).ToList();
                if (sousEnsselbleInferieur.Any())
                {
                    rootNode.enfants.Add($"<= {valeurDivision}", BuildTree(sousEnsselbleInferieur, sousEnssembleAttributs));
                }

                if (sousEnsselbleSuperieur.Any())
                {
                    rootNode.enfants.Add($"> {valeurDivision}", BuildTree(sousEnsselbleSuperieur, sousEnssembleAttributs));
                }
            }
            else
            {
                var attributeValues = GetAttributeValues(data, meilleurAttribut);

                foreach (var value in attributeValues)
                {
                    var subset = data.Where(d => Equals(d.GetValueByName(meilleurAttribut), value)).ToList();
                    var subAttributes = attributes.Where(a => a != meilleurAttribut).ToList();
                    Noeud childNode = BuildTree(subset, subAttributes);
                    rootNode.enfants.Add(value.ToString(), childNode);
                }
            }
            this.racine = rootNode;
            return rootNode;
        }

        public void AfficherArbre(Noeud noeud, string indentation = "")
        {
            // Vérifie si le nœud est une feuille
            if (noeud.estFeuille)
            {
                Console.WriteLine($"{indentation}+-- Prédiction: {noeud.prediction}");
                return;
            }

            // Affiche le critère de division du nœud courant
            Console.WriteLine($"{indentation}+-- Noeud: {noeud.critere}");

            // Parcourt chaque enfant du nœud courant
            if (noeud.enfants != null)
            {
                // Utiliser une liste pour connaître le dernier élément
                var enfantsList = noeud.enfants.ToList();
                for (int i = 0; i < enfantsList.Count; i++)
                {
                    
                    var estDernierEnfant = i == enfantsList.Count - 1;
                    var branche = estDernierEnfant ? "   +--" : "|  +--";
                    var continuation = estDernierEnfant ? "    " : "|   ";

                    // Affiche la branche actuelle
                    Console.WriteLine($"{indentation}{branche} {enfantsList[i].Key}");

                    // Appel récursif pour afficher les enfants du noeud actuel
                    AfficherArbre(enfantsList[i].Value, $"{indentation}{continuation}");
                }
            }
            else
            {
                Console.WriteLine($"{indentation}   +-- Erreur: Noeud sans enfants.");
            }
        }


        public string Classify(Diagnostic instance)
        {
            return Classify(instance, this.racine);
        }
        public string Classify(Diagnostic instance, Noeud node)
        {
            // Si le nœud est une feuille, retournez sa classe
            if (node.estFeuille)
            {
                return node.prediction;
            }

            // Sinon, continuez à traverser l'arbre en fonction de la valeur de l'attribut du nœud actuel
            var valeurAttribut = instance.GetValueByName(node.critere)?.ToString();
       
            // Gestion des attributs numériques et catégoriels
            if (node.valeur.HasValue)
            {
                // Si la valeur de division est défini, utilisez-le pour décider de la direction à suivre
                double valeurInstance;
                if (double.TryParse(valeurAttribut, out valeurInstance))
                {
                    if (valeurInstance <= node.valeur)
                    {
                        // Aller à gauche
                        if (node.enfants.ContainsKey($"<= {node.valeur}"))
                        {
                            return Classify(instance, node.enfants[$"<= {node.valeur}"]);
                        }
                    }
                    else
                    {
                        // Aller à droite
                        if (node.enfants.ContainsKey($"> {node.valeur}"))
                        {
                            return Classify(instance, node.enfants[$"> {node.valeur}"]);
                        }
                    }
                }
            }
            else
            {
                // Pour les attributs catégoriels, suivez le chemin correspondant à la valeur de l'attribut
                if (node.enfants.ContainsKey(valeurAttribut))
                {
                    return Classify(instance, node.enfants[valeurAttribut]);
                }
            }
            Console.WriteLine("Erreur");
            Console.WriteLine(instance.ToString());
            // Si aucun chemin correspondant n'est trouvé : retourner null 
            return null; 
        }

        public float Evaluate(List<Diagnostic> testData)
        {

            int correctPredictions = 0;
            for (int i = 0; i < testData.Count; i++)
            {
                string predicted = Classify(testData[i]);
                string actualLabel = testData[i].Target ? "True" : "False";
                if (predicted == actualLabel)
                {
                    correctPredictions++;
                }
            }

            return (float)correctPredictions / testData.Count;
        }

        public void ConfusionMatrix(List<Diagnostic> testData)
        {
            int truePositives = 0, falsePositives = 0, trueNegatives = 0, falseNegatives = 0;

            foreach (var instance in testData)
            {
                string prediction = Classify(instance); 
                bool actual = instance.Target;

                if (prediction == "True" && actual == true)
                {
                    truePositives++;
                }
                else if (prediction == "True" && actual == false)
                {
                    falsePositives++;
                }
                else if (prediction == "False" && actual == false)
                {
                    trueNegatives++;
                }
                else if (prediction == "False" && actual == true)
                {
                    falseNegatives++;
                }
            }

           
            Console.WriteLine($"{"",-20} | {"Prédit Non",-12} | {"Prédit Oui",-12}");
            Console.WriteLine(new string('-', 50));
            Console.WriteLine($"{"Vrai Non (Actual)",-20} | {trueNegatives,-12} | {falsePositives,-12}");
            Console.WriteLine($"{"Vrai Oui (Actual)",-20} | {falseNegatives,-12} | {truePositives,-12}");
        }




    }
}
