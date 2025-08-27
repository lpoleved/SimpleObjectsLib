using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;

namespace Simple.Controls
{
    public partial class ImageControl : Component
    {
        private const string strCheckBox = "CheckBox";
        //private static CheckBoxImageKeys checkBoxImageKeys = null;

        private ImageList smallImageList = null;
        private ImageList largeImageList = null;
        private ImageList stateImageList = null;
        private ImageList checkBoxImageList = null;
        private ImageList customImageList = null;

        private static ImageList smallImageCollection = null;
        private static ImageList largeImageCollection = null;
        private static ImageList stateImageCollection = null;
        private static ImageList checkBoxImageCollection = null;
        private static ImageList customImageCollection = null;

        public static readonly string ImageOptionSeparator = ".";
		public static readonly string ImageOptionAdd = "Add";
		public static readonly string ImageOptionExpanded = "Expanded";
        public static readonly string ImageOptionAddExt = "AddExt";
        public static readonly string ImageOptionEditExt = "EditExt";
        public static readonly string ImageOptionRemoveExt = "RemoveExt";
        
        public static string ImageNameFlag = "Flag";

        public static readonly string ImageNameCheckBoxCkecked                       = "CheckBox.Checked";
        public static readonly string ImageNameCheckBoxUnckecked                     = "CheckBox.Unchecked";
        public static readonly string ImageNameCheckBoxCheckedChildChecked           = "CheckBox.Checked.ChildChecked";
        public static readonly string ImageNameCheckBoxCheckedChildUnchecked         = "CheckBox.Checked.ChildUnchecked";
        public static readonly string ImageNameCheckBoxUncheckedChildChecked         = "CheckBox.Unchecked.ChildChecked";
        public static readonly string ImageNameCheckBoxUncheckedChildUnchecked       = "CheckBox.Unchecked.ChildUnchecked";
        public static readonly string ImageNameCheckBoxCkeckedGreyed                 = "CheckBox.Checked.Greyed";
        public static readonly string ImageNameCheckBoxUnckeckedGreyed               = "CheckBox.Unchecked.Greyed";
        public static readonly string ImageNameCheckBoxCheckedChildCheckedGreyed     = "CheckBox.Checked.ChildChecked.Greyed";
        public static readonly string ImageNameCheckBoxCheckedChildUncheckedGreyed   = "CheckBox.Checked.ChildUnchecked.Greyed";
        public static readonly string ImageNameCheckBoxUncheckedChildCheckedGreyed   = "CheckBox.Unchecked.ChildChecked.Greyed";
        public static readonly string ImageNameCheckBoxUncheckedChildUncheckedGreyed = "CheckBox.Unchecked.ChildUnchecked.Greyed";

        public static readonly string ImageNameStatusAlarmNormal   = "StatusAlarmNormal";
        public static readonly string ImageNameStatusAlarmWarning  = "StatusAlarmWarning";
        public static readonly string ImageNameStatusAlarmMinor    = "StatusAlarmMinor";
        public static readonly string ImageNameStatusAlarmMajor    = "StatusAlarmMajor";
        public static readonly string ImageNameStatusAlarmCritical = "StatusAlarmCritical";

        public static readonly string ImageNameBubbleGreenAlarm    = "Bubble.Alarm.Green";
        public static readonly string ImageNameBubbleCyanAlarm     = "Bubble.Alarm.Cyan";
        public static readonly string ImageNameBubbleYellowAlarm   = "Bubble.Alarm.Yellow";
        public static readonly string ImageNameBubbleOrangeAlarm   = "Bubble.Alarm.Orange";
        public static readonly string ImageNameBubbleRedAlarm      = "Bubble.Alarm.Red";


        private static string ImageNameOptionLarge = "Large";
		
