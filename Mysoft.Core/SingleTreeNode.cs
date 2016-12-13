using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Core
{
    public class SingleTreeNode<TNode,TKey>
    {
        public TKey Id { get; set; }

        public TNode Node { get; set; }

        public SingleTreeNode<TNode, TKey> Parent { get; set; }

        public List<SingleTreeNode<TNode,TKey>> Children { get; set; }
        
    }

    
}
