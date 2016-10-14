using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SAK_2016
{
    public class manualTestThread
    {
        public Thread thread;
        public manualTestForm mTest;
        public manualTestThread(manualTestForm f)
        {
            this.mTest = f;
            this.thread = new Thread(this.measure);
            thread.Start();
        }

        void measure()
        {
            int result;
            do
            {
                if (Properties.Settings.Default.isTestApp) result = mTest.mMain.getFakeResult();
                else result = mTest.mMain.getResult();
                mTest.updateResultField(result);
                pause();
            } while (true);
        }

        void pause()
        {
            Thread.Sleep(500);
        }
    }
}
