using System;

namespace Kode {
    internal readonly struct DoubleNode : ISyntaxTreeNode {
        private const double EPSILON = 0.0000001;
        
        public DoubleToken Number { get; }

        public DoubleNode(DoubleToken number) {
            Number = number;
        }
        
        public DoubleNode(double d) {
            Number = new DoubleToken(d);
        }

        public override bool Equals(object obj) {
            return obj is DoubleNode num && Math.Abs(num.Number.Value - Number.Value) < EPSILON;
        }

        public override string ToString() {
            return $"NUMER NODE {Number}";
        }
    }
}