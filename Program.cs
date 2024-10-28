






using System;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;


//----------------------------1-------------------------------------------------------------


//Базовое клиент-серверное приложение
//Задача: Написать простое клиент-серверное приложение, где клиент отправляет сообщение серверу, а сервер отвечает.
//Описание: Клиент отправляет строку текста на сервер, сервер получает её и отправляет клиенту сообщение, подтверждающее получение: "Сообщение получено".
//Усложнение: Добавь проверку, что сообщение не пустое, и если оно пустое, сервер отправляет ошибку.

//TcpListener listener = new TcpListener(IPAddress.Any, 13000);
//try
//{
//    listener.Start();

//    Console.WriteLine("Сервер запущен. Ожидание подключений...");



//    while (true)
//    {
//        using var tcpClient = await listener.AcceptTcpClientAsync();
//        Console.WriteLine($"Входящее подключение: {tcpClient.Client.RemoteEndPoint}");

//        var stream = tcpClient.GetStream();



//        byte[] buffer = new byte[1024];
//        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
//        var str = Encoding.UTF8.GetString(buffer, 0, bytesRead);
//        string responseMessage;
//        if (!String.IsNullOrWhiteSpace(str))
//        {
//            responseMessage = $"Сообщение получено: \'{str}\'!";
//        }
//        else
//        {
//            responseMessage = "Ошибка: сообщение пустое.";
//        }

//        byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
//        await stream.WriteAsync(responseData, 0, responseData.Length);
//        Console.WriteLine($"Сообщение получено.");


//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
//finally
//{
//    listener.Stop();
//}





//----------------------------2-------------------------------------------------------------

//Эхо - сервер
//Задача: Написать эхо-сервер, который получает сообщения от клиента и возвращает их обратно.
//Описание: Клиент отправляет строку серверу, сервер возвращает то же сообщение. Клиент должен вводить сообщения до тех пор, пока не отправит "exit",
//после чего соединение должно быть закрыто.
//Усложнение: Добавь ограничение на количество отправленных сообщений (например, максимум 5 сообщений за одну сессию).




//TcpListener listener = new TcpListener(IPAddress.Any, 8888);
//try
//{
//    listener.Start();

//    Console.WriteLine("Сервер запущен. Ожидание подключений...");

//    while (true)
//    {
//        using var tcpClient = await listener.AcceptTcpClientAsync();
//        Console.WriteLine($"Входящее подключение: {tcpClient.Client.RemoteEndPoint}");

//        var stream = tcpClient.GetStream();


//        while (true)
//        {
//            byte[] buffer = new byte[1024];
//            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
//            var str = Encoding.UTF8.GetString(buffer, 0, bytesRead);
//            string responseMessage;
//            if (!String.IsNullOrWhiteSpace(str))
//            {
//                responseMessage = $"Сообщение получено: \'{str}\'!";
//            }
//            else
//            {
//                responseMessage = "Ошибка: сообщение пустое.";
//            }

//            byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
//            await stream.WriteAsync(responseData, 0, responseData.Length);
//            Console.WriteLine($"Сообщение получено: '{str}'!");
//        }

//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
//finally
//{
//    listener.Stop();
//}




//----------------------------3-------------------------------------------------------------

//Передача файлов по TCP
//Задача: Реализовать передачу файлов с клиента на сервер.
//Описание: Клиент отправляет файл серверу. Сервер принимает файл и сохраняет его на диске с указанием времени и даты получения.
//Усложнение: Добавь возможность передачи файлов разных типов и размеров, 
//    включая проверку целостности данных с помощью контрольной суммы (например, MD5).




//TcpListener listener = new TcpListener(IPAddress.Any, 8888);
//try
//{
//    listener.Start();
//    Console.WriteLine("Сервер запущен, ожидаем подключения...");

//    while (true)
//    {
//        using (TcpClient client = listener.AcceptTcpClient())
//        {
//            Console.WriteLine("Клиент подключен.");


//            using (NetworkStream stream = client.GetStream())

//            {

//                byte[] bufferDateTime = new byte[1024];
//                int bytesRead = stream.Read(bufferDateTime, 0, bufferDateTime.Length);
//                var fileDateTime = Encoding.UTF8.GetString(bufferDateTime, 0, bytesRead);

//                byte[] bufferName = new byte[1024];
//                int bytesName = stream.Read(bufferName, 0, bufferName.Length);
//                var fileName = Encoding.UTF8.GetString(bufferName, 0, bytesRead).Trim('\0');


//                byte[] bufferData = new byte[1024];
//                int bytesData = stream.Read(bufferData, 0, bufferData.Length);
//                var fileData = Encoding.UTF8.GetString(bufferData, 0, bytesRead);


