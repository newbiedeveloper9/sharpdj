﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDj.Enums;

namespace SharpDj.ViewModel
{
    public class SdjLeftBarViewModel:BaseViewModel
    {
        #region .ctor

        public SdjLeftBarViewModel(SdjMainViewModel main)
        {
            SdjMainViewModel = main;
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


        private LeftBar _leftBarVisibility = LeftBar.Collapsed;
        public LeftBar LeftBarVisibility
        {
            get => _leftBarVisibility;
            set
            {
                if (_leftBarVisibility == value) return;
                _leftBarVisibility = value;
                OnPropertyChanged("LeftBarVisibility");
            }
        }
        #endregion Properties

        #region Methods


        #endregion Methods

        #region Commands

        #region LeftBar

        #region LeftBarOnFavoritesCommand
        private RelayCommand _leftBarOnFavoritesCommand;
        public RelayCommand LeftBarOnFavoritesCommand
        {
            get
            {
                return _leftBarOnFavoritesCommand
                       ?? (_leftBarOnFavoritesCommand = new RelayCommand(LeftBarOnFavoritesCommandExecute, LeftBarOnFavoritesCommandCanExecute));
            }
        }

        public bool LeftBarOnFavoritesCommandCanExecute()
        {
            return true;
        }

        public void LeftBarOnFavoritesCommandExecute()
        {

        }
        #endregion

        #region LeftBarOnPlaylistCommand
        private RelayCommand _leftBarOnPlaylistCommand;
        public RelayCommand LeftBarOnPlaylistCommand
        {
            get
            {
                return _leftBarOnPlaylistCommand
                       ?? (_leftBarOnPlaylistCommand = new RelayCommand(LeftBarOnPlaylistCommandExecute, LeftBarOnPlaylistCommandCanExecute));
            }
        }

        public bool LeftBarOnPlaylistCommandCanExecute()
        {
            return true;
        }

        public void LeftBarOnPlaylistCommandExecute()
        {
            SdjMainViewModel.SdjBottomBarViewModel.BottomBarAddToPlaylistCommandExecute();
            SdjMainViewModel.SdjLeftBarViewModel.LeftBarVisibility = LeftBar.Collapsed;
        }
        #endregion

        #region LeftBarOnRoomsCommand
        private RelayCommand _leftBarOnRoomsCommandCommand;
        public RelayCommand LeftBarOnRoomsCommand
        {
            get
            {
                return _leftBarOnRoomsCommandCommand
                       ?? (_leftBarOnRoomsCommandCommand = new RelayCommand(LeftBarOnRoomsCommandExecute, LeftBarOnRoomsCommandCanExecute));
            }
        }

        public bool LeftBarOnRoomsCommandCanExecute()
        {
            if (SdjMainViewModel.SdjLeftBarViewModel.LeftBarVisibility == LeftBar.Visible)
                return true;
            return false;

        }

        public void LeftBarOnRoomsCommandExecute()
        {
            SdjMainViewModel.SdjLeftBarViewModel.LeftBarVisibility = LeftBar.Collapsed;
            SdjMainViewModel.MainViewVisibility = MainView.Main;
        }
        #endregion

        #region LeftBarOnFriendsCommand
        private RelayCommand _leftBarOnFriendsCommand;
        public RelayCommand LeftBarOnFriendsCommand
        {
            get
            {
                return _leftBarOnFriendsCommand
                       ?? (_leftBarOnFriendsCommand = new RelayCommand(LeftBarOnFriendsCommandExecute, LeftBarOnFriendsCommandCanExecute));
            }
        }

        public bool LeftBarOnFriendsCommandCanExecute()
        {
            return true;
        }

        public void LeftBarOnFriendsCommandExecute()
        {

        }
        #endregion

        #region LeftBarOnPluginsCommand
        private RelayCommand _leftBarOnPluginsCommand;
        public RelayCommand LeftBarOnPluginsCommand
        {
            get
            {
                return _leftBarOnPluginsCommand
                       ?? (_leftBarOnPluginsCommand = new RelayCommand(LeftBarOnPluginsCommandExecute, LeftBarOnPluginsCommandCanExecute));
            }
        }

        public bool LeftBarOnPluginsCommandCanExecute()
        {
            return true;
        }

        public void LeftBarOnPluginsCommandExecute()
        {

        }
        #endregion

        #region LeftBarOnOptionsCommand
        private RelayCommand _leftBarOnOptionsCommand;
        public RelayCommand LeftBarOnOptionsCommand
        {
            get
            {
                return _leftBarOnOptionsCommand
                       ?? (_leftBarOnOptionsCommand = new RelayCommand(LeftBarOnOptionsCommandExecute, LeftBarOnOptionsCommandCanExecute));
            }
        }

        public bool LeftBarOnOptionsCommandCanExecute()
        {
            return true;
        }

        public void LeftBarOnOptionsCommandExecute()
        {

        }
        #endregion

        #region LeftBarOnReportBugCommand
        private RelayCommand _leftBarOnReportBugCommand;
        public RelayCommand LeftBarOnReportBugCommand
        {
            get
            {
                return _leftBarOnReportBugCommand
                       ?? (_leftBarOnReportBugCommand = new RelayCommand(LeftBarOnReportBugCommandExecute, LeftBarOnReportBugCommandCanExecute));
            }
        }

        public bool LeftBarOnReportBugCommandCanExecute()
        {
            return true;
        }

        public void LeftBarOnReportBugCommandExecute()
        {

        }
        #endregion

        #region LeftBarAboutCommand
        private RelayCommand _leftBarAboutCommand;
        public RelayCommand LeftBarAboutCommand
        {
            get
            {
                return _leftBarAboutCommand
                       ?? (_leftBarAboutCommand = new RelayCommand(LeftBarAboutCommandExecute, LeftBarAboutCommandCanExecute));
            }
        }

        public bool LeftBarAboutCommandCanExecute()
        {
            if (SdjMainViewModel.SdjLeftBarViewModel.LeftBarVisibility == LeftBar.Visible)
                return true;
            return false;

        }

        public void LeftBarAboutCommandExecute()
        {
            SdjMainViewModel.SdjLeftBarViewModel.LeftBarVisibility = LeftBar.Collapsed;
            SdjMainViewModel.MainViewVisibility = MainView.About;
        }
        #endregion

        #region MouseUpAwayFromLeftBarCommand
        private RelayCommand _mouseUpAwayFromLeftBarCommandCommand;
        public RelayCommand MouseUpAwayFromLeftBarCommand
        {
            get
            {
                return _mouseUpAwayFromLeftBarCommandCommand
                       ?? (_mouseUpAwayFromLeftBarCommandCommand = new RelayCommand(MouseUpAwayFromLeftBarCommandExecute, MouseUpAwayFromLeftBarCommandCanExecute));
            }
        }

        public bool MouseUpAwayFromLeftBarCommandCanExecute()
        {
            if (SdjMainViewModel.SdjLeftBarViewModel.LeftBarVisibility == LeftBar.Visible)
                return true;
            return false;
        }

        public void MouseUpAwayFromLeftBarCommandExecute()
        {
            SdjMainViewModel.SdjLeftBarViewModel.LeftBarVisibility = LeftBar.Collapsed;
        }
        #endregion

        #endregion

        #endregion Commands
    }
}