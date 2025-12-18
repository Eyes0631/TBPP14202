using System;
using ProVLib;
using PaeLibGeneral;
using PaeLibProVSDKEx;

namespace PaeLibPnPModule
{
    /// <summary>
    /// <c>取放頭模組</c>類別
    /// </summary>
    public class PnPModule : JModuleBase
    {
        public enum MotorName { X = 0, Y = 1, Z = 2, PitchX = 3, PitchY = 4, Rotate = 5 };

        private Motor[] MotorArray = new Motor[6];

        public enum VacuumCheckType { CheckON, CheckOff };

        public enum CylinderAction { Up, Down };

        public enum ActionMode { ALL, WithHaveICArray, ExcludeHaveICArray, WithWorkingArray, ExcludeWorkingArray };

        public enum ActionReturn { IDLE, Running, Done, VacuumError };

        public enum SafetySensorType { None, HomeSensor, InbitOnSafe, InbitOffSafe };

        public enum PnpActionMode { None, Pick, Place };

        public class MOVE_POS //X, Y, U, R
        {
            //Property
            public int X;

            public int Y;
            public int PX; //PitchX
            public int PY; //PitchY
            public int R; //Rotate

            //Method
            public MOVE_POS(MOVE_POS _mpos = null)
            {
                if (_mpos == null)
                    Clear();
                else
                    Set(_mpos);
            }

            public MOVE_POS(int _X, int _Y, int _PX, int _PY, int _R)
            {
                Set(_X, _Y, _PX, _PY, _R);
            }

            public void Clear()
            {
                X = 0;
                Y = 0;
                PX = 0;
                PY = 0;
                R = 0;
            }

            public void Set(int _X, int _Y, int _PX, int _PY, int _R)
            {
                X = _X;
                Y = _Y;
                PX = _PX;
                PY = _PY;
                R = _R;
            }

            public void Set(MOVE_POS _mpos)
            {
                X = _mpos.X;
                Y = _mpos.Y;
                PX = _mpos.PX;
                PY = _mpos.PY;
                R = _mpos.R;
            }

            public bool Equal(int _X, int _Y, int _PX, int _PY, int _R, int dif = 5)
            {
                dif = Math.Abs(dif);
                return (Math.Abs(X - _X) <= dif &&
                        Math.Abs(Y - _Y) <= dif &&
                        Math.Abs(PX - _PX) <= dif &&
                        Math.Abs(PY - _PY) <= dif &&
                        Math.Abs(R - _R) <= dif);
            }

            public bool Equal(MOVE_POS _mpos, int dif = 5)
            {
                dif = Math.Abs(dif);
                return (Math.Abs(X - _mpos.X) <= dif &&
                        Math.Abs(Y - _mpos.Y) <= dif &&
                        Math.Abs(PX - _mpos.PX) <= dif &&
                        Math.Abs(PY - _mpos.PY) <= dif &&
                        Math.Abs(R - _mpos.R) <= dif);
            }
        }

        public SafetySensorType SafetyHeightSensorType = SafetySensorType.HomeSensor;
        public bool LogTraceEnable = false;
        public bool LogDebugEnable = false;
        public string LogMsgTitle = "PNPHD";

        private Motor MT_X = null;
        private Motor MT_Y = null;
        private Motor MT_Z = null;
        private Motor MT_PitchX = null;
        private Motor MT_PitchY = null;
        private Motor MT_Rotate = null;
        private NozzleArray Nozzles = null;
        private DigitalInput SafetyHeightSen = null;
        private OutBit AxisZ_Braker = null;

        private JActionTask ACT_Home_Task;
        private JActionTask ACT_Move_Task;
        private JActionTask ACT_PNP_Task;

        private bool NoICTryRun = false;
        private bool AxisZ_HomeOK = false;
        private MyTimer ActTmr = null;
        private int Num_Col_X = 0;
        private int Num_Row_Y = 0;
        private bool[,] VacuumArray = null;
        private bool[,] HaveICArray = null;
        private bool[,] WorkingArray = null;

