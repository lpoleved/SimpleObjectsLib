using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.Controls
{
    public class ImageInfo
    {
        public string ObjectName { get; set; }
        public bool Expanded { get; set; }
        public ImageAction Action { get; set; }
        public ImageSize ImageSize { get; set; }
        public int State { get; set; }

        public string GetImageName()
        {
            string imageName = this.ObjectName;

            if (this.Expanded)
                imageName += "." + ImageManager.ImageSufixExpanded;

            if (this.Action != ImageAction.NoAction)
                imageName += "." + this.Action.ToString();

            if (this.ImageSize != ImageSize.Unspecified)
                imageName += "." + this.ImageSize.ToString();

            //if (this.State != 0)
            //{

            //}

            return imageName;
        }
    }


    // TODO:
    public class BusinessImageInfo : ImageInfo
    {
        public int StatusAlarmNormalImageIndex { get; set; }
        public int StatusAlarmWarningImageIndex { get; set; }
        public int StatusAlarmMinorImageIndex { get; set; }
        public int StatusAlarmMajorImageIndex { get; set; }
        public int StatusAlarmCriticalImageIndex { get; set; }
    }

    public enum ImageAction
    {
        NoAction,
        Add,
        Remove
    }

    public enum ImageSize
    {
        Unspecified,
        Small,
        Large,
        Big
    }
}
