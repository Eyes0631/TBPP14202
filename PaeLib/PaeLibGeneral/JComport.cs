using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PaeLibGeneral
{
    /// <summary>
    /// 序列通訊埠類別
    /// </summary>
    /// <example>
    /// <code>
    /// JComport cp = new JComport();
    /// cp.Open(1,35);  //開啟 COM1 並設定結束數字元為 '#'
    ///
    /// byte[] cmd = new byte[9];
    /// cmd[0] = 37; //起始字元
    /// cmd[1] = 255;
    /// cmd[2] = 72;    //'H'
    /// cmd[3] = 69;    //'E'
    /// cmd[4] = 76;    //'L'
    /// cmd[5] = 76;    //'L'
    /// cmd[6] = 79;    //'O'
    /// cmd[7] = (byte)(cmd[0] + cmd[1] + cmd[2] + cmd[3] + cmd[4] + cmd[5] + cmd[6]);  //checksum
    /// cmd[8] = 35; //結束字元
    /// cp.SendData(cmd);
    ///
    /// byte[] ret = new byte[9];    //先建立一長度為 9 的 byte 陣列，於 GetResult() 中會依實際需求調整陣列大小
    /// if (cp.GetResult(ref ret))
    /// {
    ///     ...
    /// }
    /// </code>
    /// </example>
    public class JComport : IDisposable
    {
        private SerialPort _serialPort = null;
        private List<Byte> _tempList = null;
        private bool _busy = false;
        private byte[] _dataReceived = null;
        private byte _endCode = 13;     //<CR>
        private JWorkerThread _receiver = null;

        /// <summary>
        /// 建構子
        /// </summary>
        public JComport()
        {
            _busy = false;

            _serialPort = new SerialPort();
            _tempList = new List<Byte>();
            _dataReceived = new byte[0];     //預設先配置 1024 bytes
            _endCode = 13;
            Array.Clear(_dataReceived, 0, _dataReceived.Length);    //清空內容
            Array.Resize(ref _dataReceived, 0);    //調整大小為 0

            _receiver = new JWorkerThread(DoReceive);
            _receiver.Start();
        }

        /// <summary>
        /// 釋放 JComport 所使用的資源
        /// </summary>
        public void Dispose()
        {
            if (_serialPort != null)
            {
                Close();
                _serialPort.Dispose();
            }

            if (_receiver != null)
            {
                _receiver.Stop();
            }
        }

        //public bool Open(byte portNum)
        //{
        //    return Open(portNum, 9600, Parity.None, 8, StopBits.One);
        //}

        /// <summary>
        /// 開啟序列通訊埠
        /// </summary>
        /// <param name="portNum">埠號(COM)</param>
        /// <param name="end">指定結束字元(預設為 \r)</param>
        /// <param name="baudRate">鮑率(預設為 9600)</param>
        /// <param name="parity">奇偶校正(預設為 None)</param>
        /// <param name="dataBits">資料位元(預設為 8)</param>
        /// <param name="stopBits">停止位元(預設為 1)</param>
        /// <returns>
        ///     <c>true</c> 開啟成功; 否則, <c>false</c>.
        /// </returns>
        public bool Open(byte portNum, byte end = 13, int baudRate = 9600, Parity parity = Parity.None,
            int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            bool bRet = false;
            try
            {
                if (_serialPort != null)
                {
                    _endCode = end;
                    _serialPort.PortName = String.Format("COM{0}", portNum);
                    _serialPort.BaudRate = baudRate;
                    _serialPort.Parity = parity;
                    _serialPort.DataBits = dataBits;
                    _serialPort.StopBits = stopBits;
                    //_serialPort.Handshake = Handshake.None;
                    //_serialPort.ReceivedBytesThreshold = 1;

                    _serialPort.RtsEnable = true;
                    _serialPort.DtrEnable = true;   //OPTICON NLV-4001 必須啟用 Data Terminal Ready (DTR) 信號，才接收得到資料。

                    //_serialPort.ReadTimeout = 1000;
                    //_serialPort.WriteTimeout = 1000;

                    if (!_serialPort.IsOpen)
                    {
                        _serialPort.Open();
                    }
                    bRet = true;    //開啟 Com Port 成功
                }
            }
            catch (Exception ex)
            {
                //開啟 Com Port 失敗
            }
            return bRet;
        }

        private void Close()
        {
            try
            {
                if ((_serialPort != null) && (_serialPort.IsOpen) && (!_busy))
                {
                    _serialPort.Close();    //關閉 Com Port 成功
                }
            }
            catch (Exception ex)
            {
                //關閉 Com Port 失敗
            }
        }

        /// <summary>
        /// 資料傳輸
        /// </summary>
        /// <param name="buffer">要傳送的資料位元組陣列</param>
        public void SendData(byte[] buffer)
        {
            try
            {
                if ((_serialPort != null) && (_serialPort.IsOpen) && (!_busy))
                {
                    _busy = true;
                    _serialPort.Write(buffer, 0, buffer.Length);    //送出資料

                    //傳送資料，重置結果陣列
                    Array.Clear(_dataReceived, 0, _dataReceived.Length);    //清空內容
                    Array.Resize(ref _dataReceived, 0);    //調整大小為 0
                }
            }
            catch (Exception ex)
            {
                //資料傳送失敗
            }
            finally
            {
                _busy = false;
            }
        }

        private void DoReceive()
        {
            try
            {
                if ((_serialPort != null) && (_serialPort.IsOpen) && (_busy.Equals(false)))
                {
                    if (_serialPort.BytesToRead > 0)
                    {
                        //等資料傳送完畢，才進行資料接收
                        Int32 receivedValue = _serialPort.ReadByte();
                        if (receivedValue >= 0)
                        {
                            _tempList.Add((byte)receivedValue);
                            //判斷是否為結束字元
                            if (receivedValue.Equals(_endCode))
                            {
                                parse(_tempList);   //解析接收字串
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void parse(List<byte> lst)
        {
            try
            {
                Array.Resize(ref _dataReceived, lst.Count);    //重新調整大小
                _dataReceived = lst.ToArray();
                lst.Clear();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// 取得結果
        /// </summary>
        /// <param name="ret">接收結果字元組陣列</param>
        /// <returns>
        ///     <c>true</c> 取得結果成功; 否則, <c>false</c>.
        /// </returns>
        public bool GetResult(ref byte[] ret)
        {
            bool bRet = false;
            try
            {
                if (_dataReceived.Length > 0)
                {
                    Array.Resize(ref ret, _dataReceived.Length);    //重新調整大小
                    _serialPort.ReadExisting();
                    Array.Copy(_dataReceived, ret, _dataReceived.Length);   //複製
                    bRet = true;
                }
            }
            catch (Exception ex)
            {
            }
            return bRet;
        }
    }
}