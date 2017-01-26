using System;

namespace Blobr
{
    public class BlobrLoadException : Exception
    {
        public BlobrLoadException(string message)
            : base (message)
            {
                
            }
    }
}