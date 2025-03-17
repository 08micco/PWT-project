using System.ComponentModel.DataAnnotations.Schema;

namespace PWT_project.Api.Models
{
	[Table("beholdning")]
	public class Beholdning
	{
		public string ean { get; set; }
		public int ShopId { get; set; }
		public int SupplierId { get; set; }
		public int InventoryQuantity { get; set; }
	}
}
