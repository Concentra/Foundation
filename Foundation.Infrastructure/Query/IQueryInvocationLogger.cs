using System;

namespace Foundation.Infrastructure.Query
{
    public interface IQueryInvocationLogger
    {
        void Log(Guid invocationId, DateTime startTime, string managerName, string methodName, string parametersJson, string userIdentifier);
    }
}