using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Mining
{
    public static class HashUtil
    {
        public static byte[] DoubleSHA256Hash(byte[] data)
        {
            using var sha256 = SHA256.Create();
            return sha256.ComputeHash(sha256.ComputeHash(data));
        }
    }
}