		public ImageControl()
        {
            InitializeComponent();

            if (this.CheckBoxImageList != null)
                LoadImages(this.CheckBoxImageList, CheckBoxImageCollection);
            
            if (this.SmallImageList != null)
                LoadImages(this.SmallImageList, SmallImageCollection);

			if (this.LargeImageList != null)
                LoadImages(this.LargeImageList, LargeImageCollection);

            if (this.CustomImageList != null)
                LoadImages(this.CustomImageList, CustomImageCollection);

            this.Load(new Properties.Resources(), createAlarmImages: false, createUpdateImages: false);
        }

        public ImageControl(IContainer container)
            : this()
        {
            container.Add(this);
        }

        //public static CheckBoxImageKeys CheckBoxImageKeys
        //{
        //    get
        //    {
        //        if (checkBoxImageKeys == null)
        //        {
        //            checkBoxImageKeys = new CheckBoxImageKeys();
        //        }

        //        return checkBoxImageKeys;
        //    }
        //}

        [DefaultValue(null)]
        public ImageList SmallImageList
        {
            get { return this.smallImageList; }
            set
            {
                this.smallImageList = value;
                SetImageListSettings(this.smallImageList);
            }
        }

        [DefaultValue(null)]
        public ImageList LargeImageList
        {
            get { return this.largeImageList; }
            set
            {
                this.largeImageList = value;
                SetImageListSettings(this.largeImageList);
            }
        }

        [DefaultValue(null)]
        public ImageList StateImageList
        {
            get { return this.stateImageList; }
            set
            {
                this.stateImageList = value;
                SetImageListSettings(this.stateImageList);
            }
        }

        [DefaultValue(null)]
        public ImageList CheckBoxImageList
        {
            get { return this.checkBoxImageList; }
            set
            {
                this.checkBoxImageList = value;
                SetImageListSettings(this.checkBoxImageList);
            }
        }

        [DefaultValue(null)]
        public ImageList CustomImageList
        {
            get { return this.customImageList; }
            set
            {
                this.customImageList = value;
                SetImageListSettings(this.customImageList);
            }
        }

        public static ImageList SmallImageCollection
        {
            get
            {
                if (smallImageCollection == null)
                {
                    smallImageCollection = new ImageList();
                    SetImageListSettings(smallImageCollection);
                }

                return smallImageCollection;
            }
        }

        public static ImageList LargeImageCollection
        {
            get
            {
                if (largeImageCollection == null)
                {
                    largeImageCollection = new ImageList();
                    SetImageListSettings(largeImageCollection);
					largeImageCollection.ImageSize = new Size(32, 32);
                }

                return largeImageCollection;
            }
        }

        public static ImageList StateImageCollection
        {
            get
            {
                if (stateImageCollection == null)
                {
                    stateImageCollection = new ImageList();
                    SetImageListSettings(stateImageCollection);
                }

                return largeImageCollection;
            }
        }

        public static ImageList CheckBoxImageCollection
        {
            get
            {
                if (checkBoxImageCollection == null)
                {
                    checkBoxImageCollection = new ImageList();
                    SetImageListSettings(checkBoxImageCollection);
                }

                return checkBoxImageCollection;
            }
        }

        public static ImageList CustomImageCollection
        {
            get
            {
                if (customImageCollection == null)
                {
                    customImageCollection = new ImageList();
                    SetImageListSettings(customImageCollection);
                }

                return customImageCollection;
            }
        }

        public static void LoadImages(ImageList imageList, ImageList originImageList)
        {
            foreach (string imageName in originImageList.Images.Keys)
            {
                Image image = originImageList.Images[imageName];

                //string newImageName = imageName.Replace("_", ".");
                imageList.Images.Add(imageName, image);
            }
        }

        public static string ImageNameAddOption(string imageName, string imageOption)
        {
            return ImageNameAddOption(imageName, imageOption, 0); 
        }

