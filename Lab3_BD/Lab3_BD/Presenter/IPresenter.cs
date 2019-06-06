using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_BD.Presenter
{
    interface IPresenter
    {
        IEnumerable<String> GetFullTree();
        IEnumerable<String> GetParent(int numb);
        IEnumerable<String> GetDaughter(int numb);
        IEnumerable<String> GetBranchTwoElements(int a, int b);
    }
}
