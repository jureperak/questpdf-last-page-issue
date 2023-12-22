using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QuestPdfLastPageIssue.TableReport;

public class TableReportDetailsComponent : IComponent
{
    private readonly TableReportDetailsModel _tableReportDetailsModel;

    public TableReportDetailsComponent(TableReportDetailsModel tableReportDetailsModel)
    {
        _tableReportDetailsModel = tableReportDetailsModel;
    }

    public void Compose(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                foreach (var _ in _tableReportDetailsModel.HeaderRow)
                {
                    columns.RelativeColumn();
                }
            });

            table.Header(header =>
            {
                foreach (var headerColumn in _tableReportDetailsModel.HeaderRow)
                {
                    header.Cell().Element(Block).Text(headerColumn);
                }
            });

            var cellStyle = TextStyle.Default.FontSize(8);

            foreach (var item in _tableReportDetailsModel.Items)
            {
                table.Cell().Background(Colors.Grey.Lighten3).BorderBottom(1).BorderColor(Colors.White).Padding(3).AlignLeft().Text(item).Style(cellStyle);
            }

            static IContainer Block(IContainer container)
            {
                var style = TextStyle.Default.FontSize(9).Medium();

                return container
                    .DefaultTextStyle(style)
                    .AlignMiddle()
                    .BorderBottom(1)
                    .PaddingLeft(3)
                    .PaddingRight(3);
            }
        });
    }
}

public class TableReportDetailsModel
{
    public required List<string> HeaderRow { get; set; }

    public required List<string> Items { get; set; }
}