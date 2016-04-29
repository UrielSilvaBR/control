using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Control.Model.Entities
{
    [DataContract]
    public class MessageLog : IEntity
    {
        [Key]
        [DataMember]
        public int Id { get; set; }
        public int? Code { get; set; }
        public string Description { get; set; }
        public string User { get; set; }
        public DateTime? MessageDate { get; set; }
    }
}
