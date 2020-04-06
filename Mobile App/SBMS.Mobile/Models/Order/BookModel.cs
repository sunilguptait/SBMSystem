using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XF.Material.Forms.Models;

namespace SBMS.Mobile.Models.Order
{
    [Serializable]
    public class BookModel
    {
        public int Book_Id { get; set; }
        public string Book_Name { get; set; }
        public string Book_ShortName { get; set; }
        public int? Book_PublisherId { get; set; }
        public decimal Book_Price { get; set; }
        public int? Book_BSMId { get; set; }
        public int? Book_TypeId { get; set; }
        public string PublisherName { get; set; }
        public string BookType { get; set; }
        public int? DefaultQuantity { get; set; }
        public int? Quantity { get; set; }
        public string Book_Image { get; set; } = "imagenotfound";
        public bool IsSelected { get; set; } = true;
        public decimal? Discount { get; set; }
        //Xamarin Design Properties
        public decimal TotalAmount { get { return Convert.ToInt32(Quantity) * Book_Price; } }
        public bool IsIncrementButtonEnabled { get; set; } = true;
        public bool IsDecrementButtonEnabled { get; set; } = true;
        public int ItemOrignalIndex { get; set; } = 0;
        public string ActionButtonIcon { get { return IsSelected ? "trash" : "restore"; } }
        public string CellColor { get { return IsSelected ? "White" : "#eee"; } }
        public string PriceLabel { get { return $"{ Quantity} * {Book_Price} ="; } }
        public int? ItemIndex { get; set; }

    }
    public class BookSearchModel
    {
        public int StudentId { get; set; } = 0;
        public int ClassId { get; set; } = 0;
    }
}
