using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TBPP14200
{
    public partial class BoardTable : BasePackageF
    {
        public BoardTable()
        {
            InitializeComponent();
            //定義表格名稱，會影響Log記錄內的DataSource名稱
            ModuleName = "BoardTable";
            FolderPath = SYSPara.SysDir + @"\LocalData\BoardTable\";
        }
    }
}
