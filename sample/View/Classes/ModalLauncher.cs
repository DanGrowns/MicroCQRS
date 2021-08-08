using System;
using TinyCqrs.Interfaces;
using View.Interfaces;

namespace View.Classes
{
    public class ModalLauncher : IModalLauncher
    {
        public Action<ICmdResult> Display { get; set; }
    }
}