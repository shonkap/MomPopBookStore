using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Mvvm;

namespace BookStore.Models
{
    public class Book : BindableBase
    {
        public Int64 ID { get; set; }
        public string Bname { get; set; }
        public string Gbook { get; set; }
        public string Pbook { get; set; }
        public string Stockbook { get; set; }

        public bool Removecheck { get; set; }

        public Book(int tID, string name, string genre, string price, string stock, bool remcheck = false)
        {
            ID = tID;
            Bname = name;
            Gbook = genre;
            Pbook = price;
            Stockbook = stock;
            Removecheck = remcheck;
        }
    }
}
