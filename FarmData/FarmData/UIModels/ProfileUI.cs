using FarmData.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FarmData.UIModels
{
    class ProfileUI
    {
        FarmInfo Info;
        public ProfileUI(FarmInfo info)
        {
            Info = info;
        }
        public Frame CreateProfileFrame()
        {
            Frame frame = new Frame();


            return frame;
        }
    }
}
