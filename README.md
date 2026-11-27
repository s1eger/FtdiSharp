# FtdiSharp

Управляемая библиотека для работы с FTDI-устройствами через нативный драйвер **D2XX**. FtdiSharp предоставляет объектно-ориентированный API для обнаружения устройств, настройки последовательного обмена, передачи данных, управления сигналами и работы с EEPROM.

Библиотека обращается к D2XX напрямую через P/Invoke и не использует виртуальный COM-порт.

## Возможности

- обнаружение подключённых устройств и получение их параметров;
- открытие устройства по индексу, серийному номеру, описанию или расположению;
- синхронное чтение и запись массивов байтов;
- настройка скорости, формата данных, чётности и управления потоком;
- управление DTR, RTS, Break, таймаутами и latency timer;
- получение состояния очередей, модема, драйвера и номера COM-порта;
- сброс устройства и порта, очистка очередей, настройка USB-параметров;
- работа с битовыми режимами FTDI;
- чтение, запись и очистка EEPROM и пользовательской области;
- выполнение vendor-specific команд;
- создание пользовательских описаний устройств через фабрики;
- автоматическое освобождение нативного дескриптора через `SafeHandle`.

## Требования

- **.NET 10** — SDK для сборки проекта или Runtime для приложения, использующего библиотеку.
- Нативный драйвер **FTDI D2XX**, установленный и доступный загрузчику библиотек ОС.
- Подключённое FTDI-устройство для выполнения операций с оборудованием.

