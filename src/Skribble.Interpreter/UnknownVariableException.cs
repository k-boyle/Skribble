using System;

namespace Skribble {
    public class UnknownVariableException : Exception {
        internal UnknownVariableException(string name) : base($"No variable {name} declared"){
            
        }
    }
}