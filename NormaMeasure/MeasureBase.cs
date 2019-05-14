using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace NormaMeasure.MeasureControl
{
    public delegate void MeasureStatus_Handler(object _measure, MeasureCycleStatus new_status);
    public delegate void MeasureHandler(object _measure);
    public delegate void MeasureFunction();

    public class MeasureBase
    {
        
        public MeasureBase()
        {
            MeasureBody = defaultMeasureThreadFunc;
        }

        /// <summary>
        /// Проверка продолжения измерения
        /// </summary>
        /// <returns></returns>
        protected virtual bool WillMeasureContinue()
        {
            return (cycleLimit > cycleNumber && !IsWillFinished && !IsFinished && IsDeviceFound);
        }

        /// <summary>
        /// Функция произвольного измерения
        /// </summary>
        private void defaultMeasureThreadFunc()
        {
            do
            {
                cycleNumber++;
                OnMeasure?.Invoke(this);
                Thread.Sleep(1000);
            } while (WillMeasureContinue());
        }
        
        /// <summary>
        /// Инициализация таймера измерения
        /// </summary>
        private void InitOverAllMeasureTimer()
        {
            TimerCallback tm = new TimerCallback(RefreshTimer);
            overallMeasureTime = 0;
            OverallMeasureTimer = new Timer(tm, 0, 0, 1000);
        }

        /// <summary>
        /// Обновление таймера измерения
        /// </summary>
        /// <param name="obj"></param>
        private void RefreshTimer(object obj)
        {
            OnOverallMeasureTimerTick?.Invoke(this);
            overallMeasureTime++;
        }


        /// <summary>
        /// Запуск измерения
        /// </summary>
        /// <param name="cycles_limit">Количество циклов измерения</param>
        public virtual void Start(int cycles_limit = 1)
        {
            if (MeasureBody != null)
            {
                cycleNumber = 0;
                cycleLimit = cycles_limit;
                if (!IsStarted)
                {
                    this.MeasureThread = new Thread(this.MeasureMainFunction);
                    MeasureThread.Start();
                }
            }
            else
            {
                throw new MeasureException("Не задана функция для измерения в поле MeasureBody!!!");
            }

        }


        /// <summary>
        /// Основной цикл измерения загружаемый в поток измерения
        /// </summary>
        private void MeasureMainFunction()
        {
            InitOverAllMeasureTimer();
            Status = MeasureCycleStatus.Started;
            do
            {
                MeasureBody();
                cycleNumber++;
                Debug.WriteLine($"MeasureBase.MeasureMainFunction() CycleNumber = {CycleNumber}");
            } while (WillMeasureContinue());
            OverallMeasureTimer.Dispose();
            Status = MeasureCycleStatus.Finished;

        }


        /// <summary>
        /// Остановка измерения
        /// </summary>
        public virtual void Stop()
        {
            if (MeasureThread != null)
            {
                Status = MeasureCycleStatus.WillFinished;
                //MeasureThread.Abort();
                //MeasureThread = null;
                //OverallMeasureTimer.Dispose();
            }else
            {
                Status = MeasureCycleStatus.Finished;
            }
        }

    

        public event MeasureHandler OnMeasureThread_Started;
        public event MeasureHandler OnMeasure;
        public event MeasureHandler OnMeasureThread_Finished;
        public event MeasureHandler OnOverallMeasureTimerTick;
        public event MeasureStatus_Handler OnStatusChanged;

        protected MeasureFunction MeasureBody;

        private Thread MeasureThread;
        private Timer OverallMeasureTimer;

        private MeasureCycleStatus status = MeasureCycleStatus.NotStarted;
        private MeasureCycleStatus status_was = MeasureCycleStatus.NotStarted;

        /// <summary>
        /// Измерение начато
        /// </summary>
        public bool IsStarted => Status == MeasureCycleStatus.Started;

        /// <summary>
        /// Флаг выхода из потока измерения
        /// </summary>
        public bool IsWillFinished => Status == MeasureCycleStatus.WillFinished;

        /// <summary>
        /// Флаг окончания результата 
        /// </summary>
        public bool IsFinished => Status == MeasureCycleStatus.Finished;

        public bool IsDeviceFound => Status != MeasureCycleStatus.DeviceNotFound;

        /// <summary>
        /// Статус измерения
        /// </summary>
        public MeasureCycleStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                status_was = status;
                status = value;

                if (status_was != status)
                {
                    OnStatusChanged?.Invoke(this, status);
                    switch (status)
                    {
                        case MeasureCycleStatus.Started:
                            OnMeasureThread_Started?.Invoke(this);
                            Debug.WriteLine($"MeasureBase.Status.Set(Started)");
                            break;
                        case MeasureCycleStatus.WillFinished:
                            Debug.WriteLine($"MeasureBase.Status.Set(WillFinished)");
                            break;
                        case MeasureCycleStatus.DeviceNotFound:
                            Debug.WriteLine($"MeasureBase.Status.Set(DeviceNotFound)");
                            break;
                        case MeasureCycleStatus.Finished:
                            OnMeasureThread_Finished?.Invoke(this);
                            Debug.WriteLine($"MeasureBase.Status.Set(Finished)");
                            break;
                    }
                }
            }
        }

        public int PauseBetweenMeasure = 10;
        protected int cycleNumber = 0;
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

    public class MeasureException : Exception
    {
        public MeasureException(string message) : base(message)
        {

        }
    }


    public enum MeasureCycleStatus
    {
        NotStarted,
        Started,
        Finished,
        WillFinished,
        DeviceNotFound
    }

}
