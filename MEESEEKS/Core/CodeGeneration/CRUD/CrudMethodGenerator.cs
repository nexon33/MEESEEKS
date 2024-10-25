using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MEESEEKS.Core.CodeGeneration.CRUD
{
    /// <summary>
    /// Coordinates the generation of all CRUD operation methods.
    /// </summary>
    public static class CrudMethodGenerator
    {
        /// <summary>
        /// Generates all CRUD operation methods asynchronously.
        /// </summary>
        /// <returns>A list of method declarations for CRUD operations.</returns>
        public static async Task<List<MemberDeclarationSyntax>> GenerateAllAsync()
        {
            return await Task.Run(() => new List<MemberDeclarationSyntax>
            {
                CreateMethodGenerator.Generate(),
                ReadMethodGenerator.Generate(),
                UpdateMethodGenerator.Generate(),
                DeleteMethodGenerator.Generate(),
                ListMethodGenerator.Generate()
            });
        }
    }
}
