using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GameBook.Server.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;

namespace GameBook.Server.Models
{
    public class DataConsumableItem
    {
        [Key]
        public int ConsumableItemID { get; set; }
        public int ItemID { get; set; }
        public int HealthValue { get; set; }
        public int EnergyValue { get; set; }
        public int RadiationValue { get; set; }

        [ForeignKey(nameof(ItemID))]
        public DataItem Item { get; set; }
    }

}