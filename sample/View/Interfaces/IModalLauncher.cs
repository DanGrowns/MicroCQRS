using System;
using TinyCqrs.Interfaces;

namespace View.Interfaces
{
    public interface IModalLauncher
    {
        Action<ICmdResult> Display { get; set; }
    }
}