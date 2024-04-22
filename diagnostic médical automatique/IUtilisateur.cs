using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace diagnostic_médical_automatique
{
    public  interface IUtilisateur
    {
        protected string identifiant { get; set; }
        protected string motDePasse { get; set; }
        protected void SeConnecter();
        protected void SeDeconnecter();
    }
}
