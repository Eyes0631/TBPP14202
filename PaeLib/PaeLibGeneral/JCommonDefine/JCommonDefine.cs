using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaeLibGeneral
{
    /// <summary>
    /// 三值邏輯 three-valued logic (使用於基礎元件, JDI, JCY, JVCC, JNOZ...etc)
    /// <para>0 : Unknown, None, Wait, Busy</para>
    /// <para>1 : True, On, Done, OK</para>
    /// <para>2 : False, Off, Timeout, NG</para>
    /// </summary>
    /// <remarks>
    /// 2019-06-03 將列舉名稱 TripletRestult 改為 ThreeValued
    /// </remarks>
    public enum ThreeValued
    {
        /// <summary>
        /// Unknown, None, Wait, Busy
        /// </summary>
        UNKNOWN = 0,

        /// <summary>
        /// True, On, Done, OK
        /// </summary>
        TRUE = 1,

        /// <summary>
        /// False, Off, Timeout, NG
        /// </summary>
        FALSE = 2,
    }

    /// <summary>
    /// 數量計數類別
    /// </summary>
    public class QtyCounter
    {
        private long m_count = 0;

        /// <summary>
        /// 建構子
        /// </summary>
        public QtyCounter()
        {
            m_count = 0;
        }

        /// <summary>
        /// 取得總數量 (唯讀)
        /// </summary>
        public long Qty { get { return m_count; } }

        /// <summary>
        /// 重置數量 (總數量歸零)
        /// </summary>
        public void Reset()
        {
            m_count = 0;
        }

        /// <summary>
        /// 加入數量
        /// </summary>
        /// <param name="count">增加量</param>
        /// <returns>總數量</returns>
        public long Add(long count)
        {
            m_count += count;
            return m_count;
        }
    }
}