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
    public class OrderProduct : IEntity
    {
        
        [Key]
        [DataMember]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int SequencialItem { get; set; }

        private string _productName;
        public string ProductName
        {
            get
            {
                if (ProductID > 0)
                    return ProductItem.Name;
                else
                    return _productName;

            } set { _productName = value; }
        }

        private string _productModel;
        public string ProductModel
        {
            get
            {
                if (ProductID > 0)
                    return ProductItem.Model;
                else
                    return _productModel;

            }
            set { _productModel = value; }
        }

        private string _description;
        public string Description
        {
            get
            {
                if (ProductID > 0)
                    return ProductItem.Description;
                else
                    return _description;

            }
            set { _description = value; }
        }

        public decimal? QuantityOrder { get; set; } 
        public decimal? QuantityDeliver { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? ItemDiscount { get; set; }
        public string Comments { get; set; }
        public string TotalPrice { get; set; }

        //Forgein Keys
        public int? ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product ProductItem { get; set; }

        public int? TypeUnitID { get; set; }
        [ForeignKey("TypeUnitID")]
        public virtual TypeUnit ProductUnit { get; set; }
    }
}
