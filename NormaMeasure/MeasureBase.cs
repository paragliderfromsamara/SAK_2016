using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace NormaMeasure.MeasureControl
{
    public delegate void MeasureHandler(object _measure);
    public delegate void MeasureFunction();

    public class MeasureBase
    {
        
        public MeasureBase()
        {
            MeasureBody = defaultMeasureThreadFunc;
        }

        protected virtual bool WillMeasureContinue()
        {
            return (cycleLimit > cycleNumber);
        }

        private void defaultMeasureThreadFunc()
        {
            do
            {
                cycleNumber++;
                OnMeasure(this);
                Thread.Sleep(1000);
            } while (WillMeasureContinue());
        }
        
        private void InitOverAllMeasureTimer()
        {
            TimerCallback tm = new TimerCallback(RefreshTimer);
            overallMeasureTime = 0;
            OverallMeasureTimer = new Timer(tm, 0, 0, 1000);
        }

        private void RefreshTimer(object obj)
        {
            OnOverallMeasureTimerTick(this);
            overallMeasureTime++;
        }

        public virtual void Start()
        {
            cycleNumber = 0;
            if (!isStarted)
            {
                this.MeasureThread = new Thread(this.MeasureMainFunction);
                MeasureThread.Start();
                isStarted = true; 
            }
        }


        private void MeasureMainFunction()
        {
            InitOverAllMeasureTimer();
            OnMeasureStart(this);
            MeasureBody();
            OverallMeasureTimer.Dispose();
            isStarted = false;
            OnMeasureStop(this);
        }


        public virtual void Stop()
        {
            if (MeasureThread != null)
            {
                MeasureThread.Abort();
                MeasureThread = null;
            }

            isStarted = false;
            OnMeasureStop(this);
        }

    

        public event MeasureHandler OnMeasureStart;
        public event MeasureHandler OnMeasure;
        public event MeasureHandler OnMeasureStop;
        public event MeasureHandler OnOverallMeasureTimerTick;

        protected MeasureFunction MeasureBody;

        private Thread MeasureThread;
        private Timer OverallMeasureTimer;

        private bool isStarted = false;

        public bool IsStart => isStarted;


        private int cycleNumber = 0;
        private int cycleLimit = 5;
        public int CycleNumber => cycleNumber;
        public int CycleLimit
        {
            get
            {
                return cycleLimit;
            }
            set
            {
                cycleLimit = value;
            }
        }

        public int OverallMeasTime => overallMeasureTime;

        protected int CurrentMeasureTime = 0;
        protected int overallMeasureTime = 0;



        
    }


}
