using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wt_lab3_3mvc_pravotorova.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Number { get; set; } = "";
        public string Category { get; set; } = "";
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public bool IsAvailable { get; set; }
    }
}
