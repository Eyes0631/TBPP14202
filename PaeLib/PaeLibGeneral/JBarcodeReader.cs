using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;   //for COM 元件
using PaeLibGeneral;

namespace PaeLibGeneral
{
    internal interface IJBarcodeReader
    {
        //Disable Auto Trigger
        bool DisableAutoTrigger();

        //Enable the laser
        bool Enable();

        //Disable the laser
        bool Disable();

        //Trigger the reader
        bool Trigger();

        //De-trigger the reader
        bool DeTrigger();

        //Get the barcode
        bool GetResult(ref string barcode);
    }

    /// <summary>
    /// 條碼槍類別，目前支援條碼槍廠牌如下：
    /// <list type="number">
    /// <item><description>CIPHER_LAB</description></item>
    /// <item><description>OPTICON_NLV</description></item>
    /// </list>
    /// </summary>
    public class JBarcodeReader
    {
        private JBarcodeReaderAbstract _barcodeReader = null;
        private JTimer _tm = new JTimer(50);
        private int _retryCount = 0;
        private string _barcode = string.Empty;

        /// <summary>
        /// 建構子
        /// </summary>
        public JBarcodeReader()
        {
        }

        /// <summary>
        /// 開啟條碼槍
        /// </summary>
        /// <param name="type">條碼槍樣式(1:CIPHER_LAB, 2:OPTICON_NLV)</param>
        /// <param name="comport">條碼槍序列埠埠號(COM)</param>
        /// <returns>
        ///     <c>true</c>：開啟成功
        ///     <c>false</c>：開啟失敗
        /// </returns>
        public bool Open(byte type, byte comport)
        {
            Close();

            switch (type)
            {
                case 1: //CIPHER_LAB
                    {
                        _barcodeReader = new JBarcodeReader_CipherLab();
                    }
                    break;

                case 2: //OPTICON_NLV
                    {
                        _barcodeReader = new JBarcodeReader_OpticonNLV();
                    }
                    break;
            }

            if (_barcodeReader != null)
            {
                if (_barcodeReader.Open(comport))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 關閉條碼槍
        /// </summary>
        public void Close()
        {
            if (_barcodeReader != null)
            {
                _barcodeReader.Close();
                _barcodeReader = null;
            }
        }

        private JActionTask ReadBarcodeTask = new JActionTask();

        /// <summary>
        /// 重置讀取條碼流程
        /// </summary>
        public void ReadBarcodeTaskReset()
        {
            ReadBarcodeTask.Reset();
            //_tm.Reset();
            _retryCount = 0;
        }

        /// <summary>
        /// 讀取條碼
        /// </summary>
        /// <returns>
        /// <list type="table">
        /// <item><term><c>#BUSY#</c></term><description>BUSY (讀取中)</description></item>
        /// <item><term><c>!ERROR!</c></term><description>ERROR (讀取失敗)</description></item>
        /// <item><term><c>!TIMEOUT!</c></term><description>TIMEOUT (讀取逾時)</description></item>
        /// <item><term><c>string</c></term><description>barcode (條碼內容)</description></item>
        /// </list>
        /// </returns>
        public string ReadBarcode()
        {
            string Ret = "#BUSY#";

            //Scan the barcode
            if (_barcodeReader.Active)
            {
                JActionTask Task = ReadBarcodeTask;
                switch (Task.Value)
                {
                    case 0: //reserve
                        {
                            //_tm.Reset();
                            Task.Next();
                        }
                        break;

                    case 1: //Delay 50 ms
                        {
                            if (_tm.Count(50))
                            {
                                Task.Next();
                            }
                        }
                        break;

                    case 2: //Disable Auto Trigger
                        {
                            if (_barcodeReader.DisableAutoTrigger())
                            {
                                //_tm.Reset();
                                Task.Next();
                            }
                            else
                            {
                                Ret = "!ERROR!";
                                Task.Next(99);
                            }
                        }
                        break;

                    case 3: //Delay 50 ms
                        {
                            if (_tm.Count(50))
                            {
                                Task.Next();
                            }
                        }
                        break;

                    case 4: //Enable the laser
                        {
                            if (_barcodeReader.Enable())
                            {
                                //_tm.Reset();
                                Task.Next();
                            }
                            else
                            {
                                Ret = "!ERROR!";
                                Task.Next(99);
                            }
                        }
                        break;

                    case 5: //Delay 50 ms
                        {
                            if (_tm.Count(50))
                            {
                                Task.Next();
                            }
                        }
                        break;

                    case 6: //De-trigger the reader
                        {
                            if (_barcodeReader.DeTrigger())
                            {
                                //_tm.Reset();
                                Task.Next();
                            }
                            else
                            {
                                Ret = "!ERROR!";
                                Task.Next(99);
                            }
                        }
                        break;

                    case 7: //Delay 50 ms
                        {
                            if (_tm.Count(50))
                            {
                                Task.Next();
                            }
                        }
                        break;

                    case 8: //Trigger the reader
                        {
                            if (_barcodeReader.Trigger())
                            {
                                //_tm.Reset();
                                Task.Next();
                            }
                            else
                            {
                                Ret = "!ERROR!";
                                Task.Next(99);
                            }
                        }
                        break;

                    case 9: //Delay 50 ms
                        {
                            if (_tm.Count(50))
                            {
                                //_tm.Reset();
                                Task.Next();
                            }
                        }
                        break;

                    case 10: //Get the barcode
                        {
                            bool bRet = _barcodeReader.GetResult(ref _barcode);
                            if (bRet && string.IsNullOrWhiteSpace(_barcode).Equals(false))
                            {
                                //_tm.Reset();
                                Task.Next();
                            }
                            else
                            {
                                if (_tm.Count(1000))
                                {
                                    _retryCount++;
                                    if (_retryCount < 3)
                                    {
                                        //Retry
                                        Task.Next(2);
                                    }
                                    else
                                    {
                                        //Timeout
                                        Ret = "!TIMEOUT!";
                                        Task.Next(99);
                                    }
                                }
                            }
                        }
                        break;

                    case 11: //Delay 50 ms
                        {
                            if (_tm.Count(50))
                            {
                                Task.Next();
                            }
                        }
                        break;

                    case 12: //De-trigger the reader
                        {
                            if (_barcodeReader.DeTrigger())
                            {
                                //_tm.Reset();
                                Task.Next();
                            }
                            else
                            {
                                Ret = "!ERROR!";
                                Task.Next(99);
                            }
                        }
                        break;

                    case 13: //Delay 50 ms
                        {
                            if (_tm.Count(50))
                            {
                                Task.Next();
                            }
                        }
                        break;

                    case 14: //Disable the laser
                        {
                            if (_barcodeReader.Disable())
                            {
                                //_tm.Reset();
                                Task.Next();
                            }
                            else
                            {
                                Ret = "!ERROR!";
                                Task.Next(99);
                            }
                        }
                        break;

                    case 15: //Delay 50 ms
                        {
                            if (_tm.Count(50))
                            {
                                Task.Next();
                            }
                        }
                        break;

                    case 16: //Return Done
                        {
                            Ret = _barcode;
                            Task.Next(99);
                        }
                        break;

                    case 99: //End
                        {
                            //Do nothing.
                        }
                        break;
                }
            }

            return Ret;
        }
    }

    //抽象類別
    internal abstract class JBarcodeReaderAbstract : IJBarcodeReader
    {
        protected JComport _barcodeReader = null;

        private bool m_Active = false;
        public bool Active { get { return m_Active; } }   //判斷條碼槍是否已啟用(Read Only)

        public JBarcodeReaderAbstract()
        {
            _barcodeReader = new JComport();
        }

        public bool Open(byte portNum, byte end = 13, int baudRate = 9600, Parity parity = Parity.None,
            int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            //bool bRet = false;
            if (_barcodeReader != null)
            {
                m_Active = _barcodeReader.Open(portNum, end, baudRate, parity, dataBits, stopBits);
            }
            return m_Active;
        }

        public void Close()
        {
            _barcodeReader.Dispose();
        }

        public virtual bool DisableAutoTrigger()
        {
            return true;
        }

        public abstract bool Enable();

        public abstract bool Disable();

        public abstract bool Trigger();

        public abstract bool DeTrigger();

        //public abstract bool GetResult(ref string barcode);
        public bool GetResult(ref string barcode)
        {
            bool bRet = false;

            if (_barcodeReader != null)
            {
                byte[] ret = new byte[0];
                if (_barcodeReader.GetResult(ref ret))
                {
                    barcode = Encoding.ASCII.GetString(ret);
                    bRet = true;
                }
            }

            return bRet;
        }
    }

    internal class JBarcodeReader_CipherLab : JBarcodeReaderAbstract
    {
        //public JBarcodeReader_CipherLab()
        //{
        //}

        //public override bool DisableAutoTrigger()
        //{
        //    bool bRet = false;

        //    if (_barcodeReader != null)
        //    {
        //        //N/A
        //        bRet = true;
        //    }

        //    return bRet;
        //}

        public override bool Enable()
        {
            bool bRet = false;

            if (_barcodeReader != null)
            {
                //<E> : Disable
                _barcodeReader.SendData(new byte[] { 0x45 });
                bRet = true;
            }

            return bRet;
        }

        public override bool Disable()
        {
            bool bRet = false;

            if (_barcodeReader != null)
            {
                //<D> : Disable
                _barcodeReader.SendData(new byte[] { 0x44 });
                bRet = true;
            }

            return bRet;
        }

        public override bool Trigger()
        {
            bool bRet = false;

            if (_barcodeReader != null)
            {
                //<#><@><T><R><I><G><O><N><CR> : Trigger On
                _barcodeReader.SendData(new byte[] { 0x23, 0x40, 0x54, 0x52, 0x49, 0x47, 0x4f, 0x4e, 0x0d });
                bRet = true;
            }

            return bRet;
        }

        public override bool DeTrigger()
        {
            bool bRet = false;

            if (_barcodeReader != null)
            {
                //<#><@><T><R><I><G><O><F><F><CR> : Trigger Off
                _barcodeReader.SendData(new byte[] { 0x23, 0x40, 0x54, 0x52, 0x49, 0x47, 0x4f, 0x46, 0x46, 0x0d });
                bRet = true;
            }

            return bRet;
        }
    }

    internal class JBarcodeReader_OpticonNLV : JBarcodeReaderAbstract
    {
        public override bool DisableAutoTrigger()
        {
            bool bRet = false;

            if (_barcodeReader != null)
            {
                //<ESC><+><F><CR> : Disable auto trigger
                _barcodeReader.SendData(new byte[] { 0x1b, 0x2b, 0x46, 0x0d });
                bRet = true;
            }

            return bRet;
        }

        public override bool Enable()
        {
            bool bRet = false;

            if (_barcodeReader != null)
            {
                //<ESC><Q><CR> : Enable the laser
                _barcodeReader.SendData(new byte[] { 0x1b, 0x51, 0x0d });
                bRet = true;
            }

            return bRet;
        }

        public override bool Disable()
        {
            bool bRet = false;

            if (_barcodeReader != null)
            {
                //<ESC><P><CR> : Disable the laser
                _barcodeReader.SendData(new byte[] { 0x1b, 0x50, 0x0d });
                bRet = true;
            }

            return bRet;
        }

        public override bool Trigger()
        {
            bool bRet = false;

            if (_barcodeReader != null)
            {
                //<ESC><Z><CR> : Trigger the reader
                _barcodeReader.SendData(new byte[] { 0x1b, 0x5a, 0x0d });
                bRet = true;
            }

            return bRet;
        }

        public override bool DeTrigger()
        {
            bool bRet = false;

            if (_barcodeReader != null)
            {
                //<ESC><Y><CR> : De-trigger the reader
                _barcodeReader.SendData(new byte[] { 0x1b, 0x59, 0x0d });
                bRet = true;
            }

            return bRet;
        }
    }
}