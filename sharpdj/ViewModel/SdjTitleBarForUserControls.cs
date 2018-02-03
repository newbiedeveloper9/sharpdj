﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpDj.ViewModel
{
    public class SdjTitleBarForUserControls : BaseViewModel
    {
        #region .ctor

        public SdjTitleBarForUserControls(SdjMainViewModel main, Action onCloseForm)
        {
            SdjMainViewModel = main;
            CloseFormExecute = onCloseForm;
        }
        #endregion .ctor

        #region Properties
        private SdjMainViewModel _sdjMainViewModel;
        public SdjMainViewModel SdjMainViewModel
        {
            get => _sdjMainViewModel;
            set
            {
                if (_sdjMainViewModel == value) return;
                _sdjMainViewModel = value;
                OnPropertyChanged("SdjMainViewModel");
            }
        }

        private string _formName;
        public string FormName
        {
            get => _formName;
            set
            {
                if (_formName == value) return;
                _formName = value;
                OnPropertyChanged("FormName");
            }
        }

        #endregion Properties

        #region Methods


        #endregion Methods

        #region Commands

        #region CloseForm
        private RelayCommand _closeForm;
        public RelayCommand CloseForm
        {
            get
            {
                return _closeForm
                       ?? (_closeForm = new RelayCommand(CloseFormExecute, CloseFormCanExecute));
            }
        }

        public bool CloseFormCanExecute()
        {
            return true;
        }

        public Action CloseFormExecute { get; set; }

        #endregion

        #endregion Commands


    }
}