using System;

namespace Skribble {
    internal readonly struct TangentToken : IUnaryOperatorToken {
        public static readonly TangentToken Instance = new TangentToken();
        
        public double Apply(double number) {
            return Math.Tan(number / 180 * Math.PI);
        }

        public override string ToString() {
            return "UNARY TAN";
        }
    }
}