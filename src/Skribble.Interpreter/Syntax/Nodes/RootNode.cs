using System.Collections.Generic;

namespace Skribble {
    internal readonly struct RootNode : ISyntaxTreeNode {
        public IEnumerable<ISyntaxTreeNode> ChildNodes { get; }

        public RootNode(IEnumerable<ISyntaxTreeNode> childNodes) {
            ChildNodes = childNodes;
        }
        
        public RootNode(params ISyntaxTreeNode[] childNodes) {
            ChildNodes = childNodes;
        }

        public override bool Equals(object obj) {
            if (obj is RootNode other) {
                using var enumer1 = ChildNodes.GetEnumerator();
                using var enumer2 = other.ChildNodes.GetEnumerator();
                while (enumer1.MoveNext() && enumer2.MoveNext()) {
                    if (!enumer1.Current!.Equals(enumer2.Current)) {
                        return false;
                    }
                }

                return !enumer1.MoveNext() && !enumer2.MoveNext();
            }

            return false;
        }

        public override string ToString() {
            return "ROOT";
        }
    }
}