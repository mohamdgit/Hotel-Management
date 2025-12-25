using Hotel_Management.DOMAIN.Models.BaseEntity;
using Hotel_Management.DOMAIN.Models.HotelModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Management.DOMAIN.Models.RoomModel
{
    public class RoomPhoto :BaseEntity<int>
    {
        
        public string Url { get; set; } = null!;
        public decimal Size { get; set; }
       
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        [InverseProperty("Photos")]
        public  Room Room { get; set; } = null!;
    }
}
