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
        public ProductProvider()
        {
            InsertDate = DateTime.Now;
        }

        [Key]
        [DataMember]
        public int Id { get; set; }

        public int ProductID { get; set; }

        public int ProviderID { get; set; }

        [ForeignKey("ProviderID")]
        public virtual Provider Provider { get; set; }

        [Display(Name ="Código Forn.")]
        public long? Code { get; set; }

        public DateTime InsertDate { get; set; }

        public bool IsActive { get; set; }

        public string ModelProvider { get; set; }

        [NotMapped]
        public int Sequencial { get; set; }

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