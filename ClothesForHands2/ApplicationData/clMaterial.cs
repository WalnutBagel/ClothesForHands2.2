using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ClothesForHands2.ApplicationData.AppConnect;

namespace ClothesForHands2.ApplicationData
{
    public partial class Material
    {
        public string GetTypeAndName { get => $"{MaterialType.Name} | {Name}"; }

        public string GetSuppliers
        {
            get
            {
                string suppliers = "";
                List<MaterialSupplier> supplierlist = new List<MaterialSupplier>();
                supplierlist = context.MaterialSupplier.Where(i => i.MaterialId.Equals(id)).ToList();
                foreach (var i in supplierlist)
                {
                    string supplier = context.Supplier.Where(j => j.id.Equals(i.SupplierId)).FirstOrDefault().SupplierName;
                    suppliers += supplier + ", ";


                }
                if (suppliers.Length > 0)
                {
                    suppliers = suppliers.Remove(suppliers.Length - 2);
                }
                return suppliers;
            }
        }

        public string GetMinCount { get => $"Минимальное количество: {MinCount} шт."; }

        public string GetCount { get => $"Остаток: {Count} шт."; }

        public string GetColor
        {
            get
            {
                if (Count <= MinCount)
                {
                    return "#f19292";
                }
                else if (Count <= MinCount * 3)
                {
                    return "#ffba01";
                }
                else
                {
                    return "#fff";
                }
            }
        }
        public string GetImage
        {
            get
            {
                if (string.IsNullOrEmpty(PhotoPath))
                {
                    return $"/Resources/nullValuePic.png";
                }
                else
                {
                    return $"/Resources/PhotoMaterials{PhotoPath}";
                }
            }
        }
    }
}
