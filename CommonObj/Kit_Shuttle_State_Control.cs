using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PaeLibGeneral;

namespace CommonObj
{
    public class KitShuttleStateControl
    {
        /// <summary>
        /// 治具台車狀態控制旗標
        /// </summary>
        private JActionFlag StateFlag = new JActionFlag();

        /// <summary>
        /// 治具台車目前狀態 (Read Only)
        /// </summary>
        public VehicleState State { get; private set; }

        /// <summary>
        /// 建構子
        /// </summary>
        public KitShuttleStateControl()
        {
            Reset();
        }

        /// <summary>
        /// 檢查治具台車指定狀態是否正確
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private bool IsNextState(VehicleState state)
        {
            if ((this.State.Equals(VehicleState.UNTESTED_FULL) && state.Equals(VehicleState.UNTESTED_EMPTY)))
                return true;
            if ((this.State.Equals(VehicleState.NONE) && (state.Equals(VehicleState.TESTED_EMPTY) || state.Equals(VehicleState.UNTESTED_EMPTY))) ||
                //(this.State.Equals(VehicleState.TESTED_EMPTY) && state.Equals(VehicleState.TESTED_EMPTY_CHECKED)) ||
                //(this.State.Equals(VehicleState.TESTED_EMPTY_CHECKED) && state.Equals(VehicleState.TESTED_EMPTY_BOOKED)) ||
                //(this.State.Equals(VehicleState.TESTED_EMPTY_BOOKED) && state.Equals(VehicleState.TESTED_FULL)) ||
                //(this.State.Equals(VehicleState.TESTED_FULL) && state.Equals(VehicleState.TESTED_FULL_IDREADED)) ||
                //(this.State.Equals(VehicleState.TESTED_FULL_IDREADED) && state.Equals(VehicleState.TESTED_FULL_PASS)) ||
                //(this.State.Equals(VehicleState.TESTED_FULL_PASS) && (state.Equals(VehicleState.TESTED_EMPTY) || state.Equals(VehicleState.UNTESTED_EMPTY))) ||
                (this.State.Equals(VehicleState.UNTESTED_EMPTY) && state.Equals(VehicleState.UNTESTED_EMPTY_BOOKED)) ||
                (this.State.Equals(VehicleState.UNTESTED_EMPTY_CHECKED) && state.Equals(VehicleState.UNTESTED_EMPTY_BOOKED)) ||
                (this.State.Equals(VehicleState.UNTESTED_EMPTY_BOOKED) && state.Equals(VehicleState.UNTESTED_FULL)) 
                //(this.State.Equals(VehicleState.UNTESTED_FULL) && state.Equals(VehicleState.UNTESTED_EMPTY)) 
                //(this.State.Equals(VehicleState.UNTESTED_FULL) && state.Equals(VehicleState.UNTESTED_FULL_IDREADED)) ||
                //(this.State.Equals(VehicleState.UNTESTED_FULL_IDREADED) && (state.Equals(VehicleState.UNTESTED_EMPTY) || state.Equals(VehicleState.TESTED_EMPTY) || state.Equals(VehicleState.TESTED_FULL_IDREADED) || state.Equals(VehicleState.TESTED_FULL_FAIL))))
                )
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 狀態重置
        /// </summary>
        public void Reset()
        {
            StateFlag.Reset();
            State = VehicleState.NONE;
        }

        /// <summary>
        /// 切換治具台車狀態，以通知其他流程執行
        /// <para>治具台車流程使用</para>
        /// </summary>
        /// <param name="state">指定要切換的狀態</param>
        /// <returns></returns>
        public bool StateDoIt(VehicleState state)
        {
            if (IsNextState(state))
            {
                this.State = state;
                this.StateFlag.DoIt();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判斷台車是否符合指定狀態
        /// <para>其他流程用來判斷是否要執行流程用</para>
        /// </summary>
        /// <param name="state">指定狀態</param>
        /// <returns></returns>
        public bool IsStateDoIt(VehicleState state)
        {
            if (this.State.Equals(state) && this.StateFlag.IsDoIt())
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 通知治具台車已完成動作，可切換至下一狀態
        /// <para>其他流程使用</para>
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool StateDone(VehicleState state)
        {
            if (this.State.Equals(state))
            {
                this.StateFlag.Done();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 台車流程判斷是否已完成動作可切換至下一流程
        /// <para>治具台車流程使用</para>
        /// </summary>
        /// <param name="state">指定狀態</param>
        /// <returns></returns>
        public bool IsStateDone(VehicleState state)
        {
            if (this.State.Equals(state) && this.StateFlag.IsDone())
            {
                return true;
            }
            return false;
        }
    }
}
