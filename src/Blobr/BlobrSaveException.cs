using System;

namespace Blobr
{
    public class BlobrSaveException : Exception
    {
        public BlobrSaveException(string message)
            :base (message)
            {
                
            }
    }
}