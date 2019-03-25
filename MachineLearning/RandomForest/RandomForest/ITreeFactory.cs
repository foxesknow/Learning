using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RandomForest
{
    public interface ITreeFactory<in TData, TScore>
    {
        ITree<TData, TScore> MakeTree(XElement element);
    }
}
