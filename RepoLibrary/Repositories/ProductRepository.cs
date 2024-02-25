
using hd_brand_asp.Data;
using hd_brand_asp.Models;

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
        
    }
}
