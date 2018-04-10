using System;
using System.ComponentModel.DataAnnotations.Schema;
using g4u.Core.Models;

namespace g4u.Controllers.Resources
{
    public class SaveProductResource
    {
        //TODO: REQUIRED ATTRIBUTES
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int? AgeLimit { get; set; }
        public int Price { get; set; }
        public DateTime? ReleaseDate { get; set; }  
        public string SellOrBuy { get; set; }
        public string Description { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CategoryId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlatformId { get; set; }
        public string AuthSub { get; set; }
    }
}