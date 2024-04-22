using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTreeLibrary
{
    public  class Diagnostic
    {
       public int idDiagnostic {  get; set; }
       public string cp { get; set; } //Type de douleur thoracique
       public  string thal {  get; set; } //Un type de test de stress au thallium cardiaque
       public string ca { get; set; } //Nombre de vaisseaux principaux (0-3) colorés par la fluoroscopie
       public double oldpeak { get; set; } //Dépression ST induite par l'exercice par rapport au repos
       public double thalach { get; set; } //Fréquence cardiaque maximale atteinte
       public bool target {  get; set; } //Présence True ou absence False de maladie cardiaque
       

        public Diagnostic(int idDiagnostic, string cp, string thal, string ca, double oldpeak, double thalach, bool target )
        {
            this.idDiagnostic = idDiagnostic;
            this.cp = cp;
            this.thal = thal;
            this.ca = ca;
            this.oldpeak = oldpeak;
            this.thalach = thalach;
            this.target = target;

        }

        public override string? ToString()
        {
            return $"Diagnostic ID: {idDiagnostic}, CP: {cp}, Thal: {thal}, CA: {ca}, Oldpeak: {oldpeak}, Thalach: {thalach}, Target: {target}";
        }

        public object GetValueByName(string propertyName)
        {
            Type type = this.GetType();
            PropertyInfo property = type.GetProperty(propertyName);
            if (property != null)
            {
                return property.GetValue(this, null);
            }

            FieldInfo field = type.GetField(propertyName);
            if (field != null)
            {
                return field.GetValue(this);
            }

            throw new ArgumentException($"Property or field '{propertyName}' not found", propertyName);
        }

        public String EstimerTargetAvecArbre( DecisionTree arbre)
        {
            
            return arbre.Classify(this);
        }

    }
}
