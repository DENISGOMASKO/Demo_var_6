using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Demo_var_6
{
    internal class ProductViewModel : PropertyChangedBase
    {
        private bool allPropertiesFilled;
        private int _selectedCategory;
        private bool _picChanged;
        private ObservableCollection<string> _categories;
        private Visibility _visibility;
        public bool CanEdit
        {
            get => ApplicationContext.GetUserRoleID()==1;
        }
        public ObservableCollection<string> Categories
        {
            get => _categories;
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }
        public bool AllPropertiesFilled
        {
            get => allPropertiesFilled;
            set
            {
                allPropertiesFilled = value;
                OnPropertyChanged(nameof(AllPropertiesFilled));
            }
        }
        public int SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        public Visibility Visibility
        {
            get => _visibility;
            set
            {
                _visibility = value;
                OnPropertyChanged(nameof(Visibility));
            }
        }
        public bool PictureChanged
        {
            get => _picChanged;
            set
            {
                _picChanged = value;
                OnPropertyChanged(nameof(PictureChanged));
            }
        }
        private BitmapImage _ImageSource;
        public BitmapImage ImageSource
        {
            get { return _ImageSource; }
            set
            {
                _ImageSource = value;
                OnPropertyChanged(nameof(ImageSource));
            }
        }
        private bool isAdding;
        private ProductModel _model;
        public Product CurrentProduct
        {
            get; set;
        }
        public ProductViewModel(Product? prod)
        {
            _model = new();
            using TradeCompletedContext context = new TradeCompletedContext();
            Categories = new ObservableCollection<string>( (from cat in context.ProductCategories select cat.CategoryName) );
            SelectedCategory = prod?.Category-1 ?? -1;
            Visibility = Visibility.Visible;
            if (prod == null)
            {
                CurrentProduct = new Product() { Photo = "picture.png" };
                Visibility = Visibility.Hidden;
                isAdding = true;
            }
            else
            {
                CurrentProduct = prod;
                isAdding = false;
            }
            if (ApplicationContext.GetUserRoleID() != 1)
            {
                Visibility = Visibility.Hidden;
            }
        }
        public CommonCommand SaveChanges
        { get => new CommonCommand(() =>
            {
                using TradeCompletedContext context = new();
                if(isAdding)
                {
                    Product pr = new Product();
                    foreach (PropertyInfo property in typeof(Product).GetProperties().Where(p => p.CanWrite))
                    {
                        property.SetValue(pr, property.GetValue(CurrentProduct, null), null);
                    }
                    pr.Category = SelectedCategory + 1;
                    if(!PictureChanged) pr.Photo = null;
                    else
                    {
                        if (!_model.IsPathInResources(ImageSource.UriSource))
                            _model.CopyImageToResources(ImageSource.UriSource.ToString());
                        pr.Photo = Path.GetFileName(ImageSource.UriSource.ToString());
                    }
                    context.Products.Add(pr);
                }
                else
                {
                    Product pr = context.Products.Where(pr => pr.ProductId == CurrentProduct.ProductId).First();
                    foreach (PropertyInfo property in typeof(Product).GetProperties().Where(p => p.CanWrite))
                    {
                        property.SetValue(pr, property.GetValue(CurrentProduct, null), null);
                    }
                    pr.Category = SelectedCategory+1;
                    if(PictureChanged)
                    {
                        if (!_model.IsPathInResources(ImageSource.UriSource))
                            _model.CopyImageToResources(ImageSource.UriSource.ToString());
                        pr.Photo = Path.GetFileName( ImageSource.UriSource.ToString());
                    }
                }
                context.SaveChanges();
            }
            , () => true);
        }
        public CommonCommand RemoveProduct
        {
            get => new CommonCommand(() =>
            {
                using TradeCompletedContext context = new();
                context.Products.Remove(context.Products.Where(pr => pr.ProductId == CurrentProduct.ProductId).First());
                context.SaveChanges();
            }
            , () => true);
        }
        public CommonCommand ChangeProductPicture
        {
            get => new CommonCommand(() =>
            {
                BitmapImage pic = _model.SetPicture();
                if(pic != null)
                {
                    ImageSource = pic;
                    PictureChanged = true;
                }
            });
         }
    }
}