        protected override void SimulationSetting(bool simulation)
        {
            //實作父類別抽象函式內容
            foreach (Motor mt in MotorArray)
                mt.IsSimulation = simulation ? 2 : 0;

            Nozzles.Simulation = simulation;
            SafetyHeightSen.Simulation = simulation;
            AxisZ_Braker.IsSimulation = simulation ? 2 : 0;
        }

        #region Alarm Param

        public struct PNPHD_AlarmCode
        {
            public ushort SafetyHeightErrorStop; //安全高度檢查異常, 馬達 {0} 動作停止
            public ushort NozzleVacuumDetectICLost; //吸嘴({0}, {1}) 真空狀態: IC 掉落
            public ushort NozzleVacuumDetectICRemain; //吸嘴({0}, {1}) 真空狀態: IC 殘留
            public ushort NozzlePickICCheckFail; //吸嘴({0}, {1}) 吸取IC 失敗
            public ushort NozzlePlaceICCheckFail; //吸嘴({0}, {1}) 放置IC 失敗
        }

        public PNPHD_AlarmCode AlarmCode;

        public delegate void DShowAlarm1(string errCode, int errNum, string ParamStr);

        public delegate void DShowAlarm2(string errCode, int errNum, int ParamInt1, int ParamInt2);

        public DShowAlarm1 ShowAlarm1;
        public DShowAlarm2 ShowAlarm2;

        #endregion Alarm Param

        public PnPModule(AlarmCallback alarm, DShowAlarm1 alarm1, DShowAlarm2 alarm2,
            Motor _MT_X, Motor _MT_Y, Motor _MT_Z, Motor _MT_PitchX, Motor _MT_PitchY, Motor _MT_Rotate,
            int _Num_Col_X, int _Num_Row_Y, NozzleArray _Nozzles, InBit _SafetyHeightSen, OutBit _AxisZ_Braker)
            : base(alarm)
        {
            ShowAlarm1 = alarm1;
            ShowAlarm2 = alarm2;
            AlarmCode.SafetyHeightErrorStop = 90;
            AlarmCode.NozzleVacuumDetectICLost = 91;
            AlarmCode.NozzleVacuumDetectICRemain = 92;
            AlarmCode.NozzlePickICCheckFail = 93;
            AlarmCode.NozzlePlaceICCheckFail = 94;

            MT_X = _MT_X;
            MT_Y = _MT_Y;
            MT_Z = _MT_Z;
            MT_PitchX = _MT_PitchX;
            MT_PitchY = _MT_PitchY;
            MT_Rotate = _MT_Rotate;

            Motor mt_tmp = new Motor();
            mt_tmp.IOPort = "";
            mt_tmp.IsSimulation = 2;
            if (MT_X == null)
                MT_X = mt_tmp;
            if (MT_Y == null)
                MT_Y = mt_tmp;
            if (MT_Z == null)
                MT_Z = mt_tmp;
            if (MT_PitchX == null)
                MT_PitchX = mt_tmp;
            if (MT_PitchY == null)
                MT_PitchY = mt_tmp;
            if (MT_Rotate == null)
                MT_Rotate = mt_tmp;

            MotorArray[(int)MotorName.X] = MT_X;
            MotorArray[(int)MotorName.Y] = MT_Y;
            MotorArray[(int)MotorName.Z] = MT_Z;
            MotorArray[(int)MotorName.PitchX] = MT_PitchX;
            MotorArray[(int)MotorName.PitchY] = MT_PitchY;
            MotorArray[(int)MotorName.Rotate] = MT_Rotate;

            Nozzles = _Nozzles;
            Num_Col_X = _Num_Col_X;
            Num_Row_Y = _Num_Row_Y;
            SafetyHeightSen = new DigitalInput(_SafetyHeightSen);
            AxisZ_Braker = _AxisZ_Braker;

            ACT_Home_Task = new JActionTask();
            ACT_Move_Task = new JActionTask();
            ACT_PNP_Task = new JActionTask();

            VacuumArray = new bool[Num_Col_X, Num_Row_Y];
            HaveICArray = new bool[Num_Col_X, Num_Row_Y];
            WorkingArray = new bool[Num_Col_X, Num_Row_Y];
            ActTmr = new MyTimer();
            ActTmr.AutoReset = false;
        }

