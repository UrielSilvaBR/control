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
    public class ProductProvider : IEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        public int ProductID { get; set; }

        public int ProviderID { get; set; }

        [ForeignKey("ProviderID")]
        public virtual Provider Provider { get; set; }

        public long? Code { get; set; }

        [NotMapped]
        public string NameProvider
        {
            get
            {
                return Provider != null ? Provider.CompanyNameOrder : "";
            }
        }

    }
}