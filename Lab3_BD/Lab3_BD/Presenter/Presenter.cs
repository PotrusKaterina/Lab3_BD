using Lab3_BD.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3_BD.Presenter
{
    class Presenter : IPresenter
    {
        IRepository repository;

        public Presenter()
        {
            repository = new Repository.Repository();
        }

        public IEnumerable<string> GetBranchTwoElements(int a, int b)
        {
            return repository.GetBranchTwoElements(a,b);
        }

        public IEnumerable<string> GetDaughter(int numb)
        {
            return repository.GetDaughter(numb);
        }

        public IEnumerable<string> GetFullTree()
        {
            return repository.GetFullTree();
        }

        public IEnumerable<string> GetParent(int numb)
        {
            return repository.GetParent(numb);
        }
    }
}
