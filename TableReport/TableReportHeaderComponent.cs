using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace QuestPdfLastPageIssue.TableReport;

public class TableReportHeaderComponent : IComponent
{
    private readonly TableReportHeaderModel _tableReportHeaderModel;

    public TableReportHeaderComponent(TableReportHeaderModel tableReportHeaderModel)
    {
        _tableReportHeaderModel = tableReportHeaderModel;
    }

    public void Compose(IContainer container)
    {
        container.AlignLeft().AlignBottom().Column(column =>
        {
            column.Item().PaddingTop(20).Row(row =>
            {
                row.RelativeItem().AlignLeft().AlignMiddle().Text(text =>
                {
                    text.Span("Title").Style(TextStyle.Default.FontSize(20).Medium());
                });

                row.RelativeItem().AlignRight().AlignMiddle().Text(text =>
                {
                    text.Span("Date ").Style(TextStyle.Default.FontSize(11).Medium());
                    text.Span(_tableReportHeaderModel.ShortDate).Style(TextStyle.Default.FontSize(11));
                });
            });
        });
    }
}

public class TableReportHeaderModel
{
    public string ShortDate { get; set; } = string.Empty;
}

