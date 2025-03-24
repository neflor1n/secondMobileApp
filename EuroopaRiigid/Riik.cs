using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace secondMobileApp.EuroopaRiigid
{
    public class Riik
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Nimetus { get; set; } 

        [NotNull]
        public string Pealinn { get; set; } 

        [NotNull]
        public int RahvastikuSuurus { get; set; } 

        [NotNull]
        public string Lipp { get; set; }

    }
}
