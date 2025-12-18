using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Threading;
using System.Runtime.InteropServices;   //for COM 元件

namespace PaeLibGeneral
{
    internal class MemoryMappedField
    {
        public string FieldName;    //欄位名稱
        public int Offset;          //欄位起始位址
        public int Size;            //欄位資料長度
    }

    /// <summary>
    /// 共享記憶體管理類別
    /// <para>使用注意事項：使用者雙方除了 ShareMemory 的名稱需一致外，增加欄位的順序與欄位大小亦須一致，以免發生資料存取位址錯誤的問題。</para>
    /// </summary>
    /// <example>
    /// 基本用法說明：
    /// <code>
    /// //建立 JMemoryMappedManager 物件 sm
    /// JMemoryMappedManager sm = new JMemoryMappedManager();
    ///
    /// //加入欄位
    /// sm.AddField("CMD", 4);  //加入整數欄位
    /// sm.AddField("Message", 256);    //加入字串欄位，長度為256字元
    ///
    /// //開啟共享記憶體 SM_TEST
    /// sm.Open("SM_TEST");     //開啟共享記憶體SM_TEST
    ///
    /// //讀寫欄位資料
    /// sm.WriteInt("CMD", 87); //寫入整數 87
    /// sm.ReadInt("CMD");  //return : 87
    /// sm.WriteStr("Message", "Any message what you want to say, but less than 256 words.");   //寫入訊息
    /// am.ReadStr("Message");  //return : "Any message what you want to say, but less than 256 words."
    ///
    /// //關閉共享記憶體 SM_TEST
    /// sm.Close();     //非必要，程式關閉會自動釋放資源
    /// </code>
    /// </example>
    public class JMemoryMappedManager : IDisposable
    {
        private JMemoryMapped mm_ShareMem = null;
        private List<MemoryMappedField> mmf_ShareMemFields = null;

        private bool m_IsShareMemOpen = false;

        /// <summary>
        /// 建構子
        /// </summary>
        public JMemoryMappedManager()
        {
            mm_ShareMem = new JMemoryMapped();
            mmf_ShareMemFields = new List<MemoryMappedField>();
        }

        /// <summary>
        /// 釋放 JMemoryMappedManager 所使用的資源
        /// </summary>
        public void Dispose()
        {
            Close();
        }

        /// <summary>
        /// 新增記憶體欄位資料
        /// </summary>
        /// <param name="_name">記憶體欄位名稱</param>
        /// <param name="_size">記憶體欄位大小</param>
        /// <returns>
        ///     <c>1</c>：新增欄位完成
        ///     <c>0</c>：共享記憶體已開啟，無法再新增欄位
        ///     <c>-1</c>：欄位名稱重複
        /// </returns>
        public int AddField(string _name, int _size)
        {
            if (!m_IsShareMemOpen)
            {
                if (mmf_ShareMemFields.FindIndex(x => x.FieldName.Equals(_name)) < 0)
                {
                    int shareMemOffset = mmf_ShareMemFields.Sum(x => x.Size);   //新欄位位址 = 目前所有欄位記憶體大小的總和
                    mmf_ShareMemFields.Add(new MemoryMappedField { FieldName = _name, Offset = shareMemOffset, Size = _size });
                    return 1;   //新增欄位完成
                }
                else
                {
                    return -1;  //欄位名稱重複
                }
            }
            return 0;   //共享記憶體已開啟，無法再新增欄位
        }

        /// <summary>
        /// 開啟共享記憶體
        /// </summary>
        /// <param name="_name">指定共享記憶體名稱</param>
        /// <returns>
        ///     <c>true</c>：開啟成功
        ///     <c>true</c>：開啟失敗
        /// </returns>
        public bool Open(string _name)
        {
            bool bRet = false;
            int shareMemSize = mmf_ShareMemFields.Sum(x => x.Size);
            if (shareMemSize > 0)
            {
                bRet = mm_ShareMem.OpenMem(_name, shareMemSize);
                m_IsShareMemOpen = bRet;
            }
            return bRet;
        }

        /// <summary>
        /// 關閉共享記憶體
        /// </summary>
        public void Close()
        {
            mm_ShareMem.CloseMem();
        }

        /// <summary>
        /// 讀取位元組陣列資料
        /// </summary>
        /// <param name="_name">欄位名稱</param>
        /// <param name="_data">接收資料之位元組陣列</param>
        /// <returns>
        ///     <c>true</c>：讀取成功
        ///     <c>false</c>：讀取失敗
        /// </returns>
        public bool ReadBytes(string _name, ref byte[] _data)
        {
            bool bRet = false;

            MemoryMappedField mmf = mmf_ShareMemFields.Find(x => x.FieldName.Equals(_name));
            if ((mmf != null) && (mmf.Size > 0))
            {
                bRet = mm_ShareMem.ReadBytes(ref _data, mmf.Offset, mmf.Size);
            }

            return bRet;
        }

