using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFbFramework019
{
    [Table("TEL_VID_CONNECT")]
    public class PO_TEL_VID_CONNECT
    {

        public PO_TEL_VID_CONNECT()
        {
            pO_TEL_OPERATORs = new List<PO_TEL_OPERATOR>();
        }

        [Required]
        [Key]
        [Column("ID_CONNECT")]
        public int Id
        {   //   ID_CONNECT   (PK)
            get;
            set;
        }


        [Column("KOD_CONNECT")]
        [MaxLength(1)]
        public string KodOfConnect
        {    //   KOD_CONNECT   C[1]
            get;
            set;
        }


        [Column("NAME_CONNECT")]
        [MaxLength(50)]
        public string Name
        {    //   NAME_CONNECT  V[50]
            get;
            set;
        }

        public ICollection<PO_TEL_OPERATOR> pO_TEL_OPERATORs { get; set; }

    }
}
