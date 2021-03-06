﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Caliburn.Micro;
using SharpDj.ViewModels.SubViews.SearchMenuComponents;

namespace SharpDj.Models
{
    public class ConversationModel : PropertyChangedBase
    {
        private readonly IEventAggregator _eventAggregator;

        #region Properties
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name == value) return;
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        private bool _isReaded;
        public bool IsReaded
        {
            get => _isReaded;
            set
            {
                if (_isReaded == value) return;
                _isReaded = value;
                NotifyOfPropertyChange(() => IsReaded);
            }
        }

        private SolidColorBrush _color;
        public SolidColorBrush Color
        {
            get => _color;
            set
            {
                if (_color == value) return;
                _color = value;
                NotifyOfPropertyChange(() => Color);
                ConversationPopupViewModel.Color = value;
            }
        }

        private string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                if (_imagePath == value) return;
                _imagePath = value;
                NotifyOfPropertyChange(() => ImagePath);
            }
        }

        private bool _isOpen = false;
        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                if (_isOpen == value) return;
                _isOpen = value;
                NotifyOfPropertyChange(() => IsOpen);
            }
        }
      
        private ConversationPopupViewModel _conversationPopupViewModel;
        public ConversationPopupViewModel ConversationPopupViewModel
        {
            get => _conversationPopupViewModel;
            set
            {
                if (_conversationPopupViewModel == value) return;
                _conversationPopupViewModel = value;
                NotifyOfPropertyChange(() => ConversationPopupViewModel);
            }
        }

        #endregion Properties

        public ConversationModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _conversationPopupViewModel = new ConversationPopupViewModel(_eventAggregator, this);
        }
    }
}
