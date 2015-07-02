using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;

namespace UpperMachine
{
    public class Widget : DockContent
    {
        public string ID { get; set; }

        public virtual void setMaster(frmMain master)
        {
            throw new NotImplementedException("A widget must implement 'setMaster' method.");
        }

        protected override string GetPersistString()
        {
            if (ID.Length < 2)
                throw new NotImplementedException("A widget's ID must be longer than 2 chars.");
            else
                return ID;
        }
    }
}
