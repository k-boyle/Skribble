using System;

namespace Kode {
    internal readonly struct CosineToken : IUnaryOperatorToken {
        public static readonly CosineToken Instance = new CosineToken();

        public double Apply(double number) {
            return Math.Cos(number / 180 * Math.PI);
        }

        public override string ToString() {
            return "UNARY COS";
        }
    }
}