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
       public int IdDiagnostic {  get; set; }
       public string Cp { get; set; } //Type de douleur thoracique
       public  string Thal {  get; set; } //Un type de test de stress au thallium cardiaque
       public string Ca { get; set; } //Nombre de vaisseaux principaux (0-3) colorés par la fluoroscopie
       public double Oldpeak { get; set; } //Dépression ST induite par l'exercice par rapport au repos
       public double Thalach { get; set; } //Fréquence cardiaque maximale atteinte
       public bool Target {  get; set; } //Présence True ou absence False de maladie cardiaque
       

        public Diagnostic(int idDiagnostic, string cp, string thal, string ca, double oldpeak, double thalach, bool target )
        {
            this.IdDiagnostic = idDiagnostic;
            this.Cp = cp;
            this.Thal = thal;
            this.Ca = ca;
            this.Oldpeak = oldpeak;
            this.Thalach = thalach;
            this.Target = target;

        }

        public override string? ToString()
        {
            return $"Diagnostic ID: {IdDiagnostic}, CP: {Cp}, Thal: {Thal}, CA: {Ca}, Oldpeak: {Oldpeak}, Thalach: {Thalach}, Target: {Target}";
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
