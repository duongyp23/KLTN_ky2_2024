using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB07.HCSN2.DUONG.Common.Command
{
    public class PropertyCommand
    {
        public static String Delete = "DELETE FROM property WHERE PropertyID = @PropertyID;"; 

        public static String GetMaxCode = "SELECT PropertyCode FROM property ORDER BY PropertyCode DESC LIMIT 1;";
    }
}
