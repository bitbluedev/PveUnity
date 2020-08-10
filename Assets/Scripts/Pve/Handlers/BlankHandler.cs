using Pve.Handlers;
using Pve.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Pve.Handlers
{
    class BlankHandler : StateHandlerBase
    {
        public override void Execute()
        {
            World.Text = "BLANK_HANDLER_PLACEHOLDER_TEXT";
        }
    }
}
