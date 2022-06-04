using Elsa.Expressions.Models;
using Elsa.Expressions.Services;
using Elsa.JavaScript.Expressions;
using Elsa.Workflows.Core.Services;

namespace Elsa.Dsl.Interpreters;

public partial class WorkflowDefinitionBuilderInterpreter
{
    public override IWorkflowDefinitionBuilder VisitExpressionMarker(ElsaParser.ExpressionMarkerContext context)
    {
        var language = context.ID();
        var expressionContent = context.expressionContent().GetText();

        // TODO: Determine actual expression type based on specified language.
        var expression = new JavaScriptExpression(expressionContent);
        var expressionReference = new JavaScriptExpressionReference(expression);
        var externalReference = new ExternalExpressionReference(expression, expressionReference);
        _expressionValue.Put(context, externalReference);
        _expressionValue.Put(context.Parent, externalReference);

        return DefaultResult;
    }
}

public record ExternalExpressionReference(IExpression Expression, MemoryReference Reference);