        /// <summary>
        /// 寫入位元組陣列資料
        /// </summary>
        /// <param name="_name">欄位名稱</param>
        /// <param name="_data">寫入資料之位元組陣列</param>
        /// <returns>
        ///     <c>true</c>：寫入成功
        ///     <c>false</c>：寫入失敗
        /// </returns>
        public bool WriteBytes(string _name, byte[] _data)
        {
            bool bRet = false;

            MemoryMappedField mmf = mmf_ShareMemFields.Find(x => x.FieldName.Equals(_name));
            if ((mmf != null) && (mmf.Size > 0))
            {
                bRet = mm_ShareMem.WriteBytes(_data, mmf.Offset, mmf.Size);
            }

            return bRet;
        }

        /// <summary>
        /// 讀取位元組資料
        /// </summary>
        /// <param name="_name">欄位名稱</param>
        /// <param name="_data">接收資料之位元組</param>
        /// <returns>
        ///     <c>true</c>：讀取成功
        ///     <c>false</c>：讀取失敗
        /// </returns>
        public bool ReadByte(string _name, ref byte _data)
        {
            bool bRet = false;

            MemoryMappedField mmf = mmf_ShareMemFields.Find(x => x.FieldName.Equals(_name));
            if ((mmf != null) && (mmf.Size.Equals(sizeof(byte))))
            {
                byte[] dataArray = new byte[mmf.Size];
                bRet = ReadBytes(_name, ref dataArray);
                if (bRet)
                {
                    _data = dataArray[0];
                }
            }

            return bRet;
        }

        /// <summary>
        /// 寫入位元組資料
        /// </summary>
        /// <param name="_name">欄位名稱</param>
        /// <param name="_data">寫入資料之位元組</param>
        /// <returns>
        ///     <c>true</c>：寫入成功
        ///     <c>false</c>：寫入失敗
        /// </returns>
        public bool WriteByte(string _name, byte _data)
        {
            bool bRet = false;

            MemoryMappedField mmf = mmf_ShareMemFields.Find(x => x.FieldName.Equals(_name));
            if ((mmf != null) && (mmf.Size.Equals(sizeof(byte))))
            {
                byte[] dataArray = BitConverter.GetBytes(_data);
                bRet = WriteBytes(_name, dataArray);
            }

            return bRet;
        }

        /// <summary>
        /// 讀取字串資料
        /// </summary>
        /// <param name="_name">欄位名稱</param>
        /// <param name="_data">接收資料之字串</param>
        /// <returns>
        ///     <c>true</c>：讀取成功
        ///     <c>false</c>：讀取失敗
        /// </returns>
        public bool ReadStr(string _name, ref string _data)
        {
            bool bRet = false;

            MemoryMappedField mmf = mmf_ShareMemFields.Find(x => x.FieldName.Equals(_name));
            if ((mmf != null) && (mmf.Size > 0))
            {
                byte[] dataArray = new byte[mmf.Size];
                bRet = ReadBytes(_name, ref dataArray);
                if (bRet)
                {
                    _data = Encoding.Default.GetString(dataArray, 0, mmf.Size).Trim();    //使用系統預設編碼傳換
                }
                else
                {
                    _data = string.Empty;
                }
            }

            return bRet;
        }

        /// <summary>
        /// 寫入字串資料
        /// </summary>
        /// <param name="_name">欄位名稱</param>
        /// <param name="_data">寫入資料之字串</param>
        /// <returns>
        ///     <c>true</c>：寫入成功
        ///     <c>false</c>：寫入失敗
        /// </returns>
        public bool WriteStr(string _name, string _data)
        {
            bool bRet = false;

            MemoryMappedField mmf = mmf_ShareMemFields.Find(x => x.FieldName.Equals(_name));
            if ((mmf != null) && (mmf.Size > 0))
            {
                byte[] dataArray = Encoding.Default.GetBytes(_data.Trim());    //使用系統預設編碼傳換
                bRet = WriteBytes(_name, dataArray);
            }

            return bRet;
        }

        /// <summary>
        /// 讀取整數資料(4 bytes)
        /// </summary>
        /// <param name="_name">欄位名稱</param>
        /// <param name="_data">接收資料之整數</param>
        /// <returns>
        ///     <c>true</c>：讀取成功
        ///     <c>false</c>：讀取失敗
        /// </returns>
        public bool ReadInt(string _name, ref int _data)
        {
            bool bRet = false;

            MemoryMappedField mmf = mmf_ShareMemFields.Find(x => x.FieldName.Equals(_name));
            if ((mmf != null) && (mmf.Size.Equals(sizeof(int))))
            {
                byte[] dataArray = new byte[mmf.Size];
                bRet = ReadBytes(_name, ref dataArray);
                if (bRet)
                {
                    _data = BitConverter.ToInt32(dataArray, 0);
                }
            }

            return bRet;
        }

