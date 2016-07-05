using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;
using Control.Model.Enums;

namespace Control.Model.Entities
{
    [DataContract]
    public class Provider : IEntity
    {        
        [Key]
        [DataMember]
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public ProviderType ProviderType { get; set; }
        public string ShortName { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
        public string AddressDistrict { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public int? ZipCode { get; set; }
        public int? PhoneCode { get; set; }
        public int? Phone { get; set; }
        public int? PhoneFax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        [Display(Name = "CPF/CNPJ")]
        public long Document { get; set; }
        public decimal Discount { get; set; }
        public int? CommercialPolicy { get; set; }
        public DateTime? RegisterDate { get; set; }
        public DateTime? LastUpdate { get; set; }

        [NotMapped]
        public string CompanyNameOrder
        {
            get
            {
                long document = Convert.ToInt64(Document);

                string cpfCnpj = ProviderType == ProviderType.Fisica ? String.Format(@"{0:000\.000\.000\-00}", document) : String.Format(@"{0:00\.000\.000\/0000\-00}", document);

                if (Id > 0)
                    return String.Format("{0} - {1} - {2}", CompanyName, ShortName, cpfCnpj);
                else
                    return CompanyName;
            }
        }
    }
}
