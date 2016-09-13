﻿/*
 * MeetupManager:
 * Copyright (C) 2013 Refractored LLC: 
 * http://github.com/JamesMontemagno
 * http://twitter.com/JamesMontemagno
 * http://refractored.com
 * 
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 * Help from Ed Snider http://twitter.com/EdSnider
 */
using System;
using Cirrious.MvvmCross.WindowsPhone.Views;
using MeetupManager.Portable.ViewModels;
using Microsoft.Phone.Controls;
using System.Windows;

namespace MeetupManager.WP8.Views
{
    public partial class EventView : MvxPhonePage
    {
        public new EventViewModel ViewModel
        {
            get { return (EventViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public EventView()
        {
            InitializeComponent();
        }

        private void AppBarSelectWinnerClick(object sender, EventArgs e)
        {
            this.ViewModel.SelectWinnerCommand.Execute();
        }

        private void AppBarRefreshClick(object sender, EventArgs e)
        {
            this.ViewModel.RefreshCommand.Execute();
        }

        private void AppBarAddNewUserClick(object sender, EventArgs e)
        {
            this.ViewModel.AddNewUserCommand.Execute();
        }

        private void Delete_OnClick(object sender, RoutedEventArgs e)
        {
            var selected = (sender as MenuItem).DataContext as MemberViewModel;
            ViewModel.DeleteUserCommand.Execute(selected);
        }
    }
}