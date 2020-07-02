﻿namespace Kode {
    internal readonly struct MultiplicationToken : IOperatorToken {
        public static readonly MultiplicationToken Instance = new MultiplicationToken();

        public long Calculate(long left, long right) {
            return unchecked(left * right);
        }

        public double Calculate(double left, double right) {
            return left * right;
        }

        public double Calculate(long left, double right) {
            return left * right;
        }

        public double Calculate(double left, long right) {
            return left * right;
        }

        public override string ToString() {
            return "OPERATOR MULTIPLICATION";
        }
    }
}