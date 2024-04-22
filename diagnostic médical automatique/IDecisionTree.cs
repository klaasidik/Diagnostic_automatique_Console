using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace diagnostic_médical_automatique
{
    public interface IDecisionTree
    {
         bool IsNumericAttribute(List<Diagnostic> data, string attributeName);
        double CalculateEntropy(List<Diagnostic> data);
        double CalculateInformationGain(List<Diagnostic> data, string attributeIndex);
        double CalculateInformationGainNumeric(List<Diagnostic> data, string attributeName, out double? splitValue);
        bool GetMostCommonClass(List<Diagnostic> data);
        string GetBestAttribute(List<Diagnostic> data, List<string> attributes, out double? splitValue);
         List<object> GetAttributeValues(List<Diagnostic> data, string bestAttribute);
         Noeud BuildTree(List<Diagnostic> data, List<string> attributes);
        void AfficherArbre(Noeud noeud, string indentation = "");
        string Classify(Diagnostic instance);
        string Classify(Diagnostic instance, Noeud node);
        float Evaluate(List<Diagnostic> testData);
        void ConfusionMatrix(List<Diagnostic> testData);

    }
}
