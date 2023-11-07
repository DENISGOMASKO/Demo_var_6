using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Demo_var_6
{
    class ProductModel
    {
        public BitmapImage SetPicture()
        {
            OpenFileDialog dialog = new();
            dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            dialog.CheckPathExists = true;
            dialog.InitialDirectory = Path.GetFullPath("res\\");
            bool isSelected = dialog.ShowDialog().Value;
            if (isSelected)
            {
                if (Path.GetFileName(dialog.FileName) != ApplicationContext._mockPictureName)
                {
                    if (!Directory.GetFiles(Path.GetFullPath("res\\")).Any(x => Path.GetFileName(x) == Path.GetFileName(dialog.FileName)) || IsPathInResources(new Uri(dialog.FileName)))
                    {
                        BitmapImage selectedImage = new BitmapImage(new Uri(dialog.FileName));
                        if (selectedImage.PixelWidth > 300 || selectedImage.PixelHeight > 200)
                            MessageBox.Show("Разрешение файла не должно превышать 300x200 пикселей!");
                        else 
                            return selectedImage;
                    }
                        
                    else MessageBox.Show($"Файл с именем '{Path.GetFileName(dialog.FileName)}' уже существует в базе данных. Переименуйте файл");
                }
                else
                {
                    MessageBox.Show($"ВНИМАНИЕ!!! имя '{ApplicationContext._mockPictureName}' используется для изображения-заглушки товаров без указанных фото. Переименуйте файл и попытайтесь снова");
                }
            }
            return null;
        }
        public bool IsPathInResources(Uri path) => path.ToString().Replace("file:///", "").Replace('/', '\\') == Path.GetFullPath("res\\") + Path.GetFileName(path.ToString());
        public void DeleteOldImage(string path) => File.Delete(Path.GetFullPath("res\\") + path);
        public void CopyImageToResources(string initPath) => File.Copy(@initPath.Replace("file:///", ""), Path.GetFullPath("res\\") + Path.GetFileName(initPath));
    }
}