        #region private

        private Motor _Motor(MotorName mt)
        {
            switch (mt)
            {
                case MotorName.X:
                    return MT_X;

                case MotorName.Y:
                    return MT_Y;

                case MotorName.Z:
                    return MT_Z;

                case MotorName.PitchX:
                    return MT_PitchX;

                case MotorName.PitchY:
                    return MT_PitchY;

                case MotorName.Rotate:
                    return MT_Rotate;
            }
            return null;
        }

        private void ServoOn()
        {
            foreach (Motor mt in MotorArray)
                mt.ServoOn(true);

            AxisZ_Braker.On();
        }

        private void ServoOff()
        {
            foreach (Motor mt in MotorArray)
                mt.ServoOn(false);

            AxisZ_Braker.Off();
        }

        private MOVE_POS ReadPos_M()
        {
            return new MOVE_POS(MT_X.ReadPos(), MT_Y.ReadPos(), MT_PitchX.ReadPos(), MT_PitchY.ReadPos(), MT_Rotate.ReadPos());
        }

        private MOVE_POS ReadEncPos_M()
        {
            return new MOVE_POS(MT_X.ReadEncPos(), MT_Y.ReadEncPos(), MT_PitchX.ReadEncPos(), MT_PitchY.ReadEncPos(), MT_Rotate.ReadEncPos());
        }

        private bool Motor_G00(MotorName mt, int pos, int SafetyMoveHeight = 0, bool CheckSafetySensor = true)
        {
            if (_Motor(mt) == null)
            {
                return true;
            }
            if (mt == MotorName.Z || CheckSafetyHeight(SafetyMoveHeight, CheckSafetySensor)) //|| Math.Abs(_Motor(mt).ReadEncPos() - pos) < 50
            {
                return _Motor(mt).G00(pos);
            }
            _Motor(mt).FastStop();
            return false;
        }

        private bool CheckSafetyHeight(int CheckHeight = 0, bool CheckSafetySensor = true, bool EnforceAct = false)
        {
            if (Simulation)
            {
                return true;
            }

            bool bSafeSensorOK = false;
            switch (SafetyHeightSensorType)
            {
                case SafetySensorType.HomeSensor:
                    bSafeSensorOK = MT_Z.IsHomeOn;
                    break;

                case SafetySensorType.InbitOffSafe:
                    bSafeSensorOK = SafetyHeightSen.ValueOff;
                    break;

                case SafetySensorType.InbitOnSafe:
                    bSafeSensorOK = SafetyHeightSen.ValueOn;
                    break;

                case SafetySensorType.None:
                    bSafeSensorOK = true;
                    break;
            }
            bSafeSensorOK = (CheckSafetySensor == false || bSafeSensorOK);

            if ((MT_Z.ReadEncPos() > CheckHeight - 100) && bSafeSensorOK)
            {
                return true;
            }
            if (EnforceAct)
            {
                Motor_G00(MotorName.Z, CheckHeight);
            }
            return false;
        }

        private void DataReset()
        {
            for (int i = 0; i < Num_Col_X; i++)
            {
                for (int j = 0; j < Num_Row_Y; j++)
                {
                    HaveICArray[i, j] = false;
                    WorkingArray[i, j] = false;
                    Nozzles[i, j].State = NozzleState.Unused;
                    //Nozzles[i, j].ActionState = NozzleActionState.None;
                }
            }
        }

        private bool NozzleInAction(int x, int y, ActionMode atm = ActionMode.ALL)
        {
            if (atm == ActionMode.WithHaveICArray && HaveICArray[x, y] == false)
                return false;
            if (atm == ActionMode.ExcludeHaveICArray && HaveICArray[x, y] == true)
                return false;
            if (atm == ActionMode.WithWorkingArray && WorkingArray[x, y] == false)
                return false;
            if (atm == ActionMode.ExcludeWorkingArray && WorkingArray[x, y] == true)
                return false;

            return true;
        }

