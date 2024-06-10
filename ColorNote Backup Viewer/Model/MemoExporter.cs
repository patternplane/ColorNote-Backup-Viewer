using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorNote_Backup_Viewer.Model
{
    public class MemoExporter
    {
        private HTMLDocumentManager htmlControler;

        public MemoExporter(HTMLDocumentManager htmlControler)
        {
            this.htmlControler = htmlControler;
        }

        public void exportFile(MemoData memo, string filePathWithoutExtension, ExportType type)
        {
            if (type == ExportType.TXT) {
                string content =
                    "● Title : "
                    + memo.title + "\n"
                    + "● Text : \n"
                    + memo.text;

                byte[] data = Encoding.UTF8.GetBytes(content);

                try
                {
                    FileStream f = new FileStream(filePathWithoutExtension + ".txt", FileMode.Create);
                    StreamWriter r = new StreamWriter(f);

                    f.Write(data, 0, data.Length);

                    r.Close();
                    f.Close();
                }
                catch { }
            }
            else if (type == ExportType.HTML)
            {
                htmlControler.SaveAs(memo, filePathWithoutExtension + ".html");
            }
        }
    }
}
