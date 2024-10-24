using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MEESEEKS.Core.CodeGeneration.CRUD
{
    public static class CrudMethodGenerator
    {
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
