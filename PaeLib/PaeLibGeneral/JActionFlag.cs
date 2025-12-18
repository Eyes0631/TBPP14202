using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaeLibGeneral
{
    /// <summary>
    /// 流程交握旗標類別
    /// <p>觸發者 : Reset()  -> DoIt() -> IsDone() ------------------> NEXT STEP</p>
    /// <p>被觸發 : IsDoIt() -----------> Doing()  -> ... -> Done() -> BACK TO START</p>
    /// </summary>
    /// <example>
    /// <code>
    /// //建立 JActionTask 物件 process1Task
    /// JActionTask process1Task = new JActionTask();
    /// //建立 JActionTask 物件 process2Task
    /// JActionTask process2Task = new JActionTask();
    /// //建立 JActionFlag 物件 doSomething
    /// JActionFlag doSomething = new JActionFlag();
    /// doSomething.Reset();
    /// ...
    /// //流程一
    /// public void ResetProcess1()
    /// {
    ///     process1Task.Reset();
    /// }
    /// public bool Process1()
    /// {
    ///     bool bRet = false;
    ///     int task = process1Task;
    ///     switch(task.Value)
    ///     {
    ///         case 0:
    ///         {
    ///             if(doSomething.IsDoing() == false)
    ///             {
    ///                 doSomething.DoIt();
    ///                 task.Next();
    ///             }
    ///         }
    ///         break;
    ///         case 1:
    ///         {
    ///             if(doSomething.IsDone())
    ///             {
    ///                 task.Next();
    ///             }
    ///         }
    ///         break;
    ///         case 2:
    ///         {
    ///             //Done
    ///             bRet = true;
    ///         }
    ///         break;
    ///         default:
    ///         {
    ///             //throw exception...
    ///         }
    ///         break;
    ///     }
    ///     return bRet;
    /// }
    /// ...
    /// //流程二
    /// public void ResetProcess2()
    /// {
    ///     process2Task.Reset();
    /// }
    /// public bool Process2()
    /// {
    ///     bool bRet = false;
    ///     int task = process2Task;
    ///     switch(task.Value)
    ///     {
    ///         case 0:
    ///         {
    ///             if(doSomething.IsDoIt())
    ///             {
    ///                 doSomething.Doing();
    ///                 task.Next();
    ///             }
    ///         }
    ///         break;
    ///         case 1:
    ///         {
    ///             //Do Something
    ///             ...
    ///             //Somethiong Done
    ///             doSomething.Done();
    ///             task.Next();
    ///         }
    ///         break;
    ///         case 2:
    ///         {
    ///             //Done
    ///             bRet = true;
    ///         }
    ///         break;
    ///         default:
    ///         {
    ///             //throw exception...
    ///         }
    ///         break;
    ///     }
    ///     return bRet;
    /// }
    /// </code>
    /// </example>
    public class JActionFlag
    {
        private bool Flag_DoSth = false;
        private bool Flag_DoSthOK = false;
        private bool Flag_Doing = false;

        /// <summary>
        /// 建構子
        /// </summary>
        public JActionFlag()
        {
            Reset();
        }

        /// <summary>
        /// 重置所有流程旗標變數
        /// </summary>
        public void Reset()
        {
            Flag_DoSth = false;
            Flag_DoSthOK = false;
            Flag_Doing = false;
        }

        /// <summary>
        /// 判斷是否需要執行流程，供動作流程本身判斷是否外部有觸發要執行流程。(被觸發者使用)
        /// </summary>
        /// <returns>
        ///   <c>true</c> 需要執行; 否則, <c>false</c>.
        /// </returns>
        public bool IsDoIt()
        {
            return Flag_DoSth;
        }

        /// <summary>
        /// 判斷流程是否執行中，供外部觸發者判斷要觸發的是否執行中。(觸發者使用)
        /// </summary>
        /// <returns>
        ///   <c>true</c> 執行中; 否則, <c>false</c>.
        /// </returns>
        public bool IsDoing()
        {
            return Flag_Doing;
        }

        /// <summary>
        /// 判斷流程是否完成，供外部觸發者判斷觸發執行的流程是否已完成。(觸發者使用)
        /// </summary>
        /// <returns>
        ///   <c>true</c> 完成; 否則, <c>false</c>.
        /// </returns>
        public bool IsDone()
        {
            return Flag_DoSthOK;
        }

        /// <summary>
        /// 通知執行流程，供外部觸發者觸發執行流程使用。(觸發者使用)
        /// </summary>
        public void DoIt()
        {
            Flag_DoSth = true;
            Flag_DoSthOK = false;
            Flag_Doing = false;
        }

        /// <summary>
        /// 通知流程執行中，供動作流程本身設定流程旗標狀態為執行中使用。(被觸發者使用)
        /// </summary>
        public void Doing()
        {
            Flag_DoSth = false;
            Flag_DoSthOK = false;
            Flag_Doing = true;
        }

        /// <summary>
        /// 通知流程執行完成，供動作流程本身設定流程旗標狀態為已完成使用。(被觸發者使用)
        /// </summary>
        public void Done()
        {
            Flag_DoSth = false;
            Flag_DoSthOK = true;
            Flag_Doing = false;
        }
    }
}