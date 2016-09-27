using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;


namespace Sorting.Dispatching
{
    public static class ZipFloClass
    {
        public static void ZipFile(string strFile, string fileName, string strZip)
        {
            ZipOutputStream s = new ZipOutputStream(File.Create(strZip));
            s.SetLevel(6); // 0 - store only to 9 - means best compression

            //byte[] buffer = new byte[4096];

            //ZipEntry entry = new ZipEntry(fileName);
            //entry.DateTime = DateTime.Now;
            //s.PutNextEntry(entry);
            //using (FileStream fs = File.OpenRead(strFile))
            //{
            //    int sourceBytes;
            //    do
            //    {
            //        sourceBytes = fs.Read(buffer, 0, buffer.Length);
            //        s.Write(buffer, 0, sourceBytes);
            //    } while (sourceBytes > 0);
            //}



            zip(strFile, s, fileName);
            s.Finish();
            s.Close();
        }

        private static void zip(string strFile, ZipOutputStream s, string staticFile)
        {
            Crc32 crc = new Crc32();

            //打开压缩文件
            FileStream fs = File.OpenRead(strFile);

            byte[] buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);

            ZipEntry entry = new ZipEntry(staticFile);

            entry.DateTime = DateTime.Now;
            entry.Size = fs.Length;
            fs.Close();
            crc.Reset();
            crc.Update(buffer);
            entry.Crc = crc.Value;
            s.PutNextEntry(entry);

            s.Write(buffer, 0, buffer.Length);
        }
    }
}