        /// <summary>
        /// 寫入整數資料(4 bytes)
        /// </summary>
        /// <param name="_name">欄位名稱</param>
        /// <param name="_data">寫入資料之整數</param>
        /// <returns>
        ///     <c>true</c>：讀取成功
        ///     <c>false</c>：讀取失敗
        /// </returns>
        public bool WriteInt(string _name, int _data)
        {
            bool bRet = false;

            MemoryMappedField mmf = mmf_ShareMemFields.Find(x => x.FieldName.Equals(_name));
            if ((mmf != null) && (mmf.Size.Equals(sizeof(int))))
            {
                byte[] dataArray = BitConverter.GetBytes(_data);
                bRet = WriteBytes(_name, dataArray);
            }

            return bRet;
        }

        /// <summary>
        /// 讀取短整數資料(2 bytes)
        /// </summary>
        /// <param name="_name">欄位名稱</param>
        /// <param name="_data">接收資料之短整數</param>
        /// <returns>
        ///     <c>true</c>：讀取成功
        ///     <c>false</c>：讀取失敗
        /// </returns>
        public bool ReadShort(string _name, ref short _data)
        {
            bool bRet = false;

            MemoryMappedField mmf = mmf_ShareMemFields.Find(x => x.FieldName.Equals(_name));
            if ((mmf != null) && (mmf.Size.Equals(sizeof(short))))
            {
                byte[] dataArray = new byte[mmf.Size];
                bRet = ReadBytes(_name, ref dataArray);
                if (bRet)
                {
                    _data = BitConverter.ToInt16(dataArray, 0);
                }
                //else
                //{
                //    _data = 0;
                //}
            }

            return bRet;
        }

        /// <summary>
        /// 寫入短整數資料(2 bytes)
        /// </summary>
        /// <param name="_name">欄位名稱</param>
        /// <param name="_data">寫入資料之短整數</param>
        /// <returns>
        ///     <c>true</c>：讀取成功
        ///     <c>false</c>：讀取失敗
        /// </returns>
        public bool WriteShort(string _name, short _data)
        {
            bool bRet = false;

            MemoryMappedField mmf = mmf_ShareMemFields.Find(x => x.FieldName.Equals(_name));
            if ((mmf != null) && (mmf.Size.Equals(sizeof(short))))
            {
                byte[] dataArray = BitConverter.GetBytes(_data);
                bRet = WriteBytes(_name, dataArray);
            }

            return bRet;
        }
    }

    internal class JMemoryMapped : IDisposable
    {
        private MemoryMappedFile m_ShareMem = null;
        private Mutex mShareMemMutex = null;
        private string m_MemName = string.Empty;
        private long m_MemSize = Int64.MaxValue;

        public JMemoryMapped()
        {
        }

        public void Dispose()
        {
            CloseMem();
        }

        public bool OpenMem(string _name, long _size)
        {
            bool bRet = false;
            try
            {
                m_ShareMem = MemoryMappedFile.CreateOrOpen(_name, _size, MemoryMappedFileAccess.ReadWrite);
                if (m_ShareMem != null)
                {
                    m_MemName = _name;
                    m_MemSize = _size;
                    mShareMemMutex = new Mutex(false, m_MemName + "_MUTEX");
                    if (mShareMemMutex != null)
                    {
                        bRet = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }
            return bRet;
        }

        public void CloseMem()
        {
            m_ShareMem.Dispose();
            mShareMemMutex.Dispose();
        }

        public bool ReadBytes(ref byte[] _data, int _offset, int _size)
        {
            bool bRet = false;
            mShareMemMutex.WaitOne();
            try
            {
                using (MemoryMappedViewStream stream = m_ShareMem.CreateViewStream(_offset, _size))
                {
                    using (var br = new BinaryReader(stream))
                    {
                        _data = br.ReadBytes(_size);
                        bRet = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                mShareMemMutex.ReleaseMutex();
            }
            return bRet;
        }

        public bool WriteBytes(byte[] _data, int _offset, int _size)
        {
            bool bRet = false;
            mShareMemMutex.WaitOne();
            try
            {
                byte[] dataArray = new byte[_size];
                dataArray.Initialize();
                int dataArrayLength = Math.Min(_data.Length, _size);
                Array.Copy(_data, dataArray, dataArrayLength);
                using (MemoryMappedViewStream stream = m_ShareMem.CreateViewStream(_offset, _size))
                {
                    using (var bw = new BinaryWriter(stream))
                    {
                        bw.Write(dataArray);
                        bRet = true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                mShareMemMutex.ReleaseMutex();
            }
            return bRet;
        }
    }
}