using System.Reflection;
using QuestPDF.Elements;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace QuestPdfLastPageIssue.TableReport;

public class TableReportFooterComponentDynamic : IDynamicComponent<int>
{
    private readonly TableReportFooterModel _tableReportFooterModel;

    public TableReportFooterComponentDynamic(TableReportFooterModel tableReportFooterModel)
    {
        _tableReportFooterModel = tableReportFooterModel;
    }

    public int State { get; set; }

    public DynamicComponentComposeResult Compose(DynamicContext context)
    {
        var content = context.CreateElement(element =>
        {
            var style = TextStyle.Default.FontSize(8);
            var pagerStyle = TextStyle.Default.FontSize(9);

            if (context.PageNumber == context.TotalPages)
            {
                element.Column(column =>
                {
                    /* -----------------------------TRY CHANGING SPACING BELOW------------------------------- */
                    //column.Spacing(7); // WORKING
                    column.Spacing(8); // NOT WORKING
                    /* -------------------------------------------------------------------------------------- */

                    column.Item().Width(125).Image($"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/Content/logo.png");
                    
                    column.Item().Row(row =>
                    {
                        row.Spacing(25);
                        row.AutoItem().Text($"{_tableReportFooterModel.Address} {_tableReportFooterModel.City}, {_tableReportFooterModel.Country}").Style(style);
                        row.AutoItem().Text($"SSN: {_tableReportFooterModel.Ssn}").Style(style);
                        row.AutoItem().Text($"{_tableReportFooterModel.PhoneNumber}").Style(style);
                        row.AutoItem().Text($"{_tableReportFooterModel.Email}").Style(style);
                        row.AutoItem().Text($"{_tableReportFooterModel.Website}").Style(style);
                    });

                    column.Item().LineHorizontal(1);

                    column.Item().AlignRight().Text($"{context.PageNumber}/{context.TotalPages}").Style(pagerStyle);
                });
            }
            else
            {
                element.Text($"{context.PageNumber}/{context.TotalPages}").Style(pagerStyle);
            }
        });

        return new DynamicComponentComposeResult
        {
            Content = content,
            HasMoreContent = false
        };
    }
}

public class TableReportFooterModel
{
    public string Country { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public string Ssn { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Website { get; set; } = string.Empty;
}