        public static string ImageNameAddOption(string imageName, string imageOption, int insertionPosition)
        {
            string newImageName = imageName;

            if (!String.IsNullOrEmpty(imageName) && !String.IsNullOrEmpty(imageOption) && imageName.Trim().Length > 0 && imageOption.Trim().Length > 0)
            {
                List<string> imageBaseNameAndOptions = imageName.Split(new string[] { ImageOptionSeparator }, StringSplitOptions.RemoveEmptyEntries).ToList();
                //string imageBaseName = imageBaseNameAndOptions[0];
                //List<string> currentImageOptions = imageBaseNameAndOptions.ToList();
                //currentImageOptions.RemoveAt(0);

                if (!imageBaseNameAndOptions.Contains(imageOption))
                {
                    if (insertionPosition >= 0 && insertionPosition < imageBaseNameAndOptions.Count)
                    {
                        imageBaseNameAndOptions.Insert(insertionPosition, imageOption);
                    }
                    else
                    {
                        // Add option at the end
                        imageBaseNameAndOptions.Add(imageOption);
                    }
                }

                newImageName = String.Join(ImageOptionSeparator, imageBaseNameAndOptions);
            }
            else
            {
                //throw new ArgumentException("You try to add image option but the image name or image option is empty.");
            }

            return newImageName;
        }

        public static string ImageNameRemoveOption(string imageName, string imageOption)
        {
            string newImageName = imageName;

            if (!String.IsNullOrEmpty(imageName) && !String.IsNullOrEmpty(imageOption) && imageName.Trim().Length > 0 && imageOption.Trim().Length > 0)
            {
                string[] imageBaseNameAndOptions = imageName.Split(new string[] { ImageOptionSeparator }, StringSplitOptions.RemoveEmptyEntries);
                string imageBaseName = imageBaseNameAndOptions[0];
                List<string> currentImageOptions = imageBaseNameAndOptions.ToList();
                currentImageOptions.RemoveAt(0);

                if (currentImageOptions.Contains(imageOption))
                    currentImageOptions.Remove(imageOption);

                string newImageOptions = String.Join(ImageOptionSeparator, currentImageOptions.ToArray());

                newImageName = newImageOptions == "" ? imageBaseName : imageBaseName + ImageOptionSeparator + newImageOptions;
            }
            else
            {
                //throw new ArgumentException("You try to remove image option but the image name or image option is empty.");
            }

            return newImageName;
        }

        public void Load(object resources)
        {
            this.Load(resources, createAlarmImages: true, createUpdateImages: true);
        }

        public void Load(object resources, bool createAlarmImages, bool createUpdateImages)
        {
            foreach (PropertyInfo property in resources.GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static))
            {
                object? propertyValue = property.GetValue(resources, null);

				if (propertyValue is Image image)
					this.LoadItem(property.Name, image, createAlarmImages, createUpdateImages);
			}
		}

