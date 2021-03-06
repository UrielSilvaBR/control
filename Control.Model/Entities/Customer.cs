﻿using System;
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
    public class Customer : IEntity
    {
        public Customer()
        {
            RegisterDate = DateTime.Now;
        }

        [Key]
        [DataMember]
        public int Id { get; set; }

        public string CompanyName { get; set; }
        public string ShortName { get; set; }
        public CustomerType CustomerType { get; set; }
        public string AddressStreet { get; set; }
        public string AddressNumber { get; set; }
        public string AddressDistrict { get; set; }

        [DataMember]
        public int AddressCityId { get; set; }

        [ForeignKey("AddressCityId")]
        public virtual City City { get; set; }

        [DataMember]
        public int AddressStateId { get; set; }

        [DataMember]
        public string AddressCountryId { get; set; }
        public string AddressComplement { get; set; }
        public string ZipCode { get; set; }
        public int? PhoneCode { get; set; }
        public int? Phone { get; set; }
        public int? PhoneFax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }

        [Display(Name = "CPF/CNPJ")]
        public string Document { get; set; }        
        public DateTime? RegisterDate { get; set; }
        public DateTime? LastUpdate { get; set; }

        public decimal? Discount { get; set; }
        public int? CommercialPolicy { get; set; }

        [Display(Name = "Cond. Pagamento")]
        public int? PaymentTermId { get; set; }
        [ForeignKey("PaymentTermId")]
        public virtual PaymentTerm PaymentTerm { get; set; }

        public int? ShippingId { get; set; }
        [ForeignKey("ShippingId")]
        public virtual ShippingMode Shipping { get; set; }

        [Display(Name = "Vendedor")]
        public int? VendorId { get; set; }
        [ForeignKey("VendorId")]
        public virtual Vendor Vendor { get; set; }

        [NotMapped]
        public string CompanyNameOrder
        {
            get
            {
                long document = Convert.ToInt64(Document);

                string cpfCnpj = CustomerType == CustomerType.Fisica ? String.Format(@"{0:000\.000\.000\-00}", document) : String.Format(@"{0:00\.000\.000\/0000\-00}", document);

                if (Id > 0)
                    return String.Format("{0} - {1} - {2}", CompanyName, ShortName, cpfCnpj);
                else
                    return CompanyName;
            }
        }

        [ForeignKey("CustomerID")]
        public virtual List<VendorsCustomer> Vendors { get; set; }
    }
}