Драйверы D2XX доступны на [сайте FTDI](https://ftdichip.com/drivers/d2xx-drivers/). Для сборки управляемого проекта физическое устройство не требуется.

## Установка

Подключите пакет `FTDI-Sharp` к своему проекту:

```bash
dotnet add package FTDI-Sharp
```

Для использования локальной копии репозитория добавьте ссылку на проект:

```bash
dotnet add reference путь/к/FtdiSharp/FtdiSharp/FtdiSharp.csproj
```

## Быстрый старт

```csharp
using System.Text;
using FtdiSharp;

var settings = new FtdiSettings
{
    BaudRate = 115200,
    DataBits = FtdiDataBits.BITS_8,
    StopBits = FtdiStopBits.ONE,
    Parity = FtdiParity.NONE,
    FlowControl = FtdiFlowControl.NONE,
    ReadTimeout = 1000,
    WriteTimeout = 1000
};

var ftdi = new Ftdi(settings);
var devices = ftdi.GetDevices();

if (devices.Count == 0)
{
    Console.WriteLine("FTDI-устройства не найдены.");
    return;
}

foreach (var device in devices)
{
    Console.WriteLine(
        $"{device.Description} | S/N: {device.Serial} | Location: {device.Location}");
}

using var channel = devices[0].OpenBySerial();

byte[] request = Encoding.ASCII.GetBytes("Hello, FTDI!");
uint written = channel.Write(request);

byte[] response = new byte[1024];
uint read = channel.Read(response);

Console.WriteLine($"Отправлено байтов: {written}");
Console.WriteLine($"Получено байтов: {read}");
Console.WriteLine(Encoding.ASCII.GetString(response, 0, (int)read));
```

При открытии канал сбрасывает устройство, применяет параметры из `FtdiSettings` и очищает очереди согласно `PurgeFlags`. По умолчанию очищаются обе очереди.

Методы `Read` и `Write` возвращают фактическое количество обработанных байтов, которое может быть меньше размера переданного буфера. `Read` также может вернуть `0`, если данные не поступили до истечения таймаута. Количество байтов, уже доступных в приёмной очереди, можно узнать через `GetQueueStatus()`.

Канал реализует `IDisposable`: освобождайте его с помощью `using` или явно вызывайте `Dispose()` / `Close()`.

## Настройки соединения

`FtdiSettings` задаёт конфигурацию, применяемую при открытии канала:

| Свойство | Значение по умолчанию | Назначение |
| --- | ---: | --- |
| `BaudRate` | `9600` | Скорость передачи |
| `DataBits` | `BITS_8` | Количество бит данных |
| `StopBits` | `ONE` | Количество стоп-битов |
| `Parity` | `NONE` | Режим чётности |
| `FlowControl` | `NONE` | Управление потоком |
| `ReadTimeout` | `1000` | Таймаут чтения, мс |
| `WriteTimeout` | `1000` | Таймаут записи, мс |
| `Latency` | `16` | Latency timer, мс |
| `BitMode` | `RESET` | Битовый режим FTDI |
| `Mask` | `0` | Маска выводов для битового режима |
| `PurgeFlags` | `BOTH` | Очереди, очищаемые после настройки |
| `Xon` / `Xoff` | `0x11` / `0x13` | Символы программного управления потоком |

Настройки можно повторно применить к открытому каналу через `ApplySettings`. Учтите, что этот метод также сбрасывает устройство и очищает выбранные очереди.

## Открытие устройства

Канал можно открыть напрямую через `Ftdi`:

```csharp
// Выберите один подходящий способ:
using var channel = ftdi.OpenByIndex(0);
// using var channel = ftdi.OpenBySerial("FT123456");
// using var channel = ftdi.OpenByDescription("USB Serial Converter");
// using var channel = ftdi.OpenByLocation(0x1234);
```

Если описание устройства уже получено через `GetDevices()`, доступны методы `OpenBySerial()`, `OpenByDescription()` и `OpenByLocation()` самого объекта `FtdiDevice`.

## Обработка ошибок

Если D2XX возвращает ошибочный статус, библиотека выбрасывает `FtdiException`. Код состояния доступен через свойство `Status`:

```csharp
try
{
    using var channel = ftdi.OpenBySerial("FT123456");
    // Работа с устройством.
}
catch (FtdiException ex)
{
    Console.WriteLine($"Ошибка FTDI: {ex.Status}");
}
```

Ошибки загрузки нативной библиотеки D2XX и ошибки управляемого кода не преобразуются в `FtdiException` и могут выбрасываться как стандартные исключения .NET.

## Пользовательские типы устройств

По умолчанию `GetDevices()` возвращает объекты `FtdiDevice`, созданные через `DefaultFtdiFactory`. Чтобы получить собственный тип, реализуйте `IFtdiDevice` и `IFtdiDeviceFactory<T>`:

```csharp
using FtdiSharp.Abstractions;

public sealed class MyDevice : IFtdiDevice
{
    public FtdiDeviceFlags Flags { get; init; }
    public FtdiDeviceType Type { get; init; }
    public uint Id { get; init; }
    public uint Location { get; init; }
    public string Serial { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
}

public sealed class MyDeviceFactory : IFtdiDeviceFactory<MyDevice>
{
    public MyDevice Create(
        FtdiDeviceFlags flags,
        FtdiDeviceType type,
        uint id,
        uint location,
        string serial,
        string description) => new()
    {
        Flags = flags,
        Type = type,
        Id = id,
        Location = location,
        Serial = serial,
        Description = description
    };
}
```

Передайте фабрику в обобщённый метод:

```csharp
IReadOnlyList<MyDevice> devices = ftdi.GetDevices(new MyDeviceFactory());
MyDevice device = ftdi.GetDeviceInfoDetail(0, new MyDeviceFactory());
```

## Основные компоненты

| Компонент | Назначение |
| --- | --- |
| `Ftdi` / `IFtdi` | Поиск устройств, получение сведений и открытие каналов |
| `FtdiDevice` / `IFtdiDevice` | Описание обнаруженного устройства |
| `FtdiChannel` / `IFtdiChannel` | Обмен данными и управление открытым устройством |
| `FtdiSettings` | Параметры соединения, применяемые к каналу |
| `IFtdiDeviceFactory<T>` | Создание пользовательских описаний устройств |
| `DefaultFtdiFactory` | Стандартная фабрика объектов `FtdiDevice` |
| `FtdiException` | Ошибка D2XX с кодом `FtdiStatus` |
| `FtdiSafeHandle` | Владение и освобождение нативного дескриптора |
| `FtdiNative` | Внутренние объявления P/Invoke для D2XX |

## Структура проекта

```text
FtdiSharp/
├── Abstractions/
│   ├── IFtdi.cs
│   ├── IFtdiChannel.cs
│   ├── IFtdiDevice.cs
│   └── IFtdiDeviceFactory.cs
├── Native/
│   ├── FtdiNative.cs
│   └── FtdiSafeHandle.cs
├── Ftdi.cs
├── FtdiChannel.cs
├── FtdiDevice.cs
├── FtdiSettings.cs
├── FtdiEnums.cs
├── FtdiException.cs
├── FtdiExtensions.cs
├── DefaultFtdiFactory.cs
└── FtdiSharp.csproj
```

## Лицензия

Проект распространяется по лицензии [MIT](LICENSE).