//                byte[] bufferHash = new byte[1024];
//                int bytesHash = stream.Read(bufferHash, 0, bufferHash.Length);


//                DateTime DateStart = DateTime.Parse(fileDateTime);
//                var DateEnd = DateTime.Now;

//                var dateFull = DateEnd.Subtract(DateStart).TotalSeconds;

//                string filePath = Path.Combine("ReceivedFiles", $"{Path.GetFileNameWithoutExtension(fileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(fileName)}");
//                Directory.CreateDirectory("ReceivedFiles");
//                File.WriteAllBytes(filePath, bufferData);

//                byte[] md5Hash = getFileHash(filePath);
//                if (AreEqual(bufferHash, md5Hash))
//                {
//                    Console.WriteLine("Контрольная сумма не совпадает. Файл может быть поврежден.");
//                    return;
//                }
//                else { Console.WriteLine("--"); }

//                Console.WriteLine($"Файл '{fileName}' успешно получен и сохранен как '{filePath}' за {dateFull}секунд.");
//            }
//        }
//    }

//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
//finally
//{
//    listener.Stop();
//}
//static byte[] getFileHash(string path)
//{
//    using (MD5 md5 = MD5.Create())
//    {
//        return md5.ComputeHash(File.OpenRead(path));
//    }
//}



//static bool AreEqual(byte[] array1, byte[] array2)
//{

//    if (array1 == null && array2 == null) return true;

//    if (array1 == null || array2 == null) return false;

//    if (array1.Length != array2.Length) return false;

//    for (int i = 0; i < array1.Length; i++)
//    {
//        if (array1[i] != array2[i])
//            return false;
//    }

//    return true;
//}






//----------------------------4-------------------------------------------------------------


//Тестирование производительности TCP-соединения
//Задача: Написать программу для тестирования скорости передачи данных через TCP.
//Описание: Клиент отправляет большой объём данных (например, файл) на сервер, а сервер измеряет время передачи и рассчитывает скорость.
//Усложнение: Добавь возможность тестирования скорости в двух направлениях (сервер-клиент и клиент-сервер).


//TcpListener listener = new TcpListener(IPAddress.Any, 8888);
//try
//{
//    listener.Start();
//    Console.WriteLine("Сервер запущен, ожидаем подключения...");

//    while (true)
//    {
//        using (TcpClient client = listener.AcceptTcpClient())
//        {
//            Console.WriteLine("Клиент подключен.");


//            using (NetworkStream stream = client.GetStream())

//            {

//                byte[] bufferDateTime = new byte[1024];
//                int bytesRead = stream.Read(bufferDateTime, 0, bufferDateTime.Length);
//                var fileDateTime = Encoding.UTF8.GetString(bufferDateTime, 0, bytesRead);

//                byte[] bufferName = new byte[1024];
//                int bytesName = stream.Read(bufferName, 0, bufferName.Length);
//                var fileName = Encoding.UTF8.GetString(bufferName, 0, bytesRead).Trim('\0');


//                byte[] bufferData = new byte[1024];
//                int bytesData = stream.Read(bufferData, 0, bufferData.Length);
//                var fileData = Encoding.UTF8.GetString(bufferData, 0, bytesRead);

//                DateTime DateStart = DateTime.Parse(fileDateTime);
//                var DateEnd = DateTime.Now;

//                var dateFull = DateEnd.Subtract(DateStart).TotalSeconds;

//                string filePath = Path.Combine("ReceivedFiles", $"{Path.GetFileNameWithoutExtension(fileName)}_{DateTime.Now:yyyyMMdd_HHmmss}{Path.GetExtension(fileName)}");
//                Directory.CreateDirectory("ReceivedFiles");
//                File.WriteAllBytes(filePath, bufferData);

//                Console.WriteLine($"Файл '{fileName}' успешно получен и сохранен как '{filePath}' за {dateFull}секунд.");
//                FileInfo fileInfo = new FileInfo(filePath);
//                Console.WriteLine($"Скорость передачи файла {fileInfo.Length / dateFull} b/sek ");



//            }
//        }
//    }

//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
//finally
//{
//    listener.Stop();
//}









//----------------------------5-------------------------------------------------------------
//Клиент - серверная система запросов и ответов
//Задача: Реализовать сервер, который может отвечать на запросы клиента, например, по времени, дате или другой простой информации.
//Описание: Клиент отправляет запрос "Время", сервер отвечает текущим временем. При запросе "Дата" сервер возвращает текущую дату. При запросе "exit" соединение закрывается.
//Усложнение: Реализуй поддержку нескольких команд и добавь команду, которая возвращает случайную цитату.




