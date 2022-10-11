using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entity;

namespace BLL
{
    public class Index_BLL
    {
        Index_DAL index_DAL = new Index_DAL();
        public List<YJ_indexPanel> indexPanel()
        {
            return index_DAL.indexPanel();
        }
    }
}
