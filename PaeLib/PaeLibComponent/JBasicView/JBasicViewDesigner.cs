using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using System.Threading.Tasks;

using System.ComponentModel;

//using System.ComponentModel.Design;
using System.Windows.Forms.Design;

//using System.Windows.Forms;

namespace PaeLibComponent
{
    /// <summary>
    /// 提供CustomUserControl在Design-Time過濾屬性以及事件的類別
    /// </summary>
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class JBasicViewDesigner : ParentControlDesigner
    {
        //public override void Initialize(IComponent c)
        //{
        //    base.Initialize(c);
        //}

        /// <summary>
        /// 設定不過濾的屬性
        /// </summary>
        private static String[] EnabledProperty = new string[] {
            "TabIndex",
            "Margin",
            //"BackColor",
            "Font",
            "Size",
            "Location",
            "Name",
        };

        /// <summary>
        /// 過濾屬性
        /// </summary>
        /// <param name="properties"></param>
        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            //針對UserControl每個屬性進行過濾
            foreach (var pi in typeof(System.Windows.Forms.UserControl).GetProperties())
            {
                if (properties.Contains(pi.Name))
                {
                    if (EnabledProperty.Contains(pi.Name) == false)
                    {
                        properties.Remove(pi.Name);
                    }
                }
            }

            base.PreFilterProperties(properties);
        }

        protected override void PreFilterEvents(System.Collections.IDictionary events)
        {
            //針對UserControl每個事件進行過濾
            foreach (var ei in typeof(System.Windows.Forms.UserControl).GetEvents())
            {
                if (events.Contains(ei.Name))
                    events.Remove(ei.Name);
            }

            base.PreFilterEvents(events);
        }
    }
}