//var tcpListener = new TcpListener(IPAddress.Any, 8888);

//try
//{
//    tcpListener.Start();   
//    Console.WriteLine("Сервер запущен. Ожидание подключений... ");

//    while (true)
//    {

//        var tcpClient = await tcpListener.AcceptTcpClientAsync();


//        Task.Run(async () => await ProcessClientAsync(tcpClient));

//    }
//}
//finally
//{
//    tcpListener.Stop();
//}

//async Task ProcessClientAsync(TcpClient tcpClient)
//{

//    var words = new Dictionary<string, string>()
//    {
//        {"Время", DateTime.Now.ToString("HH:mm:ss")},
//        {"Дата", DateTime.Now.ToString("yyyy-MM-dd")},
//    };
//    var stream = tcpClient.GetStream();

//    var response = new List<byte>();
//    int bytesRead = 10;
//    while (true)
//    {

//        while ((bytesRead = stream.ReadByte()) != '\n')
//        {

//            response.Add((byte)bytesRead);
//        }
//        var word = Encoding.UTF8.GetString(response.ToArray());

//        if (word == "exit") break;

//        Console.WriteLine($"Клиент {tcpClient.Client.RemoteEndPoint} запросил {word}");

//        if (!words.TryGetValue(word, out var translation)) translation = "не найдено в словаре";

//        translation += '\n';

//        await stream.WriteAsync(Encoding.UTF8.GetBytes(translation));
//        response.Clear();
//    }
//    tcpClient.Close();
//}

















//----------------------------6-------------------------------------------------------------

//Средний уровень: Реализовать сервер, который может принимать несколько клиентов одновременно. Каждый клиент отправляет своё имя, а сервер возвращает приветственное сообщение "Привет, {имя}!" для каждого клиента.
//Цель: использование многопоточности для работы с несколькими клиентами.
//Результат: сервер должен обрабатывать несколько клиентов параллельно.

//TcpListener listener = new TcpListener(IPAddress.Any, 13000);
//try
//{
//    listener.Start();
//    Console.WriteLine("Сервер запущен. Ожидание подключений... ");


//    while (true)
//    {
//        using var tcpClient = await listener.AcceptTcpClientAsync();
//        Console.WriteLine($"Входящее подключение: {tcpClient.Client.RemoteEndPoint}");

//        //// получаем объект NetworkStream
//        var stream = tcpClient.GetStream();



//        byte[] buffer = new byte[1024];
//        int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
//        var name = Encoding.UTF8.GetString(buffer, 0, bytesRead);
//        var responseMessage = $"Привет, {name}!";


//        byte[] responseData = Encoding.UTF8.GetBytes(responseMessage);
//        await stream.WriteAsync(responseData, 0, responseData.Length);
//        Console.WriteLine($"Отправлено: {responseMessage}");


//    }
//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
//finally
//{
//    listener.Stop();
//}







//----------------------------7-------------------------------------------------------------
//Реализация протокола передачи сообщений с подтверждением
//Задача: Написать приложение, в котором клиент отправляет данные, а сервер должен отправить подтверждение о получении каждого пакета.
//Описание: Клиент отправляет сообщение, сервер его получает и отвечает "ACK". Если клиент не получил ответ в течение определённого времени, он должен повторить отправку.
//Усложнение: Добавь возможность отправки нескольких сообщений подряд с проверкой получения каждого.


//TcpListener listener = new TcpListener(IPAddress.Any, 8888);
//try
//{
//    listener.Start();

//    Console.WriteLine("Сервер запущен. Ожидание подключений...");



//    while (true)
//    {

//        using var tcpClient = await listener.AcceptTcpClientAsync();
//        Console.WriteLine($"Входящее подключение: {tcpClient.Client.RemoteEndPoint}");


//        var stream = tcpClient.GetStream();
//        Random random = new Random();
//        while (true)
//        {
//            byte[] buffer = new byte[1024];
//            int bytesRead = stream.Read(buffer, 0, buffer.Length);
//            if (bytesRead == 0) break;

//            string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
//            Console.WriteLine($"Получено сообщение: '{message}'");


//            int delay = random.Next(1000, 3001); 
//            Thread.Sleep(delay);
//            byte[] responseData = Encoding.UTF8.GetBytes("ACK");
//            stream.Write(responseData, 0, responseData.Length);
//        }

//    }


//}
//catch (Exception ex)
//{
//    Console.WriteLine(ex.Message);
//}
//finally
//{
//    listener.Stop();
//}
