using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookStore.Models;
using System.IO;
using System.Collections.Concurrent;

namespace BookStore
{
    class DataExport
    {
        private List<Book> library;
        private ConcurrentQueue<ExportQueue> cq;
        private bool run = true;

        private string pathvar = "";

        public List<Book> readcsv()
        {
            List<Book> books = new List<Book> ();
            pathvar = System.Environment.CurrentDirectory;

            if (File.Exists(pathvar + "\\Library.csv"))
            {
                using (var reader = new StreamReader(pathvar + "\\Library.csv"))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        Book tempb = new Book(Int32.Parse(values[0]), values[1], values[2], values[3], values[4]);
                        books.Add(tempb);
                    }
                }
            }

            return books;
        }
        public void setqueue(ConcurrentQueue<ExportQueue> cqq)
        {
            cq = cqq;
        }

        public void monitorDatabase()
        {
            try
            {
                List<Book> last = null;
                while (run == true)
                {
                    ExportQueue booktemp;
                    while (cq.TryDequeue(out booktemp))
                    {
                        last = booktemp.Books;
                        if (booktemp.end == 1)
                            run = false;
                    }
                    if (last != null)
                    {
                        library = last;
                        writetofile();
                    }
                }

                ExportQueue book;
                while (cq.TryDequeue(out book))
                {
                    last = book.Books;
                }
                //writetofile();
                cq.Clear();
            }
            catch (Exception ex)
            {
                return;
            }
            return;

        }

        private int writetofile()
        {
            try
            {
                using StreamWriter file = new(pathvar + "\\Library.tmp");

                foreach (var item in library)
                {
                    file.WriteLine(item.ID + "," + item.Bname + "," + item.Gbook + "," + item.Pbook + "," + item.Stockbook);
                }
            }
            catch (Exception ex)
            {
                return -1;
            }

            int changefilename = -1;
            int cnt = 0;

            //delete old temp file
            try
            {
                File.Delete(pathvar + "\\Library_old.tmp");
            }
            catch(Exception ex)
            {

            }


            while (changefilename == -1 && cnt < 15)
            {
                try
                {
                    if (File.Exists(pathvar + "\\Library.csv"))
                    {
                        File.Move(pathvar + "\\Library.csv", pathvar + "\\Library_old.tmp");
                    }
                    

                    if (!(File.Exists(pathvar + "\\Library.csv")))
                    {
                        if (File.Exists(pathvar + "\\Library.tmp"))
                        {
                            File.Move(pathvar + "\\Library.tmp", pathvar + "\\Library.csv");
                            File.Delete(pathvar + "\\Library_old.tmp");
                            return 1;
                        }
                    }
                    //change file name
                }
                catch (Exception ex)
                {
                    cnt++;
                }
            }

            return 1;
        }
    }

    class ExportQueue
    {
        private int _end = -1;
        public int end
        {
            get { return _end; }
            set { _end = value; }
        }
        private List<Book> book;
        public List<Book> Books
        {
            get { return book; }
            set { book = value; }
        }
    }

}
