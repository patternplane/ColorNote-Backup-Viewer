using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorNote_Backup_Viewer.ViewModel
{
    public class MonthDayViewModel : ViewModelBase
    {
        public bool isNullDate { get; private set; }
        public bool hasData { get; private set; }
        public int day { get; private set; }
        public int dayOfWeek { get; private set; }
        public List<Model.MemoData> data { get; private set; }

        public void setData(List<Model.MemoData> data, int day, int dayOfWeek, bool isNullDate, bool hasData)
        {
            this.isNullDate = isNullDate;
            this.day = day;
            this.dayOfWeek = dayOfWeek;
            this.data = data;
            this.hasData = hasData;
            NotifyPropertyChanged(nameof(day));
            NotifyPropertyChanged(nameof(isNullDate));
            NotifyPropertyChanged(nameof(hasData));
        }
    }
}
