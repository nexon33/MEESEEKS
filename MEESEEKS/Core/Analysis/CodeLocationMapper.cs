using System;
using Microsoft.CodeAnalysis;
using MEESEEKS.Models.CodeAnalysis;
using MEESEEKS.Models.CodeAnalysis.Enums;

namespace MEESEEKS.Core.Analysis
{
    /// <summary>
    /// Maps Roslyn locations to custom code locations.
    /// </summary>
    internal class CodeLocationMapper
    {
        /// <summary>
        /// Maps Roslyn diagnostic severity to custom issue severity.
        /// </summary>
        /// <param name="severity">The Roslyn diagnostic severity.</param>
        /// <returns>The mapped issue severity.</returns>
        public IssueSeverity MapSeverity(DiagnosticSeverity severity)
        {
            return severity switch
            {
                DiagnosticSeverity.Error => IssueSeverity.Error,
                DiagnosticSeverity.Warning => IssueSeverity.Warning,
                DiagnosticSeverity.Info => IssueSeverity.Info,
                DiagnosticSeverity.Hidden => IssueSeverity.Info,
                _ => IssueSeverity.Info
            };
        }

        /// <summary>
        /// Maps Roslyn location to custom code location.
        /// </summary>
        /// <param name="location">The Roslyn location.</param>
        /// <returns>The mapped code location, or null if the input is null.</returns>
        public CodeLocation? MapLocation(Location? location)
        {
            if (location == null)
                return null;

            var span = location.GetLineSpan();
            return new CodeLocation
            {
                FilePath = span.Path ?? string.Empty,
                StartLine = span.StartLinePosition.Line + 1,
                EndLine = span.EndLinePosition.Line + 1,
                StartColumn = span.StartLinePosition.Character + 1,
                EndColumn = span.EndLinePosition.Character + 1
            };
        }
    }
}
