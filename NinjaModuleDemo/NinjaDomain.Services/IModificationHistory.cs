using System;

namespace NinjaDomain.Services
{
    public interface IModificationHistory 
    {
        DateTime DateModified{ get; set; }
        DateTime DateCreated { get; set; }
        bool IsDirty { get; set; }
    }
}
