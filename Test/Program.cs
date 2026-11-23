using FtdiSharp;

Ftdi f = new(new() { BaudRate = 460800 });

var version = f.GetLibraryVersion();
var devices = f.GetDevices();

foreach (var device in devices)
{
    var b = new Bkpu(device);

    byte[] data = [0x22, 0x22, 0, 0, 0];

    var write = await b.Write(data);

    write.Show("w");

    var res = await b.Read();

    res.Show("r");
}

public class Bkpu
{
    private FtdiDevice _device;

    public Bkpu(FtdiDevice device)
    {
        _device = device;
    }

    private byte _read = 0x75;
    private byte _write = 0x1A;

    private async Task<byte[]> Request(byte[] data)
    {
        using var channel = _device.OpenByDescription();

        channel.Write(data);

        await Task.Delay(100);

        var read = channel.GetQueueStatus();

        if (read == 0)
            return [];

        var buffer = new byte[read];
        channel.Read(buffer);

        return buffer;
    }

    public async Task<byte[]> Read() => await Request(_read.Add(new byte[8]));
    public async Task<byte[]> Write(byte[] data) => await Request(_write.Add(data.AddCrc().PushLength()));
}

public static class Extensions
{
    public static byte[] AddCrc(this byte[] data)
    {
        byte[] crc = BitConverter.GetBytes(Crc16(data));

        return [.. data, .. crc];
    }

    public static byte[] PushLength(this byte[] data)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(data.Length, byte.MaxValue, nameof(data));

        byte length = (byte)data.Length;

        return [length, .. data];
    }

    public static byte[] PushId(this byte[] data, byte id) => [id, .. data];

    public static void Show(this byte[] data, string text)
    {
        Console.WriteLine($"{text}: ");
        foreach (var item in data)
            Console.Write($"{item:x2} ");
        Console.WriteLine();
    }

    public static byte[] Add(this byte start, byte[] data) => [start, .. data];

    private static ushort Crc16(byte[] data)
    {
        ushort crc = 0xFFFF;

        const ushort polynomial = 0x1021;

        for (int i = 0; i < data.Length; i++)
        {
            crc ^= (ushort)(data[i] << 8);

            for (int j = 0; j < 8; j++)
            {
                if ((crc & 0x8000) != 0)
                {
                    crc = (ushort)((crc << 1) ^ polynomial);
                }
                else
                {
                    crc <<= 1;
                }
            }
        }

        return crc;
    }
}