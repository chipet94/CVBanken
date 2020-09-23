// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
//
// namespace CVBanken.Web.Controllers
// {
//     public class TemplateModel
//     {
//         
//     }
//
//     [ApiController]
//     [Route("api/[controller]")]
//     [Authorize]
//     public class TemplateController : ControllerBase
//     {
//         public TemplateController()
//         {
//        
//         }
//
//         [HttpPost]
//         [AllowAnonymous]
//         public async Task<IActionResult> Post()
//         {
//             return Ok();
//         }
//         [HttpPost]
//         [AllowAnonymous]
//         public async Task<IActionResult> Create(TemplateModel model)
//         {
//             if (model == null)
//             {
//                 return BadRequest();
//             }
//
//             return Ok();
//         }
//         
//         [HttpGet]
//         public async Task<IActionResult> GetAll()
//         {
//             return Ok();
//         }
//         
//         [HttpGet("{id}")]
//         public async Task<IActionResult> GetById(int id)
//         {
//             return Ok();
//         }
//         
//         [HttpDelete("{id}")]
//         public async Task<IActionResult> Delete(int id)
//         {
//             return Ok();
//         }
//         
//         [HttpPut("{id}")]
//         public async Task<IActionResult> Update()
//         {
//             return Ok();
//         }
//         
//     }
// }
