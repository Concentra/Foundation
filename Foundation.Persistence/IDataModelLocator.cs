using System;

namespace Foundation.Persistence
{
    public interface IDataModelLocator 
    {
        Type HookType { get;}

        string NameSpace { get; }
    }
}