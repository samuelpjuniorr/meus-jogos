using System;
using System.Collections.Generic;
using System.Text;

namespace MeusJogos.Infra.Data.Exceptions
{
    [Serializable]
    public class GlobalException : Exception
    {
        public GlobalException()
        {
        }
        public GlobalException(string message) : base(message)
        {

        }

        protected GlobalException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)

        {
        }

        public GlobalException(string message, Exception innerException)
             : base(message, innerException)
        {
        }


        public static void ReturnException(Exception ex)
        {

            switch (ex.GetType().Name)
            {
                case nameof(BusinessException):

                    throw new BusinessException(ex.Message);

                default:
                    throw ex;

            }
        }

    }
}
