﻿using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace AntBox
{
    /// <summary>
    /// Logique d'interaction pour About.xaml
    /// </summary>
    /// 

    public partial class About : Window
    {

        public static string title { get; set; }

        public About()
        {
            title = "AntBox";
            InitializeComponent();
        }
    }
}
