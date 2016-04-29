using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Control.Model.Entities
{
    [DataContract]
    public class Customer : IEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        public string CompanyName { get; set; }
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
        public int Document { get; set; }
        public decimal Discount { get; set; }
        public int? CommercialPolicy { get; set; }
        public DateTime? RegisterDate { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
