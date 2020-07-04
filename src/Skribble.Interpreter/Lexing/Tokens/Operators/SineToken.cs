using System;

namespace Skribble {
    internal readonly struct SineToken : IUnaryOperatorToken {
        public static readonly SineToken Instance = new SineToken();

        public double Apply(double number) {
            return Math.Sin(number / 180 * Math.PI);
        }

        public override string ToString() {
            return "UNARY SIN";
        }
    }
}