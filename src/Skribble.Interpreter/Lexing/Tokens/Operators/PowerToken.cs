using System;

namespace Skribble {
    internal readonly struct PowerToken : IBinaryOperatorToken {
        public static readonly PowerToken Instance = new PowerToken();

        public double Calculate(double left, double right) {
            return Math.Pow(left, right);
        }

        public override string ToString() {
            return "OPERATOR POWER";
        }
    }
}