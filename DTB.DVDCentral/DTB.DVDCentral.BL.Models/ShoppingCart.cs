using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTB.DVDCentral.BL.Models
{
    public class ShoppingCart
    {
        public double TotalCost { get; set; }
        public List<Movie> Items { get; set; }
        public int TotalCount { get { return Items.Count; } }

        public ShoppingCart()
        {
            Items = new List<Movie>();
        }
        public void Add(Movie movie)
        {
            Items.Add(movie);
            TotalCost += (double)movie.Cost;
        }
        public void Remove(Movie movie)
        {
            Items.Remove(movie);
            TotalCost -= (double)movie.Cost;
        }
        public void CheckOut()
        {
            Items = new List<Movie>();
            TotalCost = 0;

        }
    }
}
