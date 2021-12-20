using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppFbFramework019
{
    [Table("TEL_OPERATOR")]
    public class PO_TEL_OPERATOR
    {

        [Required]
        [Key]
        [Column("ID_OPERATOR")]
        public int Id
        { //  ID_OPERATOR   (PK)
            get;
            set;
        }


        [Column("KOD_OPERATOR")]
        [MaxLength(2)]
        public string Kod
        {  //  KOD_OPERATOR   C[2]
            get;
            set;
        }


        [Column("NAME_OPERATOR")]
        [MaxLength(50)]
        public string Name
        {  //   NAME_OPERATOR   V[50]
            get;
            set;
        }


        [Column("ID_BK")]
        public int? IdOfKodBudjet  // Ид бюджкода
        {  //   ID_BK
            get;
            set;
        }

        [Column("ID_CONNECT")]
        public int IdOfConnectType
        {  //  ID_CONNECT  (FK  ==>  TEL_VID_CONNECT)
            get;
            set;
        }


        [Column("ID_KAGENT")]
        public int? IdOfKagent // Идентификатор контрагента
        {  //   ID_KAGENT
            get;
            set;
        }


        [Column("NOM_DOG")]
        [MaxLength(22)]
        public string NumberOfContract // Номер договора
        {  //   NOM_DOG   V[22]
            get;
            set;
        }


        [Column("DATE_DOG")]
        public DateTime DateOfContract //Дата договора
        {  //   DATE_DOG
            get;
            set;
        }

        public PO_TEL_VID_CONNECT pO_TEL_VID_CONNECT { get; set; }

    }
}
