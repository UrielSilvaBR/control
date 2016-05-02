using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.Entities
{
    [DataContract]
    public class Branch : IEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        ////ForeignKey Keys
        public int CompanyID { get; set; }
        [ForeignKey("CompanyID")]
        public virtual Company Company { get; set; }

        public string RazaoSocial { get; set; }

        public string Fantasia { get; set; }

        public long InscricaoEstadual { get; set; }

        public long InscricaoMunicipal { get; set; }

        public long CNPJ { get; set; }
    }
}