        protected virtual void LoadItem(string key, Image image, bool createAlarmImages, bool createUpdateImages)
        {
            ImageList imageList = CustomImageCollection;
            bool isForSmallImageCollection = false;

            if (image.Size.Height == image.Size.Width)
            {
                int size = image.Size.Height;

                if (size == 16)
                {
                    if (key.StartsWith(strCheckBox + "_"))
                    {
                        imageList = CheckBoxImageCollection;
                    }
                    else
                    {
                        imageList = SmallImageCollection;
                        isForSmallImageCollection = true;
                    }
                }
                else if (size == 32)
                {
                    imageList = LargeImageCollection;
				
					if (key.EndsWith('_' + ImageControl.ImageNameOptionLarge))
						key = key.Remove(key.Length - ImageControl.ImageNameOptionLarge.Length - 1, ImageControl.ImageNameOptionLarge.Length + 1);
				}
            }

            key = key.Replace('_', '.');
            imageList.Images.Add(key, image);

            if (isForSmallImageCollection && !key.StartsWith(ImageNameFlag + ImageOptionSeparator))
            {
                byte alpha = 230;

                if (createAlarmImages)
                {
                    Image alarmImage = MatrixBlend(new Bitmap(image), Properties.Resources.Bubble_Alarm_Green, alpha);
                    imageList.Images.Add(key + ImageControl.ImageOptionSeparator + ImageControl.ImageNameStatusAlarmNormal, alarmImage);

                    alarmImage = MatrixBlend(new Bitmap(image), Properties.Resources.Bubble_Alarm_Cyan, alpha);
                    imageList.Images.Add(key + ImageControl.ImageOptionSeparator + ImageControl.ImageNameStatusAlarmWarning, alarmImage);

                    alarmImage = MatrixBlend(new Bitmap(image), Properties.Resources.Bubble_Alarm_Yellow, alpha);
                    imageList.Images.Add(key + ImageControl.ImageOptionSeparator + ImageControl.ImageNameStatusAlarmMinor, alarmImage);

                    alarmImage = MatrixBlend(new Bitmap(image), Properties.Resources.Bubble_Alarm_Orange, alpha);
                    imageList.Images.Add(key + ImageControl.ImageOptionSeparator + ImageControl.ImageNameStatusAlarmMajor, alarmImage);

                    alarmImage = MatrixBlend(new Bitmap(image), Properties.Resources.Bubble_Alarm_Red, alpha);
                    imageList.Images.Add(key + ImageControl.ImageOptionSeparator + ImageControl.ImageNameStatusAlarmCritical, alarmImage);
                }

                if (createUpdateImages)
                {
                    Image updateImage = MatrixBlend(new Bitmap(image), Properties.Resources.AddExt, alpha);
                    imageList.Images.Add(key + ImageControl.ImageOptionSeparator + ImageControl.ImageOptionAddExt, updateImage);

                    updateImage = MatrixBlend(new Bitmap(image), Properties.Resources.EditExt, alpha);
                    imageList.Images.Add(key + ImageControl.ImageOptionSeparator + ImageControl.ImageOptionEditExt, updateImage);

                    updateImage = MatrixBlend(new Bitmap(image), Properties.Resources.RemoveExt, alpha);
                    imageList.Images.Add(key + ImageControl.ImageOptionSeparator + ImageControl.ImageOptionRemoveExt, updateImage);
                }
            }
        }

        private void LoadImageList(ImageList imageList, ImageList sourceImageList)
        {
            foreach (string imageKey in imageList.Images.Keys)
            {
                Image image = imageList.Images[imageKey];
                
                imageList.Images.Add(imageKey, image);
            }
        }

        private static void SetImageListSettings(ImageList imageList)
        {
            imageList.ColorDepth = ColorDepth.Depth32Bit;
        }

        public static Bitmap MergeTwoImages(Image firstImage, Image secondImage)
        {
            if (firstImage == null)
                throw new ArgumentNullException("firstImage");

            if (secondImage == null)
                throw new ArgumentNullException("secondImage");

            int outputImageWidth = firstImage.Width > secondImage.Width ? firstImage.Width : secondImage.Width;
            int outputImageHeight = firstImage.Height + secondImage.Height + 1;
            Bitmap outputImage = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            using (Graphics graphics = Graphics.FromImage(outputImage))
            {
                graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size),
                    new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
                graphics.DrawImage(secondImage, new Rectangle(new Point(0, firstImage.Height + 1), secondImage.Size),
                    new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
            }

            return outputImage;
        }

