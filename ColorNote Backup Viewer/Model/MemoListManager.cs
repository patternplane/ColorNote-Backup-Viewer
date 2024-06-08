using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorNote_Backup_Viewer.Model
{
    public class MemoListManager
    {
        private MemoData[] memoList;

        public MemoListManager(MemoData[] memoList)
        {
            this.memoList = memoList;
        }

        public BindingList<MemoData> getMemoList()
        {
            BindingList<MemoData> result = new BindingList<MemoData>();
            foreach (MemoData m in memoList)
                result.Add(m);
            return result;
        }

        public void sortList(BindingList<MemoData> list, SortType type)
        {
            List<MemoData> temp = list.ToList();
            switch (type)
            {
                case SortType.Title :
                    temp.Sort(
                        (m1, m2) => 
                        { 
                            int result = m1.title.CompareTo(m2.title);
                            if (result == 0)
                                result = m2.date.CompareTo(m1.date);
                            if (result == 0)
                                result = m1.color.CompareTo(m2.color);
                            return result;
                        });
                    break;
                case SortType.Date:
                    temp.Sort(
                        (m1, m2) =>
                        {
                            int result = m2.date.CompareTo(m1.date);
                            if (result == 0)
                                result = m1.color.CompareTo(m2.color);
                            if (result == 0)
                                result = m1.title.CompareTo(m2.title);
                            return result;
                        });
                    break;
                case SortType.Color:
                    temp.Sort(
                        (m1, m2) =>
                        {
                            int result = m1.color.CompareTo(m2.color);
                            if (result == 0)
                                result = m2.date.CompareTo(m1.date);
                            if (result == 0)
                                result = m1.title.CompareTo(m2.title);
                            return result;
                        });
                    break;
            }
            list.Clear();
            foreach (MemoData m in temp)
                list.Add(m);
        }
    }
}
