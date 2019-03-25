using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace RandomForest
{
    public class ForestLoader<TData, TScore>
    {
        private readonly ITreeFactory<TData, TScore> m_TreeFactory;

        public ForestLoader(ITreeFactory<TData, TScore> treeFactory)
        {
            m_TreeFactory = treeFactory;
        }

        public IReadOnlyList<ITree<TData, TScore>> Load(XmlReader reader)
        {
            var trees = new List<ITree<TData, TScore>>();

            reader.ReadToFollowing("Segment");
            while(reader.Name == "Segment")
            {
                reader.ReadToFollowing("Node");
                var element = (XElement)XNode.ReadFrom(reader);

                var tree = m_TreeFactory.MakeTree(element);
                trees.Add(tree);

                reader.ReadToFollowing("Segment");
            }

            return trees;
        }
    }
}