        /// <summary>
        /// Merges Images into 1 Image.
        /// </summary>
        /// <param name="images">The Images you want merge</param>
        /// <returns>An Image of all images</returns>
        public static Image MergeImages(Image[] images)
        {
            if (images == null || images.Length <= 0)
                return null;

            Int32 imageWSize = 0;
            Int32 imageHSize = 0;

            for (int i=0; i < images.Length; i++)
            {
                if (images[i].Width > imageWSize)
                    imageWSize = images[i].Width;

                if (images[i].Height > imageHSize)
                    imageHSize = images[i].Height;
            }

            Int32 width = 0;
            Int32 height = 0;
            int picsInOneLine = 10;

            if (images.Length >= picsInOneLine)
            {
                width = picsInOneLine * imageWSize;
                decimal d = (images.Length + picsInOneLine) / picsInOneLine;
                height = (int)Math.Round(d) * imageHSize;
            }
            else
            {
                width = images.Length * imageWSize;
                height = imageHSize;
            }

            Bitmap bitmap = new Bitmap(width, height);
            int hhh = -1;
            int www = 0;
            
            for (int i=0; i < images.Length; i++)
            {
                Bitmap image = new Bitmap(images[i]);
                
                if (i % picsInOneLine == 0)
                {
                    hhh++;
                    www = 0;
                }

                //Get All of the x Pixels
                for (int w = 0; w < imageWSize; w++)
                {
                    //Get All of the Y Pixels
                    for (int h = 0; h < imageHSize; h++)
                    {
                        //Set the Cooresponding Pixel
                        int ww = w + (www * imageWSize);
                        int hh = h + (hhh * imageHSize);
                        bitmap.SetPixel(ww, hh, image.GetPixel(w, h));
                    }
                }

                www++;

            }

            //Return the new Bitmap
            return bitmap;
        }


        public static Bitmap Combine(Bitmap[] images)
        {
            List<Bitmap> newImages = new List<Bitmap>(images);
            Bitmap? finalImage = null;

            try
            {
                int width = 0;
                int height = 0;

                foreach (Bitmap bitmap in images)
                {
                    //update the size of the final bitmap
                    width += bitmap.Width;
                    height = bitmap.Height > height ? bitmap.Height : height;

                    newImages.Add(bitmap);
                }

                //create a bitmap to hold the combined image
                finalImage = new System.Drawing.Bitmap(width, height);

                //get a graphics object from the image so we can draw on it
                using (Graphics g = Graphics.FromImage(finalImage))
                {
                    //set background color
                    g.Clear(Color.Black);

                    //go through each image and draw it on the final image
                    int offset = 0;
                    foreach (System.Drawing.Bitmap image in newImages)
                    {
                        g.DrawImage(image,
                          new Rectangle(offset, 0, image.Width, image.Height));
                        offset += image.Width;
                    }
                }

                return finalImage;
            }
            catch 
            {
                if (finalImage != null)
                    finalImage.Dispose();

				throw;
			}
            finally
            {
                //clean up memory
                foreach (Bitmap image in newImages)
                    image.Dispose();
            }
        }

        public static Bitmap SimpleBlend(Bitmap image1, Bitmap image2, byte alpha)
        {
            Bitmap result = new Bitmap(image1);

            // update the alpha for each pixel of image 2
            for (int x = 0; x < image2.Width; x++)
                for (int y = 0; y < image2.Height; y++)
                    result.SetPixel(x, y, Color.FromArgb(alpha, result.GetPixel(x, y)));

            // draw image 2 on image 1

            using (Graphics g = Graphics.FromImage(result))
            {
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.DrawImageUnscaled(image2, 0, 0);
            }

            return result;
        }

        public static Bitmap MatrixBlend(Bitmap image1, Bitmap image2, byte alpha)
        {
            Bitmap result = new Bitmap(image1);

            // for the matrix the range is 0.0 - 1.0
            float alphaNorm = (float)alpha / 255.0F;
            // just change the alpha
            ColorMatrix matrix = new ColorMatrix(new float[][]{
                new float[] {1F, 0, 0, 0, 0},
                new float[] {0, 1F, 0, 0, 0},
                new float[] {0, 0, 1F, 0, 0},
                new float[] {0, 0, 0, alphaNorm, 0},
                new float[] {0, 0, 0, 0, 1F}});

            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(matrix);

            using (Graphics g = Graphics.FromImage(result))
            {
                g.CompositingMode = CompositingMode.SourceOver;
                g.CompositingQuality = CompositingQuality.HighQuality;

                g.DrawImage(image2,
                    new Rectangle(0, 0, image1.Width, image1.Height),
                    0,
                    0,
                    image2.Width,
                    image2.Height,
                    GraphicsUnit.Pixel,
                    imageAttributes);
            }

            return result;
        }
    }
}
