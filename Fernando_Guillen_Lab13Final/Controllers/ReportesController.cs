using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fernando_Guillen_Lab13Final.Data;

namespace Fernando_Guillen_Lab13Final.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly LINQExampleDbContext _context;
        public ReportesController(LINQExampleDbContext context)
        {
            _context = context;
        }

        [HttpGet("usuarios")]
        public async Task<IActionResult> GetUsuarios()
        {
            var usuarios = await _context.Users.ToListAsync();
            return Ok(usuarios);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }

        [HttpGet("tickets")]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _context.Tickets
                .Include(t => t.User)
                .Include(t => t.Responses)
                    .ThenInclude(r => r.Responder)
                .ToListAsync();

            var result = tickets.Select(t => new
            {
                TicketId = t.TicketId,
                Title = t.Title,
                Description = t.Description,
                Status = t.Status,
                User = t.User.Username,
                Responses = t.Responses.Select(r => $"{r.Responder.Username}: {r.Message}").ToList()
            });

            return Ok(result);
        }

        [HttpGet("reporte-excel")]
        public async Task<IActionResult> GetReporteExcel()
        {
            var users = await _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .ToListAsync();

            var roles = await _context.Roles
                .Include(r => r.UserRoles)
                    .ThenInclude(ur => ur.User)
                .ToListAsync();

            var tickets = await _context.Tickets
                .Include(t => t.User)
                .Include(t => t.Responses)
                    .ThenInclude(r => r.Responder)
                .ToListAsync();

            using var package = new ExcelPackage();

            // Hoja Users
            var wsUsers = package.Workbook.Worksheets.Add("Users");
            wsUsers.Cells[1, 1].Value = "UserId";
            wsUsers.Cells[1, 2].Value = "Username";
            wsUsers.Cells[1, 3].Value = "Email";
            wsUsers.Cells[1, 4].Value = "Roles";
            wsUsers.Cells[1, 5].Value = "CreatedAt";

            int row = 2;
            foreach (var u in users)
            {
                wsUsers.Cells[row, 1].Value = u.UserId.ToString();
                wsUsers.Cells[row, 2].Value = u.Username;
                wsUsers.Cells[row, 3].Value = u.Email;
                wsUsers.Cells[row, 4].Value = string.Join(", ", u.UserRoles.Select(ur => ur.Role.RoleName));
                wsUsers.Cells[row, 5].Value = u.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss");
                row++;
            }

            // Hoja Roles
            var wsRoles = package.Workbook.Worksheets.Add("Roles");
            wsRoles.Cells[1, 1].Value = "RoleId";
            wsRoles.Cells[1, 2].Value = "RoleName";
            wsRoles.Cells[1, 3].Value = "Usuarios";
            row = 2;
            foreach (var r in roles)
            {
                wsRoles.Cells[row, 1].Value = r.RoleId.ToString();
                wsRoles.Cells[row, 2].Value = r.RoleName;
                wsRoles.Cells[row, 3].Value = string.Join(", ", r.UserRoles.Select(ur => ur.User.Username));
                row++;
            }

            // Hoja Tickets
            var wsTickets = package.Workbook.Worksheets.Add("Tickets");
            wsTickets.Cells[1, 1].Value = "TicketId";
            wsTickets.Cells[1, 2].Value = "Title";
            wsTickets.Cells[1, 3].Value = "Description";
            wsTickets.Cells[1, 4].Value = "Status";
            wsTickets.Cells[1, 5].Value = "Usuario";
            wsTickets.Cells[1, 6].Value = "Responses";

            row = 2;
            foreach (var t in tickets)
            {
                wsTickets.Cells[row, 1].Value = t.TicketId.ToString();
                wsTickets.Cells[row, 2].Value = t.Title;
                wsTickets.Cells[row, 3].Value = t.Description;
                wsTickets.Cells[row, 4].Value = t.Status;
                wsTickets.Cells[row, 5].Value = t.User.Username;
                wsTickets.Cells[row, 6].Value = string.Join("\n", t.Responses.Select(r => $"{r.Responder.Username}: {r.Message}"));
                row++;
            }

            var stream = new MemoryStream(package.GetAsByteArray());
            stream.Position = 0;
            string excelName = "ReporteCompleto.xlsx";

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}
