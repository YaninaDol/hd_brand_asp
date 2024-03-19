
using hd_brand_asp.Data;
using hd_brand_asp.Models;
using Microsoft.AspNetCore.Http;

namespace RepoLibrary.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(HdBrandDboContext context) : base(context)
        {

        }



        
        

        bool IProductRepository.deleteSize(int Id, int Size)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Product> IProductRepository.getByCategory(int id)
        {
            if (db.Categories.Any(x => x.Id.Equals(id)))

            {


                //add other fields
                return db.Products.Where(x => x.Categoryid == id).ToList();

            }
            else return  db.Products.ToList();

        }

        IEnumerable<Product> IProductRepository.getByMaterial(int id)
        {
            if (db.Materials.Any(x => x.Id.Equals(id)))

            {

                return db.Products.Where(x => x.Materialid == id).ToList();

            }
            else return db.Products.ToList();
        }

        IEnumerable<Product> IProductRepository.getBySeason(int id)
        {
            if (db.Seasons.Any(x => x.Id.Equals(id)))

            {

                return db.Products.Where(x => x.Seasonid == id).ToList();

            }
            else return db.Products.ToList();
        }

        IEnumerable<Product> IProductRepository.getByShoeType(int id)
        {
            if (db.SubCategories.Any(x => x.Id.Equals(id)))

            {

                return db.Products.Where(x => Convert.ToInt32(x.SubCategoryid) == id).ToList();

            }
            else return db.Products.ToList();
        }

       
            IEnumerable<Productssize> IProductRepository.getSizes(int productId)
        {
            if (db.Products.Any(x => x.Id.Equals(productId)))

            {
                return db.Productssizes.Where(x => x.Productid == productId).ToList();

            }
            else return null;
        }

        bool IProductRepository.procedure(string id,Product item)
        {
           if (id=="1")
            {

                for(int i=35;i<43;i++)
                {
                    db.Productssizes.Add(new Productssize() { Productid = item.Id,Image=item.Image,Name=item.Name,Size=i.ToString(),Price=item.Price });
                }
                return true;
            }
            else if (id == "11")
            {

                for (int i = 36; i < 42; i++)
                {
                    db.Productssizes.Add(new Productssize() { Productid = item.Id, Image = item.Image, Name = item.Name, Size = i.ToString(), Price = item.Price });
                }
                return true;
            }
            else if (id == "12")
            {

                for (int i = 28; i < 47; i++)
                {
                    db.Productssizes.Add(new Productssize() { Productid = item.Id, Image = item.Image, Name = item.Name, Size = i.ToString(), Price = item.Price });
                }
                return true;
            }
            else 
            {

               
                db.Productssizes.Add(new Productssize() { Productid = item.Id, Image = item.Image, Name = item.Name, Size = "XS", Price = item.Price });
                db.Productssizes.Add(new Productssize() { Productid = item.Id, Image = item.Image, Name = item.Name, Size = "S", Price = item.Price });
                db.Productssizes.Add(new Productssize() { Productid = item.Id, Image = item.Image, Name = item.Name, Size = "M", Price = item.Price });
                db.Productssizes.Add(new Productssize() { Productid = item.Id, Image = item.Image, Name = item.Name, Size = "L", Price = item.Price });
                db.Productssizes.Add(new Productssize() { Productid = item.Id, Image = item.Image, Name = item.Name, Size = "XL", Price = item.Price });
                return true;
            }
        }

        IEnumerable<Productssize> IProductRepository.updateprocedure(int id)
        {
           return db.Productssizes.Where((x)=>x.Productid.Equals(id)).ToList();
        }
        string IProductRepository.SaveFile(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                string uniqueFileName = GetUniqueFileName(file.FileName);
                // Путь к корневой папке проекта
                string uploadsFolder = @"C:\Users\1\OneDrive\Документы\GitHub\hd_brand_front\src\assets";
                // Путь, куда будет сохранен файл на сервере
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return filePath.Replace('\\', '/');
            }
            return null; 
        }


        string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
        public void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

    }
}
