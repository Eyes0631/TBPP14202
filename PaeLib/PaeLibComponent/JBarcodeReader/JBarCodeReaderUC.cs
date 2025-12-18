using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PaeLibGeneral;

namespace PaeLibComponent
{
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // 元件關不掉的問題，處理方式：
    //   在 JBarCodeReader.Designer.cs 檔案中的 protected override void Dispose(bool disposing){ } 函式中，
    //   加入 _barcodeReader.Close();     //釋放 JBarcodeReaderAbstract 資源 (否則程式會關不掉) 即可。
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

    [ToolboxItem(true)] //工具箱中顯示此控制項
    public partial class JBarCodeReaderUC : JRoundRectBase
    {
        private JBarcodeReader _barcodeReader = null;

        public JBarCodeReaderUC()
        {
            InitializeComponent();

            _barcodeReader = new JBarcodeReader();
        }

        #region 屬性

        /// <summary>
        /// 取得或設定條碼槍的類型
        /// </summary>
        public enum BARCODE_READER_TYPE { NONE, OPTICON_NLV, CIPHER_LAB };

        private BARCODE_READER_TYPE m_type = BARCODE_READER_TYPE.NONE;

        [Browsable(true)]
        public BARCODE_READER_TYPE Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }

        /// <summary>
        /// 取得或設定條碼槍的ComPort，1 代表 COM1，12 代表 COM12，以此類推
        /// </summary>
        private byte m_comport = 1;

        [Browsable(true)]
        public byte ComPort
        {
            get
            {
                return m_comport;
            }
            set
            {
                m_comport = value;
            }
        }

        /// <summary>
        /// 取得或設定條碼槍是否啟用
        /// </summary>
        private bool m_active = false;

        [Browsable(false)]
        public bool Active
        {
            get
            {
                return m_active;
            }
            //set
            //{
            //    if (value)
            //    {
            //        switch (m_type)
            //        {
            //            case BARCODE_READER_TYPE.CIPHER_LAB:
            //                {
            //                    _barcodeReader = new JBarcodeReader_CipherLab();
            //                }
            //                break;
            //            case BARCODE_READER_TYPE.OPTICON_NLV:
            //                {
            //                    _barcodeReader = new JBarcodeReader_OpticonNLV();
            //                }
            //                break;
            //            default:
            //                {
            //                    _barcodeReader = null;
            //                }
            //                break;
            //        }
            //        m_active = false;
            //        if (_barcodeReader != null)
            //        {
            //            if (_barcodeReader.Open(m_comport))
            //            {
            //                m_active = true;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        if (_barcodeReader != null)
            //        {
            //            _barcodeReader.Close();
            //        }
            //        m_active = false;
            //    }
            //}
        }

        #endregion 屬性

        #region 方法

        public bool Open()
        {
            bool bRet = false;
            switch (m_type)
            {
                case BARCODE_READER_TYPE.CIPHER_LAB:
                    {
                        bRet = _barcodeReader.Open(1, m_comport);
                    }
                    break;

                case BARCODE_READER_TYPE.OPTICON_NLV:
                    {
                        bRet = _barcodeReader.Open(2, m_comport);
                    }
                    break;
            }
            return bRet;
        }

        public void Close()
        {
            _barcodeReader.Close();
        }

        private JActionTask ReadBarcodeTask = new JActionTask();

        public void ReadBarcodeReset()
        {
            _barcodeReader.ReadBarcodeTaskReset();

            //ReadBarcodeTask.Reset();
            //_tm.Reset();
            //_retryCount = 0;
        }

        public enum BARCODE_READER_RESULT { BUSY, ERROR, TIMEOUT, DONE };

