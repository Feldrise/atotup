﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atotupe.Interfaces;
using Xamarin.Forms;

namespace Atotupe.Tools
{
    public class EntryPopup
    {
        public string Text { get; set; }
        public string Title { get; set; }
        public List<string> Buttons { get; set; }

        public EntryPopup(string title, string text, params string[] buttons)
        {
            Title = title;
            Text = text;
            Buttons = buttons.ToList();
        }

        public EntryPopup(string title, string text) : this(title, text, "OK", "Cancel")
        {
        }

        public event EventHandler<EntryPopupClosedArgs> PopupClosed;
        public void OnPopupClosed(EntryPopupClosedArgs e)
        {
            PopupClosed?.Invoke(this, e);
        }

        public void Show(string type)
        {
            DependencyService.Get<IEntryPopupLoader>().ShowPopup(this, type);
        }
    }

    public class EntryPopupClosedArgs : EventArgs
    {
        public string Text { get; set; }
        public string Button { get; set; }
    }
}
