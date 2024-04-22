namespace diagnostic_médical_automatique
{
    public abstract class Personne
    { public int idPersonne {  get; set; }
      public string nom {  get; set; }
      public string prénom { get; set; }
     public DateOnly dateNaissance { get; set; }
     public string adresse { get; set; }

        protected Personne(int idPersonne, string nom, string prénom, DateOnly dateNaissance, string adresse)
        {
            this.idPersonne = idPersonne;
            this.nom = nom;
            this.prénom = prénom;
            this.dateNaissance = dateNaissance;
            this.adresse = adresse;
        }
        public int CalculerAge()
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            int age = today.Year - dateNaissance.Year;
            if (dateNaissance > today.AddYears(-age)) age--;
            return age;
        }

        public override string ToString()
        {
            return $"ID: {idPersonne}, Nom: {nom}, Prénom: {prénom}, Date de Naissance: {dateNaissance}, Âge: {CalculerAge()}, Adresse: {adresse}";
        }
    }
}
