using System;

namespace Foundation.Persistence
{
    public interface ITypeHolder 
    {
        Type HookType { get;}

        string NameSpace { get; }
    }
}