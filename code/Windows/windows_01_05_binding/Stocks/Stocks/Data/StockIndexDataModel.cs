/***
 * Excerpted from "Seven Mobile Apps in Seven Weeks",
 * published by The Pragmatic Bookshelf.
 * Copyrights apply to this code. It may not be used to create training material,
 * courses, books, articles, and the like. Contact us if you are in doubt.
 * We make no guarantees that this code is fit for any purpose.
 * Visit http://www.pragmaticprogrammer.com/titles/7apps for more book information.
***/
﻿using Stocks.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Data {
  public class StockIndexDataModel : INotifyPropertyChanged {

    private string quotes;
    public string QuoteList {
      get { return quotes; }
      private set {
        quotes = value;
        NotifyPropertyChanged("QuoteList");
      }
    }

    public StockIndexDataModel() {
      QuoteList = "";
      GetQuotesForSymbols("^IXIC,^GSPC,^FTSE,^N225");
    }

    public async void GetQuotesForSymbols(string symbols) {
      var request = new StockQuoteRequest();
      var message = await request.RequestStockQuotes(symbols);
      QuoteList = QuoteListToString(message.Quotes);
    }
    //...
    
    private string QuoteListToString(List<StockQuote> quotes) {
      var stringBuilder = new StringBuilder();
      foreach (StockQuote stock in quotes) {
        stringBuilder.Append(stock.Name + "\n");
      }
      return stringBuilder.ToString();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void NotifyPropertyChanged(string name) {
      if (name != null && PropertyChanged != null) {
        PropertyChanged(this, new PropertyChangedEventArgs(name));
      }
    }
  }
}
