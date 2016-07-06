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
    public class Product : IEntity 
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        public int? ProductCode { get; set; }
        public string Name { get; set; }
        [DataMember]
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Description { get; set; }
        public string UnitMeasure { get; set; }

        public string Height { get; set; } //Altura
        public string Width { get; set; } //Largura
        public string Lenght { get; set; } //Comprimento

        public decimal? Weight { get; set; }

        public decimal? UnitPrice { get; set; }
        public decimal? ListPrice { get; set; }
        public decimal? CostPrice { get; set; }
        public decimal? AliqICMS { get; set; }
        public decimal? AliqIPI { get; set; }
        public decimal? Markup { get; set; }
        [Display(Name="Qtd. Estoque")]
        public decimal? QuantityCurrentStock { get; set; }

        public decimal? MinimumStockAlert { get; set; }
        public string ImageURL { get; set; }
        /// <summary>
        /// Indica se é um produto combinado, que possui outros produtos do estoque em sua composição
        /// </summary>
        /// 
        public bool? CombinedProduct { get; set; }

        [Display(Name="NCM")]
        public string NCMCode { get; set; }

        [Display(Name = "Descrição NCC")]
        public string DescriptionNCC { get; set; }

        //Forgein Keys
        public int? ProductTypeUnitID { get; set; }

        [ForeignKey("ProductTypeUnitID")]
        public virtual TypeUnit ProductUnit { get; set; }

        [NotMapped]
        public string FullDescription
        {
            get
            {
                if (Id > 0)
                    return String.Format("{0} | {1} | {2}", Name, Model, NCMCode);
                else
                    return Name;
            }
        }
    }
}

