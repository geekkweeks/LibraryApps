using Library.Domain.Db;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Library.Common
{
    public static class Helper
    {
        public static string TxtEncrypt(string txt)
        {
            string EncryptionKey = "UKLSIOAP89082444";
            byte[] clearBytes = Encoding.Unicode.GetBytes(txt);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    txt = Convert.ToBase64String(ms.ToArray());
                }
            }
            return txt;
        }

        public static string TxtDecrypt(string txt)
        {
            string EncryptionKey = "UKLSIOAP89082444";
            byte[] cipherBytes = Convert.FromBase64String(txt);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    txt = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return txt;
        }

        public static Dictionary<int, string> GetMasterCategoryBook()
        {
            var res = new Dictionary<int, string>();
            LibraryDBContext db = new LibraryDBContext();
            foreach(var item in db.MasterCategoryBooks.ToList())
            {
                res.Add(item.CategoryId, item.CategoryName);
            }
            return res;
            
        }

        public static int GetTotalDays(string startDate, string endDate)
        {
            var start = DateTime.ParseExact(startDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var end = DateTime.ParseExact(endDate, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            return (int)(end - start).TotalDays;
        }

        public static int GetTotalDays(DateTime startDate, DateTime endDate)
        {
            return (int)(endDate - startDate).TotalDays;
        }

        public static string ConvertToCurrency(double value)
        {
            return string.Format("{0:#.00}", Convert.ToDecimal(value));
        }
    }
}
