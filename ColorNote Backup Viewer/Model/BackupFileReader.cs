using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ColorNote_Backup_Viewer.Model
{
    public class BackupFileReader
    {
        private string filePath;
        private byte[] lastDecryptedData;
        private long fileBackupDate;

        public BackupFileReader()
        {
            this.lastDecryptedData = null;
        }

        public bool setFilePath(string path)
        {
            try
            {
                new FileStream(path, FileMode.Open).Close();
                this.filePath = path;
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public bool setPassword(string password)
        {
            try
            {
                FileStream f = new FileStream(filePath, FileMode.Open);
                BinaryReader sr = new BinaryReader(f);

                List<byte> temp = new List<byte>();
                byte[] temp2;
                while ((temp2 = sr.ReadBytes(100)).Length != 0)
                    temp.AddRange(temp2);
                byte[] rawData = temp.ToArray();

                
                lastDecryptedData = AESDecryptor.decryptBackupFile(rawData, password);
                if (BitConverter.IsLittleEndian)
                    fileBackupDate = System.Buffers.Binary.BinaryPrimitives.ReadInt64BigEndian(new ReadOnlySpan<byte>(rawData, 16, 8));
                else
                    fileBackupDate = BitConverter.ToInt64(rawData, 16);

                f.Close();
                sr.Close();
            }
            catch
            {
                lastDecryptedData = null;
            }

            return (lastDecryptedData != null);
        }

        public BackupFileData getDataFromFile()
        {
            MemoData[] data = getMemoData();
            if (data.Length > 0)
                return new BackupFileData(data, filePath, fileBackupDate);
            else
                return null;
        }

        private MemoData[] getMemoData()
        {
            MemoJSONParser jsonParser = new MemoJSONParser();
            List<MemoData> result = new List<MemoData>();

            FileStream f= new FileStream(@"C:\Users\patternplane\Desktop\결과.txt", FileMode.Create);
            BinaryWriter sr = new BinaryWriter(f);
            sr.Write(lastDecryptedData);
            sr.Close();
            f.Close();

            foreach (string eachMemoJson in jsonParser.parse(Encoding.UTF8.GetChars(lastDecryptedData)))
            {
                try
                {
                    rawMemoData rawMemo = System.Text.Json.JsonSerializer.Deserialize<rawMemoData>(eachMemoJson);
                    
                    if (rawMemo.encrypted == 1)
                    {
                        rawMemo.note = Encoding.UTF8.GetString(
                            AESDecryptor.decryptEncryptedNote(Convert.FromBase64String(rawMemo.note)));
                    }

                    if (rawMemo.folder_id == 0 || rawMemo.folder_id == 16
                        || rawMemo.active_state == 0 || rawMemo.active_state == 16)
                        if ((rawMemo.title != null && rawMemo.title.Length > 0)
                            || (rawMemo.note != null && rawMemo.note.Length > 0))
                            result.Add(
                                new MemoData(
                                    rawMemo.title,
                                    rawMemo.note,
                                    rawMemo.modified_date,
                                    rawMemo.folder_id == 16,
                                    rawMemo.reminder_base,
                                    rawMemo.active_state == 16,
                                    rawMemo.color_index));
                        }
                catch { }
            }
            return result.ToArray();
        }
    }
}
