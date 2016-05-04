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
    public class Contact : IEntity 
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        public int? CustomerID { get; set; }
        public int? VendorID { get; set; }
        public string ContatName { get; set; }
        public string ContatRoleName { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
        public string AddressDistrict { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
        public int? ZipCode { get; set; }
        public DateTime? RegisterDate { get; set; }
        public DateTime? LastUpdate { get; set; }

        [ForeignKey("VendorID")]
        public virtual Vendor ContactVendor { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer ContactCustomer { get; set; }
    }
}
