using ClosedXML.Excel;
using Fernando_Guillen_Lab13Final.Data;
using Microsoft.EntityFrameworkCore;

namespace Fernando_Guillen_Lab13Final.Services
{
    public class ExcelReportService
    {
        private readonly LINQExampleDbContext _context;

        public ExcelReportService(LINQExampleDbContext context)
        {
            _context = context;
        }
        public async Task<byte[]> GenerarReporteTicketsAsync()
        {
            var tickets = await _context.Tickets
                .Include(t => t.User)
                .Include(t => t.Responses)
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Tickets");
            ws.Cell(1, 1).Value = "Ticket ID";
            ws.Cell(1, 2).Value = "Título";
            ws.Cell(1, 3).Value = "Descripción";
            ws.Cell(1, 4).Value = "Usuario";
            ws.Cell(1, 5).Value = "Respuestas";

            int row = 2;

            foreach (var t in tickets)
            {
                ws.Cell(row, 1).Value = t.TicketId.ToString();
                ws.Cell(row, 2).Value = t.Title;
                ws.Cell(row, 3).Value = t.Description;
                ws.Cell(row, 4).Value = t.User?.Username ?? "Sin usuario";
                ws.Cell(row, 5).Value = t.Responses?.Count ?? 0;

                row++;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
        public async Task<byte[]> GenerarReporteUsuariosAsync()
        {
            var usuarios = await _context.Users
                .Include(u => u.Tickets)
                .ToListAsync();

            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Usuarios");

            ws.Cell(1, 1).Value = "User ID";
            ws.Cell(1, 2).Value = "Username";
            ws.Cell(1, 3).Value = "Email";
            ws.Cell(1, 4).Value = "Total Tickets";

            int row = 2;
            foreach (var u in usuarios)
            {
                ws.Cell(row, 1).Value = u.UserId.ToString();
                ws.Cell(row, 2).Value = u.Username;
                ws.Cell(row, 3).Value = u.Email;
                ws.Cell(row, 4).Value = u.Tickets?.Count ?? 0;

                row++;
            }
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