        private void JLogTrace(string msg)
        {
            if (LogTraceEnable)
            {
                JLogger.LogTrace("Trace", LogMsgTitle + " | " + msg);
            }
        }

        private void JLogDebug(string msg)
        {
            if (LogDebugEnable)
            {
                JLogger.LogDebug("Debug", LogMsgTitle + " | " + msg);
            }
        }

        #endregion private

        #region public

        public bool AlwaysRunSafeCheck(int CheckHeight = 0, bool CheckSafetySensor = true)
        {
            bool ret = true;
            try
            {
                if (CheckSafetyHeight(CheckHeight, CheckSafetySensor) == false)
                {
                    foreach (Motor mt in MotorArray)
                    {
                        if (mt == MT_Z)
                            continue;

                        if (mt.Busy())
                        {
                            mt.FastStop();
                            ret = false;
                            ShowAlarm1("E", (int)AlarmCode.SafetyHeightErrorStop, mt.Text);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                ret = false;
            }
            return ret;
        }

        public void SetSpeed(MotorName mt, int Speed, int Acc, int Dec)
        {
            _Motor(mt).SetSpeed(Speed);
            _Motor(mt).SetAcceleration(Acc);
            _Motor(mt).SetDeceleration(Dec);
        }

        public void StopMotor(MotorName mt)
        {
            _Motor(mt).Stop();
        }

        public void StopMotor()
        {
            foreach (Motor mt in MotorArray)
                mt.Stop();
        }

        public void FastStopMotor(MotorName mt)
        {
            _Motor(mt).FastStop();
        }

        public void FastStopMotor()
        {
            foreach (Motor mt in MotorArray)
                mt.FastStop();
        }

        public bool[,] GetVacuumStatusArray()
        {
            if (Simulation)
            {
                VacuumArray = HaveICArray;
            }
            else
            {
                for (int x = 0; x < Num_Col_X; x++)
                {
                    for (int y = 0; y < Num_Row_Y; y++)
                    {
                        VacuumArray[x, y] = (Nozzles[x, y].VacuumCheck() == ThreeValued.TRUE);
                    }
                }
            }
            return VacuumArray;
        }

        public bool SetWorkingArray(bool[,] ActOn, bool InPicking)
        {
            if (ActOn != null && ActOn.Rank == 2 && ActOn.GetLength(0) == Num_Col_X && ActOn.GetLength(1) == Num_Row_Y)
            {
                for (int x = 0; x < Num_Col_X; x++)
                {
                    for (int y = 0; y < Num_Row_Y; y++)
                    {
                        WorkingArray[x, y] = ActOn[x, y];
                        //if (ActOn[x, y] && HaveICArray[x, y] == InPicking)
                        //{
                        //    //if (ActOn[x, y] && InPicking == true && HaveICArray[x, y] == true) //啟動吸IC, 但已經有IC
                        //    //if (ActOn[x, y] && InPicking == false && HaveICArray[x, y] == false) //啟動放IC, 但沒有IC
                        //    WorkingArray[x, y] = false;
                        //}
                        //else
                        //{
                        //    WorkingArray[x, y] = ActOn[x, y];
                        //}
                    }
                }
                return true;
            }
            return false;
        }

        public bool CheckVacuumStatus(VacuumCheckType vct, ActionMode atm = ActionMode.ALL, bool AlarmShow = false)
        {
            if (NoICTryRun)
            {
                return true;
            }

            bool ret = true;
            for (int x = 0; x < Num_Col_X; x++)
            {
                for (int y = 0; y < Num_Row_Y; y++)
                {
                    if (NozzleInAction(x, y, atm) == false)
                        continue;

                    if (vct == VacuumCheckType.CheckON && Nozzles[x, y].VacuumCheck() != ThreeValued.TRUE)
                    {
                        ret = false;
                        if (AlarmShow)
                        {
                            ShowAlarm2("E", (int)AlarmCode.NozzleVacuumDetectICLost, x, y); //IC 掉落
                        }
                    }
                    if (vct == VacuumCheckType.CheckOff && Nozzles[x, y].VacuumCheck() != ThreeValued.FALSE)
                    {
                        ret = false;
                        if (AlarmShow)
                        {
                            ShowAlarm2("E", (int)AlarmCode.NozzleVacuumDetectICRemain, x, y); //IC 殘留
                        }
                    }
                }
            }
            return ret;
        }

        public bool[,] VacuumUpdateHaveICArray(ActionMode atm = ActionMode.ALL)
        {
            //以實際真空反應更新有無IC狀態(HaveICArray), 傳回值：HaveICArray
            if (NoICTryRun)
            {
                return HaveICArray;
            }

            for (int x = 0; x < Num_Col_X; x++)
            {
                for (int y = 0; y < Num_Row_Y; y++)
                {
                    if (NozzleInAction(x, y, atm) == false)
                        continue;

                    HaveICArray[x, y] = (Nozzles[x, y].VacuumCheck() == ThreeValued.TRUE);
                }
            }
            return HaveICArray;
        }

        public bool CheckWorkingNozzlesAfterPnp(bool InPicking, bool AlarmShow = true)
        {
            //以「取放模式」判斷工作吸嘴「取放後」的狀態是否正確

            //#1 更新工作吸嘴有無IC狀態
            VacuumUpdateHaveICArray(ActionMode.WithWorkingArray);

            //#2 檢查IC反應是否合乎取放模式
            bool ret = true;
            for (uint x = 0; x < Num_Col_X; x++)
            {
                for (uint y = 0; y < Num_Row_Y; y++)
                {
                    if (WorkingArray[x, y] && HaveICArray[x, y] != InPicking)
                    {
                        //if (inWorkingArray[x, y] && InPicking == true && HaveICArray[x, y] == false)
                        //if (inWorkingArray[x, y] && InPicking == false && HaveICArray[x, y] == true)

                        if (NoICTryRun)
                        {
                            HaveICArray[x, y] = InPicking;
                        }
                        else
                        {
                            ret = false;
                            if (AlarmShow)
                            {
                                int iNozzleNo = ((int)x + 1) + ((int)y * Num_Col_X); //01~08
                                if (InPicking)
                                {
                                    ShowAlarm2("E", (int)AlarmCode.NozzlePickICCheckFail, (int)x, (int)y);
                                }
                                else
                                {
                                    ShowAlarm2("E", (int)AlarmCode.NozzlePlaceICCheckFail, (int)x, (int)y);
                                }
                            }
                        }
                    }
                }
            }
            return ret;
        }

        public bool NozzleCylinderCtrl(CylinderAction cat, ActionMode atm = ActionMode.ALL, PnpActionMode ppm = PnpActionMode.None)
        {
            bool ret = true;
            for (int x = 0; x < Num_Col_X; x++)
            {
                for (int y = 0; y < Num_Row_Y; y++)
                {
                    if (NozzleInAction(x, y, atm) == false)
                        continue;

                    if (cat == CylinderAction.Up)
                    {
                        ret = ((Nozzles[x, y].CylinderOn() == ThreeValued.TRUE) && ret);
                    }
                    if (cat == CylinderAction.Down)
                    {
                        if (ppm == PnpActionMode.Pick && Nozzles[x, y].VacuumCheck() == ThreeValued.TRUE)
                        {
                            ret = ((Nozzles[x, y].CylinderOn(500) == ThreeValued.TRUE) && ret);
                        }
                        else
                        {
                            ret = ((Nozzles[x, y].CylinderOff() == ThreeValued.TRUE) && ret);
                        }
                    }
                }
            }
            return ret;
        }

        public bool VacuumControl(bool turnOn, ActionMode atm = ActionMode.ALL)
        {
            if (NoICTryRun)
            {
                return true;
            }

            bool ret = true;
            for (int x = 0; x < Num_Col_X; x++)
            {
                for (int y = 0; y < Num_Row_Y; y++)
                {
                    if (NozzleInAction(x, y, atm) == false)
                        continue;

                    if (turnOn)
                    {
                        ret = (Nozzles[x, y].VacuumOn(0) && ret);
                    }
                    else
                    {
                        ret = (Nozzles[x, y].VacuumOff(0) && ret);
                    }
                }
            }
            return ret;
        }

        public bool DestroyControl(bool turnOn, ActionMode atm = ActionMode.ALL)
        {
            if (NoICTryRun)
            {
                return true;
            }

            bool ret = true;
            for (int x = 0; x < Num_Col_X; x++)
            {
                for (int y = 0; y < Num_Row_Y; y++)
                {
                    if (NozzleInAction(x, y, atm) == false)
                        continue;

                    if (turnOn)
                    {
                        ret = (Nozzles[x, y].DestoryOn(0) && ret);
                    }
                    else
                    {
                        ret = (Nozzles[x, y].DestoryOff(0) && ret);
                    }
                }
            }
            return ret;
        }

        public bool Is_AxisZ_HomeOK()
        {
            return AxisZ_HomeOK;
        }

        public void ACT_Home_Task_Reset()
        {
            AxisZ_HomeOK = false;
            ACT_Home_Task.Reset();
            ActTmr.Restart();
        }

        public void ACT_Move_Task_Reset()
        {
            ACT_Move_Task.Reset();
            ActTmr.Restart();
        }

        public void ACT_PNP_Task_Reset()
        {
            ACT_PNP_Task.Reset();
            ActTmr.Restart();
        }

        public bool ACT_Home_Task_Run(int _X, int _Y, int _PX, int _PY, int _R)
        {
            return ACT_Home_Task_Run(new MOVE_POS(_X, _Y, _PX, _PY, _R));
        }

        public bool ACT_Home_Task_Run(MOVE_POS _mpos = null)
        {
            switch (ACT_Home_Task.Value)
            {
                case 0: //Task Reset
                    {
                        DataReset();
                        ServoOn();

                        foreach (Motor mt in MotorArray)
                            mt.HomeReset();

                        ACT_Home_Task.Next();
                    }
                    break;

                case 1: //Z 軸歸零
                    {
                        if (MT_Z.Home())
                        {
                            AxisZ_HomeOK = true;
                            ActTmr.Restart();
                            ACT_Home_Task.Next();
                        }
                    }
                    break;

                case 2: //開真空, 關破壞
                    {
                        bool b1 = VacuumControl(true);
                        bool b2 = DestroyControl(false);
                        if (b1 && b2)
                        {
                            ActTmr.Restart();
                            ACT_Home_Task.Next();
                        }
                    }
                    break;

                case 3: //真空反應等待時間
                    {
                        if (ActTmr.On(2000))
                        {
                            ACT_Home_Task.Next();
                        }
                    }
                    break;

                case 4: //檢查真空狀態
                    {
                        if (CheckVacuumStatus(VacuumCheckType.CheckOff, ActionMode.ALL, true))
                        {
                            ACT_Home_Task.Next();
                        }
                    }
                    break;

                case 5: //關真空, 汽缸常態
                    {
                        bool b1 = NozzleCylinderCtrl(CylinderAction.Down);
                        bool b2 = VacuumControl(false);
                        bool b3 = DestroyControl(false);
                        if (b1 && b2 && b3)
                        {
                            ACT_Home_Task.Next();
                        }
                    }
                    break;

                case 6: //各軸歸零
                    {
                        bool b1 = (MT_X == null || MT_X.Home());
                        bool b2 = (MT_Y == null || MT_Y.Home());
                        bool b3 = (MT_PitchX == null || MT_PitchX.Home());
                        bool b4 = (MT_PitchY == null || MT_PitchY.Home());
                        bool b5 = (MT_Rotate == null || MT_Rotate.Home());
                        if (b1 && b2 && b3 && b4 && b5)
                        {
                            ACT_Move_Task_Reset();
                            ActTmr.Restart();
                            ACT_Home_Task.Next();
                        }
                    }
                    break;

                case 7: //初始狀態位置
                    {
                        if (_mpos == null || ACT_Move_Task_Run(_mpos, 0) == ActionReturn.Done)
                        {
                            ActTmr.Restart();
                            ACT_Home_Task.Next();
                        }
                    }
                    break;

                case 8: //完成
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        public ActionReturn ACT_Move_Task_Run(int _X, int _Y, int _PX, int _PY, int _R, int SafetyMoveHeight = 0)
        {
            return ACT_Move_Task_Run(new MOVE_POS(_X, _Y, _PX, _PY, _R), SafetyMoveHeight);
        }

        public ActionReturn ACT_Move_Task_Run(MOVE_POS _mpos, int SafetyMoveHeight = 0, PnpActionMode ppm = PnpActionMode.None)
        {
            //即時吸嘴真空狀態檢查
            if (CheckVacuumStatus(VacuumCheckType.CheckON, ActionMode.WithHaveICArray, true) == false ||
                CheckVacuumStatus(VacuumCheckType.CheckOff, ActionMode.ExcludeHaveICArray, true) == false)
            {
                StopMotor();
                return ActionReturn.VacuumError;
            }

            switch (ACT_Move_Task.Value)
            {
                case 0: //檢查目前位置
                    {
                        MOVE_POS postmp = ReadEncPos_M();
                        if (postmp.Equal(_mpos))
                        {
                            NozzleCylinderCtrl(CylinderAction.Down, ActionMode.WithWorkingArray, ppm);
                            NozzleCylinderCtrl(CylinderAction.Up, ActionMode.ExcludeWorkingArray);
                            return ActionReturn.Done;
                        }
                        ACT_Move_Task.Next();
                    }
                    break;

                case 1: //檢查Z軸高度
                    {
                        if (CheckSafetyHeight(SafetyMoveHeight))
                        {
                            ACT_Move_Task.Next();
                        }
                        else
                        {
                            //MT_Z.G00(SafetyHeight);
                            Motor_G00(MotorName.Z, SafetyMoveHeight);
                        }
                    }
                    break;

                case 2: //各軸同動
                    {
                        bool b1 = Motor_G00(MotorName.X, _mpos.X, SafetyMoveHeight);
                        bool b2 = Motor_G00(MotorName.Y, _mpos.Y, SafetyMoveHeight);
                        bool b3 = Motor_G00(MotorName.PitchX, _mpos.PX, SafetyMoveHeight);
                        bool b4 = Motor_G00(MotorName.PitchY, _mpos.PY, SafetyMoveHeight);
                        bool b5 = Motor_G00(MotorName.Rotate, _mpos.R, SafetyMoveHeight);
                        bool b6 = NozzleCylinderCtrl(CylinderAction.Down, ActionMode.WithWorkingArray, ppm);
                        bool b7 = NozzleCylinderCtrl(CylinderAction.Up, ActionMode.ExcludeWorkingArray);
                        if (b1 && b2 && b3 && b4 && b5 && b6 && b7)
                        {
                            JLogTrace("(X=" + _mpos.X.ToString() + ", Y=" + _mpos.Y.ToString()
                                + ", PX=" + _mpos.PX.ToString() + ", PY=" + _mpos.PY.ToString()
                                + ", R=" + _mpos.R.ToString() + ")");
                            ACT_Move_Task.Next();
                        }
                    }
                    break;

                case 3: //完成
                    {
                        //ACT_Move_Task.Next(0);
                        return ActionReturn.Done;
                    }
                    break;
            }
            return ActionReturn.Running;
        }

        public bool ACT_PNP_Task_Run(int PnpDown, int PnpUp, bool InPicking, int SteadyDelay, int PnpDelay, int UpPosDelay, bool BypassVacuumStatusCheck = false)
        {
            switch (ACT_PNP_Task.Value)
            {
                case 0: //真空狀態檢查
                    {
                        if (BypassVacuumStatusCheck)
                        {
                            ACT_PNP_Task.Next();
                            break;
                        }
                        if (CheckVacuumStatus(VacuumCheckType.CheckON, ActionMode.WithHaveICArray, true) == false ||
                            CheckVacuumStatus(VacuumCheckType.CheckOff, ActionMode.ExcludeHaveICArray, true) == false)
                        {
                            StopMotor();
                            return false;
                        }
                        ACT_PNP_Task.Next();
                    }
                    break;

                case 1: //汽缸動作, Z軸下降
                    {
                        bool b1 = Motor_G00(MotorName.Z, PnpDown);
                        bool b2 = NozzleCylinderCtrl(CylinderAction.Down, ActionMode.WithWorkingArray, (InPicking ? PnpActionMode.Pick : PnpActionMode.None));
                        bool b3 = NozzleCylinderCtrl(CylinderAction.Up, ActionMode.ExcludeWorkingArray);
                        if (b1 && b2 && b3)
                        {
                            JLogTrace("Z = " + PnpDown.ToString());
                            ActTmr.Restart();
                            ACT_PNP_Task.Next();
                        }
                    }
                    break;

                case 2: //馬達下降整定時間
                    {
                        if (ActTmr.On(SteadyDelay))
                        {
                            ACT_PNP_Task.Next();
                        }
                    }
                    break;

                case 3: //開啟/關閉真空
                    {
                        if (InPicking)
                        {
                            bool b1 = VacuumControl(true, ActionMode.WithWorkingArray);
                            bool b2 = DestroyControl(false, ActionMode.WithWorkingArray);
                            if (b1 && b2)
                            {
                                ActTmr.Restart();
                                ACT_PNP_Task.Next();
                            }
                        }
                        else
                        {
                            bool b1 = VacuumControl(false, ActionMode.WithWorkingArray);
                            bool b2 = DestroyControl(true, ActionMode.WithWorkingArray);
                            if (b1 && b2)
                            {
                                ActTmr.Restart();
                                ACT_PNP_Task.Next();
                            }
                        }
                    }
                    break;

                case 4: //等待時間
                    {
                        if (ActTmr.On(PnpDelay))
                        {
                            ACT_PNP_Task.Next();
                        }
                        else if (InPicking && NoICTryRun == false && CheckVacuumStatus(VacuumCheckType.CheckON, ActionMode.WithWorkingArray))
                        {
                            ACT_PNP_Task.Next();
                        }
                    }
                    break;

                case 5: //關閉吹氣
                    {
                        if (InPicking)
                        {
                            ACT_PNP_Task.Next();
                        }
                        else
                        {
                            bool b1 = VacuumControl(false, ActionMode.WithWorkingArray);
                            bool b2 = DestroyControl(false, ActionMode.WithWorkingArray);
                            if (b1 && b2)
                            {
                                ACT_PNP_Task.Next();
                            }
                        }
                    }
                    break;

                case 6: //Z軸上昇
                    {
                        if (Motor_G00(MotorName.Z, PnpUp))
                        {
                            ActTmr.Restart();
                            JLogTrace("Z = " + PnpUp.ToString());
                            ACT_PNP_Task.Next();
                        }
                    }
                    break;

                case 7: //真空狀態更新
                    {
                        if (ActTmr.On(UpPosDelay))
                        {
                            VacuumUpdateHaveICArray(ActionMode.WithWorkingArray);
                            ACT_PNP_Task.Next();
                        }
                        else if (InPicking && NoICTryRun == false && CheckVacuumStatus(VacuumCheckType.CheckON, ActionMode.WithWorkingArray))
                        {
                            VacuumUpdateHaveICArray(ActionMode.WithWorkingArray);
                            ACT_PNP_Task.Next();
                        }
                    }
                    break;

                case 8: //完成
                    {
                        return true;
                    }
                    break;
            }
            return false;
        }

        public void SetNoICTryRun(bool RunNoIC)
        {
            NoICTryRun = RunNoIC;
        }

        #endregion public
    }
}