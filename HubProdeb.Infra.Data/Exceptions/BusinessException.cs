using System;
using System.Collections.Generic;
using System.Text;

namespace MeusJogos.Infra.Data.Exceptions
{
    [Serializable]
    public class BusinessException : GlobalException
    {
        public BusinessException()
        {
        }

        public BusinessException(string message) : base(message)
        {
        }

        protected BusinessException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)

        {
        }

        public BusinessException(string message, Exception innerException)
             : base(message, innerException)
        {
        }
    }
}
