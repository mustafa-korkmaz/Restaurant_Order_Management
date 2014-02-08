using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockProgram.Reports
{
    class Period
    {
        private DateTime toDate;
        private DateTime fromDate;

        public DateTime ToDate
        {
            get {
                return this.toDate;
            }
        }
        public DateTime FromDate
        {
            get {
                return this.fromDate;
            }
        }
        public int Diff
        {
            get
            {
                return toDate.Subtract(fromDate).Days;
            }
        }
      

        public Period(DateTime fromDate,DateTime toDate)
        {
            this.toDate = toDate;
            this.fromDate = fromDate;
        }
    }
    public enum TimePeriod { Haftalik=0,Aylik,Yillik,Gunluk}

    class Periods : Period
    {
        private string timeFormat;
        private TimePeriod tp;
        public string Time
        {
            get
            {
                string retValue = string.Empty;
                switch (this.tp)
                {
                    case TimePeriod.Gunluk:
                        retValue = "Gün";
                        break;
                    case TimePeriod.Haftalik:
                        retValue= "Hafta";
                        break;
                    case TimePeriod.Aylik:
                        retValue = "Ay";
                        break;
                    case TimePeriod.Yillik:
                        retValue = "Yıl";
                        break;
                    default:
                        break;
                }
                return retValue;
            }
        }

        public Periods(DateTime fromDate,DateTime toDate,TimePeriod timePeriod)  :   base (fromDate,toDate)     
        {
            this.tp = timePeriod; 
        }
        public List<Period> GetPeriodList()
        {
            List<Period> periodList = new List<Period>();
            Period tailoredPeriod = SetPeriods(); //burada date editleri olması gerektiği zamanlara ayarladık.
            DateTime tempDate = tailoredPeriod.FromDate;
            int periodCounter = 0;

            if (tp == TimePeriod.Gunluk)
            {
                periodCounter = (tailoredPeriod.Diff + 1);

                for (int i = 0; i < periodCounter; i++)
                {
                    periodList.Add(new Period(tempDate.AddDays(i), tempDate.AddDays(i)));
                }
            }
            else if (tp==TimePeriod.Haftalik)
            {
                 periodCounter = (tailoredPeriod.Diff + 1) / 7;

                 for (int i = 0; i < periodCounter; i++)
                 {
                     periodList.Add(new Period(tempDate.AddDays(i*7),tempDate.AddDays(((i+1)*7)-1)));
                 }
            }
            else if (tp==TimePeriod.Aylik)
            {
                    decimal d = (Convert.ToDecimal((tailoredPeriod.Diff + 1)) / 30);
                    periodCounter = Convert.ToInt32(Math.Round(d));

                    for (int i = 0; i < periodCounter; i++)
                    {
                        periodList.Add(new Period(tempDate.AddMonths(i),tempDate.AddMonths(i+1).AddDays(-1)));
                    }
            }
            else if (tp == TimePeriod.Yillik)
            {
                decimal d = (Convert.ToDecimal((tailoredPeriod.Diff + 1)) / 365);
                periodCounter = Convert.ToInt32(Math.Round(d));

                for (int i = 0; i < periodCounter; i++)
                {
                    periodList.Add(new Period(tempDate.AddYears(i), tempDate.AddYears(i + 1).AddDays(-1)));
                }
            }
       
            return periodList;
        }

        private Period SetPeriods()
        {
            DateTime d1 = base.FromDate;
            DateTime d2 = base.ToDate;
            switch (tp)
            {
                case TimePeriod.Haftalik: //ilk gün pazartesi olana kadar geri git
                    while (d1.DayOfWeek!=DayOfWeek.Monday)
                    {
                       d1= d1.AddDays(-1);
                    }
                    while (d2.DayOfWeek!=DayOfWeek.Sunday) //son gün pazar olana kadar ileri git
                    {
                        d2=d2.AddDays(1);
                    }
                    break;
                case TimePeriod.Aylik:
                    d1 = d1.AddDays(-(d1.Day)+1); // tarihi o ayın ilk gününe set et
                    
                    d2 = d2.AddMonths(1); // önce 1 ay ekleyelim
                    d2 = d2.AddDays(-(d2.Day)); // önceki ayın son gününe dönmek için kaçıncı günde isen onu çıkaralım
                    break;
                case TimePeriod.Yillik:
                    d1 = new DateTime(d1.Year, 1, 1); // o yılın 1 ocak tarihi
                    d2 = new DateTime(d2.Year, 12, 31); // o yılın 31 aralık tarihi
                    break;
                default:
                    break;
            }
            return new Period(d1,d2);
        }

    }
}
