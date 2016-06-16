using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoD
{
    abstract class GameObject
    {
        public GameObject(char icon)
        {
            this.Icon = icon;
        }
        public char Icon { get; private set; }
    }
}
