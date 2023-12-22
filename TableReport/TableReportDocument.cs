using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QuestPdfLastPageIssue.TableReport;

internal class TableReportDocument : IDocument
{
    private readonly TableReportDocumentModel _model;

    public TableReportDocument(TableReportDocumentModel model)
    {
        _model = model;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(32);
                page.Size(PageSizes.A4);

                page.Header().ShowOnce().Component(new TableReportHeaderComponent(_model.TableReportHeader));

                page.Content().Component(new TableReportDetailsComponent(_model.TableReportDetails));

                page.Footer().Dynamic(new TableReportFooterComponentDynamic(_model.TableReportFooter));
            });
    }
}

public class TableReportDocumentModel
{
    public required TableReportHeaderModel TableReportHeader { get; set; }

    public required TableReportFooterModel TableReportFooter { get; set; }

    public required TableReportDetailsModel TableReportDetails { get; set; }
}