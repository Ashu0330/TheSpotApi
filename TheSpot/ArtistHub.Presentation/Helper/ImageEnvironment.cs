using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;


public class ImageEnvironment
{
#if DEBUG
    public static string Baseurl => "https://localhost:7115/";


#else
        public static string Baseurl=> "https://musicapi.deftinstitute.in/";
#endif
}

