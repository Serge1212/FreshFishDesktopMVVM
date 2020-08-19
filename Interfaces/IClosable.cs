using System;
using System.Collections.Generic;
using System.Text;

namespace FreshFishDesktopMVVM.Interfaces
{
    public interface IClosable
    {
        public Action Close { get; set; }
    }
}
