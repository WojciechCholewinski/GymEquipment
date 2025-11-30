using GymEquipment.Api.Contracts;
using GymEquipment.Application.Products;
using Microsoft.AspNetCore.Mvc;

namespace GymEquipment.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _products;

    public ProductsController(IProductService products)
    {
        _products = products;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll(CancellationToken cancellationToken)
    {
        var products = await _products.GetAllAsync(cancellationToken);
        return Ok(products.ToDto());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var product = await _products.GetByIdAsync(id, cancellationToken);

        if (product is null)
            return NotFound();

        return Ok(product.ToDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var (validation, product) = await _products.CreateAsync(
            request.Name,
            request.Type,
            request.Category,
            request.WeightKg,
            request.QuantityAvailable,
            request.Price,
            request.Description,
            cancellationToken);

        if (!validation.IsValid || product is null)
        {
            return validation.ToBadRequest(this);
        }

        var dto = product.ToDto();

        return CreatedAtAction(
            nameof(GetById),
            new { id = dto.Id },
            dto);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var existing = await _products.GetByIdAsync(id, cancellationToken);
        if (existing is null)
            return NotFound();

        var (validation, updated) = await _products.UpdateAsync(
            id,
            request.QuantityAvailable,
            request.Price,
            request.Description,
            cancellationToken);

        if (!validation.IsValid || updated is null)
        {
            return validation.ToBadRequest(this);
        }

        return Ok(updated.ToDto());
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _products.DeleteAsync(id, cancellationToken);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    [HttpGet("{id:guid}/history")]
    public async Task<ActionResult<IEnumerable<ProductHistoryEntryDto>>> GetHistory(Guid id, CancellationToken cancellationToken)
    {
        var product = await _products.GetByIdAsync(id, cancellationToken);
        if (product is null)
            return NotFound();

        var history = await _products.GetHistoryAsync(id, cancellationToken);
        return Ok(history.ToDto());
    }
}
