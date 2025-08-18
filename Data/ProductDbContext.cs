using BitxifyProduct.Models;
using Microsoft.EntityFrameworkCore;

namespace BitxifyProduct.Data;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<ProductModel> tblProduct => Set<ProductModel>();
}
