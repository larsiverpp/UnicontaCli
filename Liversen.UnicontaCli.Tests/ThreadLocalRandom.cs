using System;
using System.Security.Cryptography;

namespace Liversen.UnicontaCli;

public static class ThreadLocalRandom
{
    public static int Next(int maxValue = int.MaxValue) => RandomNumberGenerator.GetInt32(maxValue);

    public static int Next(int minValue, int maxValue) => RandomNumberGenerator.GetInt32(minValue, maxValue);

    public static short NextShort() => (short)Next(short.MaxValue);

    public static short NextShort(short maxValue) => (short)Next(maxValue);

    public static short NextShort(short minValue, short maxValue) => (short)Next(minValue, maxValue);

    public static double NextDouble()
    {
        var bytes = new byte[8];
        RandomNumberGenerator.Fill(bytes);
        var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
        return ul / (double)(1UL << 53);
    }

    public static decimal NextDecimal() => (decimal)NextDouble();

    public static void NextBytes(byte[] buffer) => RandomNumberGenerator.Fill(buffer);

    public static bool NextBool() => Next(2) == 1;

    public static char NextDigitChar() => (char)('0' + Next(10));

    public static char NextLcAlphaChar() => (char)('a' + Next(26));

    public static char NextUcAlphaChar() => (char)('A' + Next(26));

    public static char NextUcAlphaOrDigitChar() => NextBool() ? NextDigitChar() : NextUcAlphaChar();

    public static string NextDigitString(int length) => NextString(length, '0', 10);

    public static string NextLcAlphaString(int length) => NextString(length, 'a', 26);

    public static string NextUcAlphaString(int length) => NextString(length, 'A', 26);

    public static string NextUcAlphaOrDigitString(int length)
    {
        var buffer = new byte[length];
        NextBytes(buffer);
        var chars = new char[length];
        for (var i = 0; i < length; ++i)
        {
            chars[i] = (buffer[i] & 128) != 0 ? (char)('0' + (buffer[i] % 10)) : (char)('A' + (buffer[i] % 26));
        }

        return new(chars);
    }

    static string NextString(int length, char start, int range)
    {
        var buffer = new byte[length];
        NextBytes(buffer);
        var chars = new char[length];
        for (var i = 0; i < length; ++i)
        {
            chars[i] = (char)(start + (buffer[i] % range));
        }

        return new(chars);
    }
}
