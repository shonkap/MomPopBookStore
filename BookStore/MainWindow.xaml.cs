using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using BookStore.ViewModels;
using BookStore.Models;

namespace BookStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowModels DataInfo;
        public MainWindow()
        {
            InitializeComponent();

            DataInfo = new MainWindowModels();
            DataContext = DataInfo; //idk wtf this does but it needed
            DataInfo.stuff = dataGrid;

        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            DataInfo.booklibrary(1);
        }

        //###################
        //Data Grid data Functions
        //###################
        private void _showCellsEditingTemplate(DataGridRow row)
        {
            foreach (DataGridColumn col in dataGrid.Columns)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(col.GetCellContent(row));
                while (parent.GetType().Name != "DataGridCell")
                    parent = VisualTreeHelper.GetParent(parent);

                DataGridCell cell = ((DataGridCell)parent);
                DataGridTemplateColumn c = (DataGridTemplateColumn)col;
                if (c.CellEditingTemplate != null)
                    cell.Content = ((DataGridTemplateColumn)col).CellEditingTemplate.LoadContent();
            }
        }

        private void _showCellsNormalTemplate(DataGridRow row, bool canCommit = false)
        {
            foreach (DataGridColumn col in dataGrid.Columns)
            {
                DependencyObject parent = VisualTreeHelper.GetParent(col.GetCellContent(row));
                while (parent.GetType().Name != "DataGridCell")
                    parent = VisualTreeHelper.GetParent(parent);

                DataGridCell cell = ((DataGridCell)parent);
                DataGridTemplateColumn c = (DataGridTemplateColumn)col;

                /*if (col.DisplayIndex < 4)
                {
                    if (canCommit == true)
                        ((TextBox)cell.Content).GetBindingExpression(TextBox.TextProperty).UpdateSource(); //figure out how all this works
                    else
                        ((TextBox)cell.Content).GetBindingExpression(TextBox.TextProperty).UpdateTarget(); //figure out how all this works
                }*/
                cell.Content = c.CellTemplate.LoadContent();
            }
        }

        //add a empty row button
        private void button_AddBook_Click(object sender, RoutedEventArgs e)
        {
            DataInfo.addBook(new Book(-1, "", "", "", ""));

            var border = VisualTreeHelper.GetChild(dataGrid, 0) as Decorator;
            if (border != null)
            {
                var scroll = border.Child as ScrollViewer;
                if (scroll != null) scroll.ScrollToEnd();
            }
        }

        //selection to change ordering of books based on numbers
        private void stock_dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(stock_dropdown.SelectedIndex == 0)
            {
                if (DataInfo != null)
                    DataInfo.filtdata();
            }
            else if(stock_dropdown.SelectedIndex == 1)
            {
                price_dropdown.SelectedIndex = 0;
                DataInfo.filtdata(4);
            }
            else if (stock_dropdown.SelectedIndex == 2)
            {
                price_dropdown.SelectedIndex = 0;
                DataInfo.filtdata(5);
            }
            if (DataInfo != null)
                DataInfo.searchdatahelper();
        }
        private void price_dropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (price_dropdown.SelectedIndex == 0)
            {
                if(DataInfo != null)
                    DataInfo.filtdata();
            }
            else if (price_dropdown.SelectedIndex == 1)
            {
                stock_dropdown.SelectedIndex = 0;
                DataInfo.filtdata(6);
            }
            else if (price_dropdown.SelectedIndex == 2)
            {
                stock_dropdown.SelectedIndex = 0;
                DataInfo.filtdata(7);
            }

            if (DataInfo != null)
                DataInfo.searchdatahelper();
        }


        //search for book names or generes
        private void book_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(book_search.Text != "")
                DataInfo.searchdata(2, book_search.Text);
            else
                DataInfo.searchdata();
        }

        private void genre_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (genre_search.Text != "")
                DataInfo.searchdata(3, genre_search.Text);
            else
                DataInfo.searchdata();
        }

        //search buttons
        private void button_clearfilt_Click(object sender, RoutedEventArgs e)
        {
            genre_search.Text = "";
            book_search.Text = "";

            price_dropdown.SelectedIndex = 0;
            stock_dropdown.SelectedIndex = 0;

            DataInfo.searchdata();
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            DataInfo.celledit();
        }

    }
}
