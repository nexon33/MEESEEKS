using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MEESEEKS.Core
{
    public partial class CodeGenerator
    {
        private T AddXmlDocumentation<T>(T declaration, string description) where T : SyntaxNode
        {
            var documentationComment = SyntaxFactory.DocumentationComment(
                SyntaxFactory.XmlText(SyntaxFactory.XmlTextLiteral($"/// ")),
                SyntaxFactory.XmlElement("summary",
                    SyntaxFactory.SingletonList<XmlNodeSyntax>(
                        SyntaxFactory.XmlText(
                            SyntaxFactory.XmlTextLiteral(Environment.NewLine),
                            SyntaxFactory.XmlTextLiteral("/// "),
                            SyntaxFactory.XmlTextLiteral(description),
                            SyntaxFactory.XmlTextLiteral(Environment.NewLine),
                            SyntaxFactory.XmlTextLiteral("/// ")))));

            return declaration.WithLeadingTrivia(
                SyntaxFactory.TriviaList(
                    SyntaxFactory.Trivia(documentationComment)));
        }

        private T AddXmlDocumentationWithParams<T>(T declaration, string description, params (string name, string description)[] parameters) where T : SyntaxNode
        {
            var paramNodes = parameters.Select(p =>
            {
                var nameAttr = SyntaxFactory.XmlNameAttribute(p.name);
                var paramElement = SyntaxFactory.XmlElement(
                    SyntaxFactory.XmlElementStartTag(SyntaxFactory.XmlName("param"),
                        SyntaxFactory.SingletonList<XmlAttributeSyntax>(nameAttr)),
                    SyntaxFactory.SingletonList<XmlNodeSyntax>(
                        SyntaxFactory.XmlText(
                            SyntaxFactory.XmlTextLiteral(Environment.NewLine),
                            SyntaxFactory.XmlTextLiteral("/// "),
                            SyntaxFactory.XmlTextLiteral(p.description),
                            SyntaxFactory.XmlTextLiteral(Environment.NewLine),
                            SyntaxFactory.XmlTextLiteral("/// "))),
                    SyntaxFactory.XmlElementEndTag(SyntaxFactory.XmlName("param")));
                return paramElement;
            });

            var summaryNode = SyntaxFactory.XmlElement("summary",
                SyntaxFactory.SingletonList<XmlNodeSyntax>(
                    SyntaxFactory.XmlText(
                        SyntaxFactory.XmlTextLiteral(Environment.NewLine),
                        SyntaxFactory.XmlTextLiteral("/// "),
                        SyntaxFactory.XmlTextLiteral(description),
                        SyntaxFactory.XmlTextLiteral(Environment.NewLine),
                        SyntaxFactory.XmlTextLiteral("/// "))));

            var nodes = new[] { summaryNode }.Concat(paramNodes);
            var documentationComment = SyntaxFactory.DocumentationComment(nodes.ToArray());

            return declaration.WithLeadingTrivia(
                SyntaxFactory.TriviaList(
                    SyntaxFactory.Trivia(documentationComment)));
        }

        private string ExtractXmlDocumentation(SyntaxNode node)
        {
            var trivia = node.GetLeadingTrivia()
                .Select(t => t.GetStructure())
                .OfType<DocumentationCommentTriviaSyntax>()
                .FirstOrDefault();

            if (trivia == null)
                return string.Empty;

            var summary = trivia.ChildNodes()
                .OfType<XmlElementSyntax>()
                .FirstOrDefault(x => x.StartTag.Name.ToString() == "summary");

            if (summary == null)
                return string.Empty;

            return string.Join(Environment.NewLine,
                summary.Content
                    .OfType<XmlTextSyntax>()
                    .SelectMany(text => text.TextTokens)
                    .Select(token => token.ValueText.Trim()))
                .Trim();
        }
    }
}
