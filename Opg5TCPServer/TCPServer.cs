using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using BogLibrary;
using Newtonsoft.Json;

namespace Opg5TCPServer
{
    class TCPServer
    {
        static void Main(string[] args)
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener serverSocket = new TcpListener(ip, 4646);
            serverSocket.Start();
            Console.WriteLine("Server activated");

            while (true)
            {
                TcpClient socket = serverSocket.AcceptTcpClient();
                Console.WriteLine("client accepted");

                Task.Run(() =>
                {
                    TcpClient tempSocket = socket;
                    DoClient(tempSocket);
                });
            }
            
        }

        private static readonly List<Bog> library = new List<Bog>()
        {
            new Bog("Mester Jakob", "Paul", 323, "1234567890123"),
            new Bog("Robin Hood", "Karl", 423, "2345678901234"),
            new Bog("David", "Caspar", 163, "3456789012345"),
            new Bog("Lord of the Rings", "Talia", 247, "4567890123456"),

        };


        // Stream ns = new NetworkStream(connectionSocket);
        public static void DoClient(TcpClient socket)
        {
            using (socket)
            {


                Stream ns = socket.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.AutoFlush = true; // enable automatic flushing

                string message;
              

                while (true)
                {
                    message = sr.ReadLine();
                    string[] splitStrings = message.Split(" ", 2);
                    if (splitStrings[0] == "GetAll")
                    {
                        sw.WriteLine(JsonConvert.SerializeObject(library));
                    }

                    if (splitStrings[0] == "Get")
                    {
                        if (splitStrings.Length == 2)
                        {
                            Bog book = library.Find(x => x.Isbn13 == splitStrings[1]);
                            sw.WriteLine(JsonConvert.SerializeObject(book));
                        }
                    }

                    if (splitStrings[0] == "Save")
                    {
                        if (splitStrings.Length >= 2)
                        {
                            Bog newBook = JsonConvert.DeserializeObject<Bog>(splitStrings[1]);
                            Console.WriteLine("adding new book " + newBook.Titel);
                            library.Add(newBook);
                        }
                    }

                    Console.WriteLine("Client" + message);
                    
                }

            }


        }


    }




}
