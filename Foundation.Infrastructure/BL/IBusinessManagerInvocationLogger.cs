using System;

namespace Foundation.Infrastructure.BL
{
    public interface IBusinessManagerInvocationLogger
    {
        void Log(Guid invocationId, DateTime startTime, string managerName, string methodName, string parametersJson, string userIdentifier);
    }
}