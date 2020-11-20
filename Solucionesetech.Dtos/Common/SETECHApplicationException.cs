using System;

using System.Runtime.Serialization;

namespace Solucionesetech.Dtos.Common
{
    [Serializable]
   public  class SETECHApplicationException : Exception
    {
        public SETECHApplicationException()
        {

        }

        public SETECHApplicationException(string message)
            : base(message)
        {

        }
    }
}
