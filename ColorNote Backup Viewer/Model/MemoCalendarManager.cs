using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorNote_Backup_Viewer.Model
{
    public class MemoCalendarManager
    {
        private MemoData[] memoCalendar;

        public MemoCalendarManager(MemoData[] memoCalendar)
        {
            this.memoCalendar = memoCalendar;
        }

        public BindingList<MemoData> getMemoCalendar()
        {
            return new BindingList<MemoData>(memoCalendar);
        }
    }
}
