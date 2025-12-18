using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaeLibGeneral
{
    /// <summary>
    /// 流程節點類別(從0開始)，主要目的為能延續C++習慣的別名用法
    /// </summary>
    /// <example>
    /// 一般常用作法範例：
    /// <code>
    /// //建立 JActionTask 物件 autorunTask
    /// JActionTask autorunTask = new JActionTask();
    /// autorunTask.Reset();
    /// ...
    /// //於 switch...case... 流程中使用
    /// public bool DoSomthing()
    /// {
    ///     bool bRet = false;
    ///     int task = autorunTask;
    ///     switch(task.Value)
    ///     {
    ///         case 0:
    ///         {
    ///             ...
    ///             task.Next();
    ///         }
    ///         break;
    ///         case 1:
    ///         {
    ///             if(...)
    ///             {
    ///                 task.Next();
    ///             }
    ///             else
    ///             {
    ///                 task.Next(0);
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
    /// </code>
    /// </example>
    public class JActionTask
    {
        private int task = 0;

        /// <summary>
        /// 建構子
        /// </summary>
        public JActionTask()
        {
            Reset();
        }

        /// <summary>
        /// 流程節點 (唯讀)
        /// </summary>
        /// <value>
        /// The task.
        /// </value>
        public int Value
        {
            get
            {
                return task;
            }
        }

        /// <summary>
        /// 重置流程節點(task=0)
        /// </summary>
        public void Reset()
        {
            task = 0;
        }

        /// <summary>
        /// 前進下一流程節點(task++)
        /// </summary>
        public void Next()
        {
            task++;
        }

        /// <summary>
        /// 前進到指定的流程節點(task=nt)
        /// </summary>
        /// <param name="nt">指定的流程節點</param>
        public void Next(int nt)
        {
            task = Math.Max(-1, nt);
        }

        /// <summary>
        /// 前進到指定的流程節點(task=nt)
        /// </summary>
        /// <param name="nt">指定的流程節點</param>
        public void Jump(int nt)
        {
            Next(nt);
        }
    }
}