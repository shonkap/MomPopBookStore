using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Commands;
using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BookStore.Models;
using System.Windows.Controls;

using System.Collections.Concurrent;
using System.Threading;


namespace BookStore.ViewModels
{
    class MainWindowModels : BindableBase
    {

        public DataGrid stuff;
        private Int64 curitemID = -1;

        //data export
        ConcurrentQueue<ExportQueue> cq = new ConcurrentQueue<ExportQueue>();
        private DataExport exportfunc = new DataExport();
        System.Windows.Threading.DispatcherTimer dispatcherTimer;
        private Thread exportThread;
        bool keepexpthreadalive = false;


        //sorting values
        private int storedsearchval = 0;
        private int storedsortval = 0;
        private string storedsearchstr = "";
        private List<Book> filtlist;

        private bool query = false;


        private ObservableCollection<Book> _books;
        public ObservableCollection<Book> Books
        {
            get { return _books; }
            set { SetProperty(ref _books, value); }
        }

        private DelegateCommand<Book> _deleteCommand;
        public DelegateCommand<Book> DeleteCommand =>
            _deleteCommand ?? (_deleteCommand = new DelegateCommand<Book>(ExecuteCommandName));


        void ExecuteCommandName(Book paramter)
        {
            //Books = cancel;
            Books.Remove(paramter);

            if (query == true)
                searchdata(storedsearchval, storedsearchstr);

            booklibrary();
        }

        //checks the database thread thing is still alive
        private void threadcheck(object sender, EventArgs e)
        {
            if (exportThread.IsAlive == false && keepexpthreadalive == true)
            {
                exportThread.Start();
            }
        }

        public MainWindowModels()
        {
            List<Book> TempBook = exportfunc.readcsv();

            Books = new ObservableCollection<Book>();

            if(TempBook != null)
            {
                foreach (Book book in TempBook)
                {
                    Books.Add(book);
                }
            }
            else
            {
                Books.Add(new Book(1, "Test1", "Tests", "10.1", "1", true));
            }

            //Books.Add(new Book(1, "Test1", "Tests", "10.1", "1", true));
            //Books.Add(new Book(2, "Test2", "Tests", "15.1", "1", true));
            //Books.Add(new Book(3, "Test3", "Tests", "16.1", "1"));
            //Books.Add(new Book(4, "Test4", "Tests", "103.1", "1"));
            //Books.Add(new Book(5, "Test5", "Tests", "105.1", "1"));
            //Books.Add(new Book(6, "Test6", "Tests", "10.5", "1"));

            curitemID = Books[Books.Count - 1].ID;

            filtlist = Books.Cast<Book>().ToList();

            exportfunc.setqueue(cq);
            exportThread = new Thread(exportfunc.monitorDatabase);
            exportThread.Start();
            keepexpthreadalive = true;


            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(threadcheck);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
        }



        //Public helper functions

        public void threadabort()
        {
            if (exportThread.IsAlive)
            {
                //exportThread.Abort();
            }
        }
        //update the database very inefficiently
        public void booklibrary(int endval = -1)
        {
            if (endval == 1)
                keepexpthreadalive = false;

            ExportQueue exq = new ExportQueue();
            exq.Books = Books.Cast<Book>().ToList();
            exq.end = endval;
            cq.Enqueue(exq);
            //exportfunc.monitorDatabase();
        }
        public void celledit()
        {
            searchdata(storedsearchval, storedsearchstr);

            booklibrary();
        }

        public void addBook(Book Newbook)
        {
            //clear the filters
            searchdata();
            filtdata();

            if(Books[Books.Count - 1].Bname == "" || Books[Books.Count - 1].Gbook == "" || Books[Books.Count - 1].Pbook == "" || Books[Books.Count - 1].Stockbook == "")
            {
                var listvar1 = Books
                .Where(item => item.Bname == "" || item.Gbook == "" || item.Pbook == "" || item.Stockbook == "")
                .Select(item => item);

                stuff.ItemsSource = listvar1.Cast<Book>().ToList();
                //filtlist = Books.Cast<Book>().ToList();

                return;
            }

            Newbook.ID = curitemID+1;
            curitemID++;

            Books.Add(Newbook);
            if (query == true)
                searchdata(storedsearchval, storedsearchstr);

            booklibrary();
        }

        public void searchdatahelper()
        {
            searchdata(storedsearchval, storedsearchstr);
        }

        //search data
        public void searchdata(int sortval = 0, string searchstr = "")
        {
            storedsearchval = sortval;
            storedsearchstr = searchstr;

            if (sortval == 0) //show all
            {
                storedsearchstr = "";
                query = false;
                stuff.ItemsSource = Books;
                filtlist = Books.Cast<Book>().ToList();
            }

            if(sortval == 2) //search book
            {
                query = true;

                var listvar1 = Books
                .Where(item => item.Bname.ToLower().Contains(searchstr.ToLower()))
                .Select(item => item);

                stuff.ItemsSource = listvar1.Cast<Book>().ToList();
                filtlist = listvar1.Cast<Book>().ToList();
            }
            if (sortval == 3)//search genre
            {
                query = true;

                var listvar2 = Books
                .Where(item => item.Gbook.ToLower().Contains(searchstr.ToLower()))
                .Select(item => item);

                stuff.ItemsSource = listvar2.Cast<Book>().ToList();
                filtlist = listvar2.Cast<Book>().ToList();
            }

            if (storedsortval != 0)
            {
                filtdata(storedsortval);
            }
        }

        //filter data
        public void filtdata(int sortval = 0)
        {
            storedsortval = sortval;

            if (sortval == 0) //show all
            {
                query = false;
                stuff.ItemsSource = Books;
                filtlist = Books.Cast<Book>().ToList();

                return;
            }

            //ascen descend
            if (sortval == 4)
            {
                query = true;

                var listvar2 = filtlist
                .Where(item => item.Stockbook != "")
                .OrderBy(item => float.Parse(item.Stockbook))
                .Select(item => item);

                stuff.ItemsSource = listvar2.Cast<Book>().ToList();
                return;
            }
            if (sortval == 5)
            {
                query = true;

                var listvar2 = filtlist
                .Where(item => item.Stockbook != "")
                .OrderByDescending(item => float.Parse(item.Stockbook))
                .Select(item => item);

                stuff.ItemsSource = listvar2.Cast<Book>().ToList();
                return;
            }

            if (sortval == 6)
            {
                query = true;

                var listvar2 = filtlist
                .Where(item => item.Pbook != "")
                .OrderBy(item => float.Parse(item.Pbook))
                .Select(item => item);

                stuff.ItemsSource = listvar2.Cast<Book>().ToList();
                return;
            }
            if (sortval == 7)
            {
                query = true;

                var listvar2 = filtlist
                .Where(item => item.Pbook != "")
                .OrderByDescending(item => float.Parse(item.Pbook))
                .Select(item => item);

                stuff.ItemsSource = listvar2.Cast<Book>().ToList();
                return;
            }
        }
    }
}
