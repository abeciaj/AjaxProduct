using System.Diagnostics;
using BitxifyProduct.Data;
using Microsoft.AspNetCore.Mvc;
using BitxifyProduct.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace BitxifyProduct.Controllers;

public class HomeController : Controller
{
    private readonly ProductDbContext _context;
    private readonly ILogger<HomeController> _logger;
    public HomeController(ProductDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<IActionResult> Index()
    {
        var products = await _context.tblProduct.ToListAsync();
        return View(products);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetDetailsById(int id)
    {
        var product = await _context.tblProduct.FindAsync(id);
        if (product == null)
            return new JsonResult(new { responseCode = 1, responseMessage = "Not found" });

        return new JsonResult(new { responseCode = 0, responseMessage = JsonConvert.SerializeObject(product) });
    }

    [HttpPost]
    public async Task<IActionResult> InsertProduct([FromForm] ProductModel product)
    {
        _context.tblProduct.Add(product);
        await _context.SaveChangesAsync();
        return new JsonResult(new { responseCode = 0, responseMessage = JsonConvert.SerializeObject(product) });
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProduct(int id, string name, float price)
    {
        var product = await _context.tblProduct.FindAsync(id);
        if (product == null)
            return new JsonResult(new { responseCode = 1, responseMessage = "Not found" });

        product.Name = name;
        product.Price = price;
        await _context.SaveChangesAsync();

        return new JsonResult(new { responseCode = 0, responseMessage = JsonConvert.SerializeObject(product) });
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.tblProduct.FindAsync(id);
        if (product == null)
            return new JsonResult(new { responseCode = 1, responseMessage = "Not found" });

        _context.tblProduct.Remove(product);
        await _context.SaveChangesAsync();

        return new JsonResult(new { responseCode = 0, responseMessage = "Deleted" });
    }
}