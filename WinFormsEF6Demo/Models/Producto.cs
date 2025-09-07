using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsEF6Demo.Models
{
    [Table("PRODUCTO")]
    public class Producto
    {
        [Column("PRO_CODIGO")]
        public int ProductoId { get; set; }

        [Required]
        [Column("PRO_DESCRIPCION")]
        [StringLength(150)]
        public string Descripcion { get; set; } = string.Empty;

        [Required]
        [Column("PRO_ESTADO")]
        [StringLength(1)]
        public string Estado { get; set; } = string.Empty;

        [Required]
        [Column("PRO_PVP")]
        public decimal Pvp { get; set; }

        [Required]
        [Column("PRO_IVA")]
        public int Iva { get; set; }
    }
}