        public BARCODE_READER_RESULT ReadBarcode(ref string barcode)
        {
            BARCODE_READER_RESULT Ret = BARCODE_READER_RESULT.DONE;
            barcode = string.Empty;
            string sRet = _barcodeReader.ReadBarcode();
            switch (sRet)
            {
                case "#BUSY#":
                    {
                        Ret = BARCODE_READER_RESULT.BUSY;
                    }
                    break;

                case "!ERROR!":
                    {
                        Ret = BARCODE_READER_RESULT.ERROR;
                    }
                    break;

                case "!TIMEOUT!":
                    {
                        Ret = BARCODE_READER_RESULT.ERROR;
                    }
                    break;

                default:    //取得條碼
                    {
                        barcode = sRet;
                    }
                    break;
            }
            return Ret;

            //Scan the barcode
            //if (m_active)
            //{
            //    ActionTask Task = ReadBarcodeTask;
            //    switch (Task.Value)
            //    {
            //        case 0: //reserve
            //            {
            //                _tm.Reset();
            //                Task.Next();
            //            }
            //            break;
            //        case 1: //Delay 50 ms
            //            {
            //                if (_tm.Start(50))
            //                {
            //                    Task.Next();
            //                }
            //            }
            //            break;
            //        case 2: //Disable Auto Trigger
            //            {
            //                if (_barcodeReader.DisableAutoTrigger())
            //                {
            //                    _tm.Reset();
            //                    Task.Next();
            //                }
            //                else
            //                {
            //                    Ret = BARCODE_READER_RESULT.ERROR;
            //                    Task.Next(99);
            //                }
            //            }
            //            break;
            //        case 3: //Delay 50 ms
            //            {
            //                if (_tm.Start(50))
            //                {
            //                    Task.Next();
            //                }
            //            }
            //            break;
            //        case 4: //Enable the laser
            //            {
            //                if (_barcodeReader.Enable())
            //                {
            //                    _tm.Reset();
            //                    Task.Next();
            //                }
            //                else
            //                {
            //                    Ret = BARCODE_READER_RESULT.ERROR;
            //                    Task.Next(99);
            //                }
            //            }
            //            break;
            //        case 5: //Delay 50 ms
            //            {
            //                if (_tm.Start(50))
            //                {
            //                    Task.Next();
            //                }
            //            }
            //            break;
            //        case 6: //De-trigger the reader
            //            {
            //                if (_barcodeReader.DeTrigger())
            //                {
            //                    _tm.Reset();
            //                    Task.Next();
            //                }
            //                else
            //                {
            //                    Ret = BARCODE_READER_RESULT.ERROR;
            //                    Task.Next(99);
            //                }
            //            }
            //            break;
            //        case 7: //Delay 50 ms
            //            {
            //                if (_tm.Start(50))
            //                {
            //                    Task.Next();
            //                }
            //            }
            //            break;
            //        case 8: //Trigger the reader
            //            {
            //                if (_barcodeReader.Trigger())
            //                {
            //                    _tm.Reset();
            //                    Task.Next();
            //                }
            //                else
            //                {
            //                    Ret = BARCODE_READER_RESULT.ERROR;
            //                    Task.Next(99);
            //                }
            //            }
            //            break;
            //        case 9: //Delay 50 ms
            //            {
            //                if (_tm.Start(50))
            //                {
            //                    _tm.Reset();
            //                    Task.Next();
            //                }
            //            }
            //            break;
            //        case 10: //Get the barcode
            //            {
            //                bool bRet = _barcodeReader.GetResult(ref _barcode);
            //                if (bRet && string.IsNullOrWhiteSpace(_barcode).Equals(false))
            //                {
            //                    _tm.Reset();
            //                    Task.Next();
            //                }
            //                else
            //                {
            //                    if (_tm.Start(300))
            //                    {
            //                        _retryCount++;
            //                        if (_retryCount < 3)
            //                        {
            //                            //Retry
            //                            Task.Next(2);
            //                        }
            //                        else
            //                        {
            //                            //Timeout
            //                            Ret = BARCODE_READER_RESULT.TIMEOUT;
            //                            Task.Next(99);
            //                        }
            //                    }
            //                }
            //            }
            //            break;
            //        case 11: //Delay 50 ms
            //            {
            //                if (_tm.Start(50))
            //                {
            //                    Task.Next();
            //                }
            //            }
            //            break;
            //        case 12: //De-trigger the reader
            //            {
            //                if (_barcodeReader.DeTrigger())
            //                {
            //                    _tm.Reset();
            //                    Task.Next();
            //                }
            //                else
            //                {
            //                    Ret = BARCODE_READER_RESULT.ERROR;
            //                    Task.Next(99);
            //                }
            //            }
            //            break;
            //        case 13: //Delay 50 ms
            //            {
            //                if (_tm.Start(50))
            //                {
            //                    Task.Next();
            //                }
            //            }
            //            break;
            //        case 14: //Disable the laser
            //            {
            //                if (_barcodeReader.Disable())
            //                {
            //                    _tm.Reset();
            //                    Task.Next();
            //                }
            //                else
            //                {
            //                    Ret = BARCODE_READER_RESULT.ERROR;
            //                    Task.Next(99);
            //                }
            //            }
            //            break;
            //        case 15: //Delay 50 ms
            //            {
            //                if (_tm.Start(50))
            //                {
            //                    Task.Next();
            //                }
            //            }
            //            break;
            //        case 16: //Return Done
            //            {
            //                barcode = _barcode;
            //                Ret = BARCODE_READER_RESULT.DONE;
            //                Task.Next(99);
            //            }
            //            break;
            //        case 99: //End
            //            {
            //                //Do nothing.
            //            }
            //            break;
            //    }
            //}

            //return Ret;
        }

        #endregion 方法
    }
}