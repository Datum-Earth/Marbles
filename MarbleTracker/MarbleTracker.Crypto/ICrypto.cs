using System;
using System.Collections.Generic;
using System.Text;

namespace MarbleTracker.Crypto
{
    public interface ICrypto
    {
        /// <summary>
        /// Generates a new initialization vector.
        /// </summary>
        /// <returns></returns>
        byte[] GenerateIV();

        byte[] Encrypt();
    }
}
