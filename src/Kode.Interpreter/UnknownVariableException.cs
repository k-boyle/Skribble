using System;

namespace Kode {
    public class UnknownVariableException : Exception {
        internal UnknownVariableException(string name) : base($"No variable {name} declared"){
            
        }
    }
}