﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MCTest.Cryptography
{
    public static class JavaHexDigest
    {
        public static string Create(string data)
        {
            var sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(data));
            bool negative = (hash[0] & 0x80) == 0x80;
            if (negative) // check for negative hashes
                hash = TwosCompliment(hash);
            // Create the string and trim away the zeroes
            string digest = GetHexString(hash).TrimStart('0');
            if (negative)
                digest = "-" + digest;
            return digest;
        }


        private static string GetHexString(byte[] p)
        {
            string result = "";
            for (int i = 0; i < p.Length; i++)
            {
                if (p[i] < 0x10)
                    result += "0";
                result += p[i].ToString("x"); // Converts to hex string
            }
            return result;
        }
        private static byte[] TwosCompliment(byte[] p) // little endian
        {
            int i;
            bool carry = true;
            for (i = p.Length - 1; i >= 0; i--)
            {
                p[i] = (byte)~p[i];
                if (carry)
                {
                    carry = p[i] == 0xFF;
                    p[i]++;
                }
            }
            return p;
        }
    }
}
