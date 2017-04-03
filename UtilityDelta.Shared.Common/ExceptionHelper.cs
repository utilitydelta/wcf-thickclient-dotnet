using System;

namespace UtilityDelta.Shared.Common
{
    public static class ExceptionHelper
    {
        public static Exception InnerMostException(this Exception exception)
        {
            var ex = exception;
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex;
        }
    }
}