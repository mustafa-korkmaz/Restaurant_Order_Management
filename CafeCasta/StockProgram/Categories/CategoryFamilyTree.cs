using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StockProgram.DBObjects;
using System.Diagnostics;

namespace StockProgram.Categories
{
    class CategoryFamilyTree
    {
        private int rootCatId;
        private List<CategoryItem> cItemsList;
        private List<Generation> gensList;
        public List<Generation> WholeGenerationList
        {
            get { return this.gensList; }
        }

        public CategoryFamilyTree(int rootCategoryId, ref List<CategoryItem> cItemsList)
        {
            this.rootCatId = rootCategoryId;
            this.cItemsList = cItemsList;
            gensList = new List<Generation>();
            this.GetWholeFamily();
        }
        public CategoryFamilyTree(ref List<CategoryItem> cItemsList)
        {
            this.cItemsList = cItemsList;
            //gensList = new List<Generation>();
          //  this.GetWholeFamily();
        }

        private CategoryItem GetCategoryItemFromId(int cat_id)
        {
            CategoryItem cItem = new CategoryItem();
            foreach (CategoryItem item in this.cItemsList)
            {
                if (item.Id == cat_id)
                {
                    cItem = item; // cItem ı bulduk.
                    break;
                }
            }
            return cItem;
        }

        /// <summary>
        /// verilen cat_id için en üst kategori id ye sahip item ı döndürür.
        /// </summary>
        /// <returns></returns>
        public CategoryItem GetTopCategoryItem(int cat_id)
        {
            CategoryItem cItem = new CategoryItem();
            cItem = GetCategoryItemFromId(cat_id);

            while (cItem.ParentId!=0)// parent id 0 olana kadar yukarı çıkacagız
            {
                foreach (CategoryItem item in this.cItemsList)
                {
                    if (item.Id ==cItem.ParentId)
                    {
                        cItem = item; // cItem ı parent ına atadık. böylece ağaçta 1 basamak yükseldik.
                        break;
                    }
                }
            }
            return cItem;
        }

        /// <summary>
        /// Verilen kök kategori id den başlayıp o id ile ilişkili ve o id den önce gelen tüm bireyleri döndürür.(bread crumbs)
        /// </summary>
        /// <returns></returns>
        public List<CategoryItem> GetBreadCrumbs(int currentCatId)
        {
            List<CategoryItem> RootToCurrentList = new List<CategoryItem>();
            CategoryItem cItem = new CategoryItem();
            cItem = GetCategoryItemFromId(currentCatId);
            RootToCurrentList.Add(cItem);

            while (cItem.ParentId != 0)// parent id 0 olana kadar yukarı çıkacagız
            {
                foreach (CategoryItem item in this.cItemsList)
                {
                    if (item.Id == cItem.ParentId)
                    {
                        cItem = item; // cItem ı parent ına atadık. böylece ağaçta 1 basamak yükseldik.
                        RootToCurrentList.Add(item);
                        break;
                    }
                }
            }
            return RootToCurrentList; //sondan başa doğru giden bir ağaç yolumuz oldu
        
        }

        /// <summary>
        /// En üst kategori id den başlayıp o id ile ilişkili tüm bireyleri döndürür
        /// </summary>
        /// <returns></returns>
        private List<Generation> GetWholeFamily()
        {
            Generation gen = new Generation();
            List<CategoryItem> childItems = new List<CategoryItem>();

            foreach (CategoryItem item in this.cItemsList) // first only add the parent (root) node
            {
                if (item.Id == this.rootCatId)
                {
                    childItems.Add(item);
                }
            }

            if (childItems.Count > 0)
            {
                gen.CatItemList = childItems;
                gen.GenerationID = childItems[0].Id;
                gensList.Add(gen);
                this.GetChildItems(this.rootCatId);
            }
            return this.gensList;
        }
        private void GetGenerations()
        {
            Generation gen = new Generation();  
            GetChildItems(this.rootCatId);
        }

        /// <summary>
        ///Parametre olarak gönderilen kategori id sini parentId olarak kabul eden item listesini döndürür
        /// </summary>
        /// <param name="tempParentId"></param>
        private void GetChildItems(int tempParentId)
        {
            Generation gen = new Generation();
            List<CategoryItem> childItems = new List<CategoryItem>();
     
            foreach (CategoryItem item in this.cItemsList)
            {
                if (item.ParentId==tempParentId)
                {
                    childItems.Add(item);
                }
            }
            if (childItems.Count>0)
            {
                gen.CatItemList = childItems;
                gen.GenerationID = childItems[0].ParentId;
                this.gensList.Add(gen);  
            }
     
            for (int i = 0; i < childItems.Count; i++)
            {
                GetChildItems(childItems[i].Id);
            }
            return;
        }

        /// <summary>
        /// Seçilen kategori ile ilgili olan tüm aile bireylerinin listesini döndürür
        /// </summary>
        /// <returns></returns>
        public List<int> GetFamilyMemberIDs()
        {
            List<int> categoryIdList = new List<int>();

            for (int i = 0; i < this.gensList.Count; i++)
            {
                foreach (CategoryItem item in this.gensList[i].CatItemList)
                {
                    categoryIdList.Add(item.Id);
                }
            }
            return categoryIdList;
        }

    }
}
