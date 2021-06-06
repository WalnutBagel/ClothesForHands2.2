using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClothesForHands2.ApplicationData;
using static ClothesForHands2.ApplicationData.AppConnect;

namespace ClothesForHands2.Pages
{
    /// <summary>
    /// Логика взаимодействия для ListOfMaterialPage.xaml
    /// </summary>
    public partial class ListOfMaterialPage : Page
    {
        public List<string> SuppliersList = new List<string>();

        public List<Material> MaterialList = new List<Material>(); // создаем список, в который будем выгружать наше представление.

        List<string> FilterList = new List<string>();

        List<string> SortList = new List<string>()
        {
            "Без сортировки",
            "Наименование (по возрастанию)",
            "Наименование (по убыванию)",
            "Остаток на складе(по возрастанию)",
            "Остаток на складе(по убыванию)",
            "Стоимость(по возрастанию)",
            "Стоимость(по убыванию)"
        };

        public ListOfMaterialPage()
        {
            InitializeComponent();
            UpdateTable(false);

            var typeMaterial = context.MaterialType.ToList();
            foreach (var i in typeMaterial)
            {
                FilterList.Add(i.Name);
            }

            FilterList.Insert(0, "All types");
            FilterCMB.ItemsSource = FilterList;
            FilterCMB.SelectedIndex = 0;

            ListSortBox.ItemsSource = SortList;
            ListSortBox.SelectedIndex = 0;
        }
        
        public void UpdateTable(bool Filter)
        {
            if (Filter)
            {
                lvMaterials.ItemsSource = MaterialList; // если были использованы фильтры - выводим отфильтрованный список
            }
            else
            {
                lvMaterials.ItemsSource = context.Material.ToList();  // иначе - загружаем новый
            }
        }

        public void MakeFilters()
        {
            var list = context.Material.Where(i => i.Name.Contains(TbSearch.Text)).ToList();


            // Фильтрация

            var selectFiltr = FilterCMB.SelectedIndex;

            if (selectFiltr != 0)
            {
                list = list.Where(i => i.TypeId == selectFiltr).ToList();
            }

            // сортировка по ВСЕМУ ОСТАЛЬНОМУ
            if (ListSortBox != null)
            {
                var SortSelect = ListSortBox.SelectedIndex;
                switch (SortSelect)
                {
                    case 0:
                        list = list.OrderBy(i => i.id).ToList();
                        break;
                    case 1:
                        list = list.OrderBy(i => i.Name).ToList(); // по возрастанию
                        break;

                    case 2:
                        list = list.OrderByDescending(i => i.Name).ToList(); // по убыванию
                        break;

                    case 3:
                        list = list.OrderBy(i => i.Count).ToList();
                        break;

                    case 4:
                        list = list.OrderByDescending(i => i.Count).ToList();
                        break;

                    case 5:
                        list = list.OrderBy(i => i.Price).ToList();
                        break;

                    case 6:
                        list = list.OrderByDescending(i => i.Price).ToList();
                        break;

                    default:
                        list = list.OrderBy(i => i.id).ToList();
                        break;
                }
            }


            MaterialList = list; // в список с которого проиходит выгрузка в таблицу - забиваем новый измененный
            UpdateTable(true); // обновляем таблицу
        }

        private void lvMaterials_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MakeFilters();
        }

        private void ListSortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MakeFilters();
        }

        private void FilterCMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MakeFilters();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            MakeFilters();
        }
    }
}
