using System;
using System.Security.Cryptography;
using System.Text;

String input = "yzbqklnj";
int i = 1;
Boolean found = false;

while (!found)
{
    String key = input + i;
    String hash = Convert.ToHexString(MD5.HashData(ASCIIEncoding.ASCII.GetBytes(key)));
    if (hash.StartsWith("000000"))
    {
        found = true;
        Console.WriteLine(key);
    }
    i++;
}