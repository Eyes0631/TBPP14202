using CommonObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BFM
{
    public enum InspectionState
    {
        Unknow,
        Success,
        Fail,
        Error,
        Wait,
    }

    public class CVisionResult
    {
        private double PosU;
        private BasePosInfo mPos;
        private InspectionState mInspectionResult = InspectionState.Unknow;
        private bool bGrabSuccess;
        private int AddNewJob;
        private int ChangeJob;
        private double x2;
        private double y2;

        public CVisionResult()
        {
            Reset();
        }

        public void Reset()
        {
            mPos.X = 0;
            mPos.Y = 0;
            mPos.Z = 0;
            mPos.U = 0;
            PosU = 0;
            x2 = 0;
            y2 = 0;
            mInspectionResult = InspectionState.Unknow;
            bGrabSuccess = false;
            AddNewJob = -1;
            ChangeJob = -1;
        }

        
        public bool ParseResultString(string result)
        {
            bool bRet = false;
            try
            {
                char[] Separators = new char[] { ',', ';', '=', '|' };
                String[] sCommand = result.Split(Separators, StringSplitOptions.RemoveEmptyEntries);

                //if (sCommand[0] == "Pass")
                if (sCommand[0] == "1")
                {
                    mInspectionResult = InspectionState.Success;
                    double itemp = 0;
                    double.TryParse(sCommand[1], out itemp);
                    mPos.X = (int)itemp;
                    double.TryParse(sCommand[2], out itemp);
                    mPos.Y = (int)itemp;
                    double.TryParse(sCommand[3], out PosU);
                    double.TryParse(sCommand[4], out x2);
                    double.TryParse(sCommand[5], out y2);
                    //PosU = 0;
                    //mPos.U = (int)itemp;
                    //mPos.Y = int.TryParse(sCommand[2]);
                    //mPos.U = int.TryParse(sCommand[3]);
                    //mPos.X = 0;
                    //mPos.Y = 0;
                    //mPos.U = 0;
                    bRet = true;
                }
                //else if (sCommand[0] == "Fail")
                else if (sCommand[0] == "0")
                {
                    mInspectionResult = InspectionState.Fail;
                    double itemp = 0;
                    double.TryParse(sCommand[4], out itemp);
                    mPos.X = (int)itemp;
                    double.TryParse(sCommand[5], out itemp);
                    mPos.Y = (int)itemp;
                    double.TryParse(sCommand[3], out PosU);
                    double.TryParse(sCommand[4], out x2);
                    double.TryParse(sCommand[5], out y2);
                    //PosU = 0;
                    //mPos.U = (int)itemp;
                    //mPos.X = Convert.ToInt32(sCommand[1]);
                    //mPos.Y = Convert.ToInt32(sCommand[2]);
                    //mPos.U = Convert.ToInt32(sCommand[3]);
                    //mPos.X = 0;
                    //mPos.Y = 0;
                    //mPos.U = 180;
                    bRet = true;
                }
                //else if (sCommand[0] == "ERR")
                else if (sCommand[0] == "-1")
                {
                    mInspectionResult = InspectionState.Error;
                    mPos.X = 0;
                    mPos.Y = 0;
                    mPos.U = 0;
                    PosU = 0;
                    x2 = 0;
                    y2 = 0;
                    bRet = true;
                }
                else if (sCommand[0] == "GrabSuccessCmd")
                {
                    bGrabSuccess = true;
                }

                if (sCommand[0] == "ChangeJobCmd")
                {
                    int.TryParse(sCommand[1], out ChangeJob);
                }

                if (sCommand[0] == "AddNewJobCmd")
                {
                    int.TryParse(sCommand[1], out AddNewJob);
                }
                return bRet;
            }
            catch (Exception ex)
            {                
                mInspectionResult = InspectionState.Error;
                mPos.X = 0;
                mPos.Y = 0;
                mPos.U = 0;
                PosU = 0;
                x2 = 0;
                y2 = 0;
                return false;
            }
        }

        public InspectionState GetIsInspectionResultFinish()
        {
            return mInspectionResult;
        }

        public double GetX2()
        {
            return x2;
        }

        public double GetY2()
        {
            return y2;
        }

        public BasePosInfo GetPosInfo()
        {
            return mPos;
        }

        //public int GetAngle()
        //{
        //    return mPos.U;
        //}

        public double GetAngle()
        {
            return PosU;
        }

        public int GetOffsetX()
        {
            return mPos.X;
        }

        public int GetOffsetY()
        {
            return mPos.Y;
        }

        public bool GetGrabSuccessValue()
        {
            return bGrabSuccess;
        }

        public int GetChangeJobValue()
        {
            return ChangeJob;
        }

        public int GetAddNewJobValue()
        {
            return AddNewJob;
        }
    }
}
